using Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<Context>(options => options.UseMySQL(connectionString));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();