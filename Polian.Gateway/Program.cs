using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;
using Polian.Gateway.Extension;

var builder = WebApplication.CreateBuilder(args);
builder.AddAppAuthetication();
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();
app.MapGet("/", () => "Hello World!");
app.UseOcelot().GetAwaiter().GetResult();
app.Run();
