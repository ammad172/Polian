using Email.Services.Api.Messaging;
using Email.Services.Api.Model;
using Email.Services.Api.Services;
using Email.Services.Api.Services.IService;
using RabbitMQ.Client.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddHostedService<RabbitMQConsmerService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
