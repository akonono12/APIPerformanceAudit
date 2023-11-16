using APIPerformanceAudit.Domain.Common;
using APIPerformanceAudit.Domain.UrlManagement.Interfaces;
using APIPerformanceAudit.Domain.UrlManagement.Repositories;
using APIPerformanceAudit.Infrastructure.UrlAuditInfra.Services;
using FluentValidation;
using MediatR;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

builder.Services.Configure<UrlAuditSetting>(config.GetSection("UrlAuditSetting"));

builder.Services.AddSingleton<IUrlRepository,UrlRepository>();
builder.Services.AddSingleton<UrlAuditInitializerService>(); 
builder.Services.AddHostedService<UrlAuditProcessingService>();

var assembly = AppDomain.CurrentDomain.Load("APIPerformanceAudit.Application");
builder.Services.AddMediatR(assembly);
builder.Services.AddValidatorsFromAssembly(assembly);

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
