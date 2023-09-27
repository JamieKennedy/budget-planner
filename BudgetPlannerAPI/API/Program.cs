using System.Text.Json.Serialization;

using API.Extensions;
using API.Middleware;

using Common;
using Common.Constants;

using LoggerService.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Db Context
builder.Services.ConfigureSqlContext(builder.Configuration);

// Logger
builder.Services.ConfigureLoggerManager();

// Cors
builder.Services.ConfigureCorsPolicy(builder.Configuration);

// Identity
builder.Services.ConfigureIdentity();

// Auth
builder.Services.AddAuthentication();
builder.Services.ConfigureJwt(builder.Configuration);

// Managers
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();

// Auto Mapper
builder.Services.AddSingleton(MappingProfile.CreateMapper());

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
}); ;
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