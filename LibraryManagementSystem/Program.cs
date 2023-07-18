using AutoMapper;
using Library.Application;
using Library.Application.Mapper;
using Library.Infrastructure;
using Test.Infrastructure.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddInInfrastructureServices(builder.Configuration);
builder.Services.AddInApplicationServices(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mapperConfig = new MapperConfiguration(config =>
{
    config.AddProfile(new MapperProfile());
    // Add additional mappings as needed
});

IMapper mapper = mapperConfig.CreateMapper();
MapperHelper.Configure(mapper);
builder.Services.AddSingleton(mapper);

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
