using System.Text.Json;
using System.Text.Json.Serialization;
using AutoBogus;
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
using ThisRefactored.Domain.Entities;
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
var isNewDb = false;
if (!Directory.Exists("App_Data"))
{
    isNewDb = true;
    Directory.CreateDirectory("App_Data");
}

if (!File.Exists("App_Data/products.db"))
{
    isNewDb = true;
    using var _ = File.Create("App_Data/products.db");
}

builder.Services.AddDbContext<ProductDbContext>(options => options.UseSqlite("Data Source=App_Data/products.db"));

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

if (isNewDb && app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
    context.Database.Migrate();
    // seed data
    var productFaker = new AutoFaker<Product>().RuleFor(x => x.Name, fake => fake.Commerce.ProductName())
                                               .RuleFor(x => x.Description, fake => fake.Commerce.ProductDescription())
                                               .RuleFor(x => x.Price, fake => decimal.Parse(fake.Commerce.Price()))
                                               .RuleFor(x => x.DeliveryPrice, fake => decimal.Parse(fake.Commerce.Price()))
                                               .RuleFor(x => x.ProductOptions, _ => null!);

    var products = productFaker.Generate(11);

    var productOptionFaker = new AutoFaker<ProductOption>().RuleFor(x => x.Name, fake => fake.Commerce.ProductName())
                                                           .RuleFor(x => x.Description, fake => fake.Commerce.ProductDescription())
                                                           .RuleFor(x => x.ProductId,
                                                                    fake => fake.PickRandom(products).Id)
                                                           .RuleFor(x => x.Product, _ => null!);
    
    var productOptions = productOptionFaker.Generate(30);
    context.ProductOptions.AddRange(productOptions);
    context.Products.AddRange(products);
    context.SaveChanges();
}

app.Run();