using Microsoft.EntityFrameworkCore;
using ReportGenerator.BackgroundService;
using ReportGenerator.Config;
using ReportGenerator.Context;
using ReportGenerator.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<GenerateReportService>();
builder.Services.Configure<RabbitMQConfiguration>(builder.Configuration.GetSection("RabbitMQConfig"));
builder.Services.AddDbContext<ReportGeneratorContext>(config => config.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnectionString")), ServiceLifetime.Singleton);


builder.Services.AddHostedService<GenerateReportBackgroundService>();

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
