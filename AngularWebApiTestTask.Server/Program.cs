﻿using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using AngularWebApiTestTask.Server.Database;
using AngularWebApiTestTask.Server.Database.Models;
using AngularWebApiTestTask.Server.Domain;
using AngularWebApiTestTask.Server.Infrastructure;
using Microsoft.AspNetCore.Identity;
using AngularWebApiTestTask.Server.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = ".NET Core, Angular application API",
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddCors(x => x.AddDefaultPolicy(p => p.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()));

ConfigureMiddlewares();
ConfigureServices();

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors();
app.MapControllers();
app.MapFallbackToFile("/index.html");

InitializeDb();

app.Run();
return;

void ConfigureMiddlewares()
{
    builder.Services.AddTransient<ExceptionHandlingMiddleware>();
}

void ConfigureServices()
{
    builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

    builder.Services.AddScoped<ICountryRepository, CountryRepository>();
    builder.Services.AddScoped<IProvinceRepository, ProvinceRepository>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
}

void InitializeDb()
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    DbInitializer.InitializeIfNeeded(context);
}