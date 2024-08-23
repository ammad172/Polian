using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Polian.App.Services;
using Polian.App.Services.IServices;
using Polian.App.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<ICouponService, CouponService>();
builder.Services.AddHttpClient<IProductsService, ProductsService>();
builder.Services.AddHttpClient<IAuthService, AuthService>();
builder.Services.AddHttpClient<ICartService, CartService>();
builder.Services.AddHttpClient<IOrderService, OrderService>();
builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });

Utils.ProductsBase = builder.Configuration["ServiceUrls:ProductsService"] ?? "";
Utils.CouponBase = builder.Configuration["ServiceUrls:CouponService"] ?? "";
Utils.AuthApiBase = builder.Configuration["ServiceUrls:AuthService"] ?? "";
Utils.CartApiBase = builder.Configuration["ServiceUrls:CartService"] ?? "";
Utils.OrderApiBase = builder.Configuration["ServiceUrls:OrderService"] ?? "";

builder.Services.AddScoped<IBaseServices, BaseServices>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
     .AddCookie(option =>
     {
         option.ExpireTimeSpan = TimeSpan.FromHours(10);
         option.LoginPath = "/Authentication/Login";
         option.AccessDeniedPath = "/Authentication/AccedDenied";
     });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseNotyf();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
