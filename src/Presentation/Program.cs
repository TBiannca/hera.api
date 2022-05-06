using System.Text;
using Data;
using Domain;
using Domain.Auth.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
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

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = "https://localhost",
            ValidIssuer = "https://localhost",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret"))
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddMvc().AddNewtonsoftJson();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy",  policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:3000")
            .WithMethods("*")
            .WithHeaders(HeaderNames.ContentType);
    });
});

builder.Services.AddScoped<Schema>();
builder.Services.AddScoped<Presentation.Person.Creating.IResolver, Presentation.Person.Creating.Resolver>();
builder.Services.AddScoped<Presentation.Person.Fetching.IResolver, Presentation.Person.Fetching.Resolver>();

var app = builder.Build();

app.UseRouting();
app.UseAuthorization();
app.UseCors("Policy");
app.UseAuthentication();
app.MapControllers();

using var serviceScope = ((IApplicationBuilder) app).ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope();
var context = serviceScope.ServiceProvider.GetRequiredService<Context>();
context.Database.Migrate();

app.Run();