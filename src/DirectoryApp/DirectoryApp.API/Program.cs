using DirectoryApp.API.Validator;
using DirectoryApp.Application;
using DirectoryApp.Application.ConfigurationModel;
using DirectoryApp.Infrastructure;
using DirectoryApp.Persistence;
using DirectoryApp.Persistence.Context;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(config =>
{
    config.RegisterValidatorsFromAssemblyContaining<CreateContactInformationCommandValidator>();
}); 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RedisConfiguration>(builder.Configuration.GetSection("RedisConfig"));
builder.Services.Configure<RabbitMQConfiguration>(builder.Configuration.GetSection("RabbitMQConfig"));

builder.Services.AddDbContext<ApplicationContext>(config => config.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnectionString")));

//Calling External Service Registrators
builder.Services.RegisterApplicationServices();
builder.Services.RegisterPersistenceServices();
builder.Services.RegisterInfrastructureServices();


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
