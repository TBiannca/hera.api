using System.Text;
using Data;
using Domain;
using Domain.Auth.Models;
using Domain.Person.Creating.Commands;
using Domain.Person.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Presentation.Auth;
using Presentation.GraphQL.Base;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetSection("ConnectionString").Value;
var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));
builder.Services.AddDbContext<Context>(options => options.UseMySql(connectionString, serverVersion));

builder.Services.AddIdentity<MApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<Context>()
    .AddDefaultTokenProviders();

builder.Services.AddTransient<ITokenService, TokenService>();

builder.Services.AddAuthorization();
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = "https://localhost:7216/",
            ValidIssuer = "https://localhost:7216/",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"))
        };
    });
builder.Services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddMvc().AddNewtonsoftJson();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy",  policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:3000")
            .WithMethods("*")
            .WithHeaders("*");
    });
});

builder.Services.AddScoped<Schema>();
builder.Services.AddScoped<Presentation.Person.Creating.IResolver, Presentation.Person.Creating.Resolver>();
builder.Services.AddScoped<Presentation.Person.Fetching.IResolver, Presentation.Person.Fetching.Resolver>();
builder.Services.AddScoped<Presentation.Person.Deleting.IResolver, Presentation.Person.Deleting.Resolver>();
builder.Services.AddScoped<ICreatePerson, CreatePerson>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

var app = builder.Build();

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("Policy");
app.MapControllers();

using var serviceScope = ((IApplicationBuilder) app).ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope();
var context = serviceScope.ServiceProvider.GetRequiredService<Context>();
context.Database.Migrate();

app.Run();