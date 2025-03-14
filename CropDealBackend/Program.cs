using CropDealBackend.Interfaces;
using CropDealBackend.Models;
using CropDealBackend.Repositories;
using CropDealBackend.Repository;
using CropDealBackend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System;

var builder = WebApplication.CreateBuilder(args);
// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()      // Log to console
    .WriteTo.File("Logs/myapp.log", rollingInterval: RollingInterval.Day) // Log to file with daily rolling
    .CreateLogger();
builder.Services.AddHttpClient("JWTMicroservice", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Microservices:JWTMicroservice"]);
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Main API", Version = "v1" });
});

// Replace default .NET Core logging with Serilog
builder.Host.UseSerilog();
// **1. Configure Database (Entity Framework Core - SQL Server)**
builder.Services.AddSignalR();
builder.Services.AddDbContext<CropDealContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<JWTMicroservice>();
builder.Services.AddScoped<INotification, NotificationRepository>();
builder.Services.AddScoped<ICropDetail, CropDetailRepository>();
builder.Services.AddScoped<IAddonType, AddOnTypeRepository>();
builder.Services.AddScoped<ICropType, CropTypeRepository>();
builder.Services.AddScoped<IUserDetail, UserDetailRepository>();
builder.Services.AddScoped<IAddOn, AddOnRepository>();
builder.Services.AddScoped<ITransactions, TransactionsRepository>();
builder.Services.AddScoped<ISubscription, SubscriptionRepository>();
builder.Services.AddScoped<IInvoice, InvoiceRepository>();
builder.Services.AddScoped<IBankDetail, BankDetailRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Main API V1");
        c.SwaggerEndpoint("https://localhost:7009/swagger/v1/swagger.json", "Authentication API"); // External API
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
