using System.Data.Common;
using AutoService.Data.Repositories;
using AutoService.Repository;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using FluentMigrator.Runner.Initialization;
using AutoService.Data.Migrations;
using System;
using System.Reflection;

[assembly: ApiController]
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<CustomerRepository>();
builder.Services.AddSingleton<CarRepository>();
builder.Services.AddSingleton<OrderRepository>();
builder.Services.AddSingleton<CarRepositorySQL>();
builder.Services.AddSingleton<CustomerRepositorySQL>();
builder.Services.AddSingleton<OrderRepositorySQL>();

builder.Services
    .AddFluentMigratorCore().ConfigureRunner(rb =>
        rb.AddPostgres().
            WithGlobalConnectionString("Server=localhost:5431;Database=AutoServiceDB;User Id=User;Password=User1234;")
            .ScanIn(Assembly.GetExecutingAssembly()).For
            .Migrations())
    .AddLogging(lb => lb.AddFluentMigratorConsole())
    .BuildServiceProvider(false);

builder.Services.AddTransient<DbConnection>(s => new NpgsqlConnection("Server=localhost:5431;Database=AutoServiceDB;User Id=User;Password=User1234;"));

var app = builder.Build();
using var serviceprovider = app.Services.CreateScope();
var services = serviceprovider.ServiceProvider;
var runner = services.GetRequiredService<IMigrationRunner>();
runner.MigrateUp();

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
