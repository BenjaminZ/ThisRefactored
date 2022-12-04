using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ThisRefactored.Application;
using ThisRefactored.Application.Commands.CreateProduct;
using ThisRefactored.Domain;
using ThisRefactored.Persistence;
using ThisRefactored.WebApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Serilog
builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
builder.Services.AddTransient<IDomainEventPublisher, MediatorEventPublisher>();

builder.Services.AddControllers(c =>
         {
             // use json format
             c.OutputFormatters.Clear();
             var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
             options.Converters.Add(new JsonStringEnumConverter());
             c.OutputFormatters.Add(new SystemTextJsonOutputFormatter(options));

             // use general error handling
             c.Filters.Add<ModelStateValidationFilter>();
             c.Filters.Add<RequestInvalidFilter>();
             c.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
         })
        .AddJsonOptions(c =>
         {
             c.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
             c.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
         });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// db context
if (!Directory.Exists("App_Data"))
{
    Directory.CreateDirectory("App_Data");
}
if (!File.Exists("App_Data/products.db"))
{
    File.Create("App_Data/products.db");
}
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlite("Data Source=App_Data/products.db"));

// MediatR
builder.Services.AddMediatR(typeof(MediatorEventPublisher));

// fluent validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(CreateProductValidator));
builder.Services.AddFluentValidationRulesToSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/api/error-development");
}
else
{
    app.UseExceptionHandler("/api/error");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
    context.Database.Migrate();
    // seed data
}

app.Run();