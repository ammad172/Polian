using Authentication.Service.api.RabbitMqSender;
using Coupon.Authentication.Service.api;
using Coupon.Authentication.Service.api.Models;
using Coupon.Authentication.Service.api.Models.Model;
using Coupon.Authentication.Service.api.Services;
using Coupon.Authentication.Service.api.Services.IService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
// Add services to the container.
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddControllers();
builder.Services.AddScoped<IJWTtokengenerator, JWTtokengenerator>();
builder.Services.AddScoped<IAuth, Auth>();
builder.Services.AddScoped<IRabbitMQAuthsender, RabbitMQAuthsender>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("Connection2"));
});

builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            new string[] { }

        }

    });

});
var secret = builder.Configuration.GetValue<string>("JwtOptions:Secret");
var issuer = builder.Configuration.GetValue<string>("JwtOptions:Issuer");
var audience = builder.Configuration.GetValue<string>("JwtOptions:Audience");

var key = Encoding.ASCII.GetBytes(secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(y =>
{
    y.Authority = "https://localhost:7001";
    y.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidAudience = audience,
        ValidIssuer = issuer
    };
});
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth Api");
    c.RoutePrefix = string.Empty;

});
//}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

Initialize(app.Services);

app.Run();


// dbInitializer.cs
void Initialize(IServiceProvider serviceProvider)
{
    using (var serviceScope = serviceProvider.CreateScope())
    {
        var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

        // auto migration
        if (context.Database.GetPendingMigrations().Count() > 0)
        {
            context.Database.Migrate();
        }

    }
}
