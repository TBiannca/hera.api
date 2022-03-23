using Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetSection("ConnectionString").Value;
var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));
builder.Services.AddDbContext<Context>(options => options.UseMySql(connectionString, serverVersion));

var app = builder.Build();

using var serviceScope = ((IApplicationBuilder) app).ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope();
var context = serviceScope.ServiceProvider.GetRequiredService<Context>();
context.Database.Migrate();

app.MapGet("/", () => "Hello World!");

app.Run();