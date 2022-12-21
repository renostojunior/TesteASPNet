using TesteASPNet.Application.Model;
using TesteASPNet.Domain.Entity;
using TesteASPNet.Domain.Interfaces;
using TesteASPNet.Infra.Context;
using TesteASPNet.Infra.Repository;
using AutoMapper;
using TesteASPNet.Architecture.Service.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TesteASPNet.Service.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connection = builder.Configuration["MySqlConnection:MySqlConnectionString"];

builder.Services.AddDbContext<MySqlContext>(
        options => options.UseMySql(connection, new MariaDbServerVersion(new Version(10, 9, 2))));

builder.Services.AddSingleton(new MapperConfiguration(config =>
{
    config.CreateMap<Product, ProductModel>().ReverseMap();
}).CreateMapper());

builder.Services.AddScoped<IBaseRepository<Product>, BaseRepository<Product>>();
builder.Services.AddScoped<IBaseService<Product>, BaseService<Product>>();
builder.Services.AddSingleton<ProductValidator>();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
