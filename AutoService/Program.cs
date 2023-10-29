using System.Data.Common;
using AutoService.Data.Repositories;
using AutoService.Repository;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using FluentMigrator.Runner.Initialization;

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

var serviceProvider = new ServiceCollection()
    .AddFluentMigratorCore()
    .ConfigureRunner(builder => builder
        .AddSqlServer()
        .WithGlobalConnectionString("postgres://User:User1234@localhost:5431/AutoServiceDB\"")
        .ScanIn(typeof(YourMigrationClass).Assembly).For.Migrations())
    .BuildServiceProvider();



builder.Services.AddTransient<DbConnection>(s => new NpgsqlConnection("postgres://User:User1234@localhost:5431/AutoServiceDB"));
builder.Services.AddTransient<DbConnection>(s => new NpgsqlConnection("Server=localhost:5431;Database=AutoServiceDB;User Id=User;Password=User1234;"));

var app = builder.Build();

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
