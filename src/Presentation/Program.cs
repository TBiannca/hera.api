using Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetSection("ConnectionString").Value;
builder.Services.AddDbContext<Context>(options => options.UseMySQL(connectionString));

var app = builder.Build();

app.MapGet("/", () =>
{
    using var serviceScope = ((IApplicationBuilder) app).ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope();
    var context = serviceScope.ServiceProvider.GetRequiredService<Context>();
    context.Database.EnsureCreated();

    return "Hello World!";
});

app.Run();