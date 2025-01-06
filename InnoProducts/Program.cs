using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Claims;
using FluentValidation;
using HalfbitZadanie.Extensions;
using InnoProducts.Models;
using InnoProducts.Repositories;
using InnoProducts.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddCookie(IdentityConstants.ApplicationScheme)
    .AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddApiEndpoints();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// builder.Services.AddRequestValidations(); 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}
// app.UseAuthorization();
app.MapGet("users/me", async (ClaimsPrincipal claims, ApplicationDbContext context) =>
    {
        string userId = claims.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        return await context.Users.FindAsync(userId);
    })
    .RequireAuthorization();

app.UseHttpsRedirection();
app.MapControllers();
app.MapIdentityApi<User>();

app.Run();





















/*
using System.Reflection;
using System.Security.Claims;
using HalfbitZadanie.Extensions;
using InnoProducts.Models;
using InnoProducts.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddCookie(IdentityConstants.ApplicationScheme)
    .AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddApiEndpoints();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}
app.MapControllers();

app.MapGet("users/me", async (ClaimsPrincipal claims, ApplicationDbContext context) =>
    {
        string userId = claims.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        return await context.Users.FindAsync(userId);
    })
    .RequireAuthorization();

app.UseHttpsRedirection();
app.MapIdentityApi<User>();

app.Run();
*/

