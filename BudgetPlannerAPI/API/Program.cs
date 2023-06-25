using API.Extensions;
using API.Middleware;

using Common.Constants;
using Common.Models;

using LoggerService.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Db Context
builder.Services.ConfigureSqlContext(builder.Configuration);

// Logger
builder.Services.ConfigureLoggerManager();

// Managers
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();

// Auto Mapper
builder.Services.AddSingleton(MappingProfile.CreateMapper());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Cors
var corsPolicy = app.Configuration.GetSection(ConfigurationConst.Cors.POLICY_SECTION)[ConfigurationConst.Cors.POLICY_NAME];
if (corsPolicy is not null)
{
    app.UseCors(corsPolicy);
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();