using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Ryzen.Shop.Infrastructure;
using Ryzen.Shop.Trolley.Api.Business;
using Ryzen.Shop.Trolley.Api.Services;
using Ryzen.Shop.Trolley.Api.Validators;
using Ryzen.Shop.Trolley.Api.ViewModel;
using Ryzen.Shop.Trolley.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddValidatorsFromAssemblyContaining<TrolleyItemValidator>();


builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRedis(builder.Configuration);
builder.Services.AddScoped<ITrolleyService, TrolleyService>();
builder.Services.AddScoped<IPromotionsService, PromotionsService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddTransient<IDiscountEngine,DiscountEngine>();
builder.Services.AddTransient<ITrolleyRepository, RedisTrolleyRepository>();

builder.Services.AddSingleton<IDiscountFactory,DiscountFactory>();


builder.Services.AddHttpClient("CatalogApi", client =>
{
    string basAddress = builder.Configuration.GetValue<string>("CatalogUrl");
    client.BaseAddress = new Uri(basAddress);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
})
.AddTransientHttpErrorPolicy(x =>
    x.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(300))

  )
.AddTransientHttpErrorPolicy(
        p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = false;
});
builder.Services.AddMessaging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.Run();
