using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using VintageRugsApi.Data;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

const string connectionStringEnvVariableName = "DATABASE_CONNECTION_STRING";

// var serverVersion = new MySqlServerVersion(new Version(10, 4, 22));
var connectionString = Environment.GetEnvironmentVariable(connectionStringEnvVariableName);
if (connectionString == null)
{
    throw new ArgumentNullException(connectionString);
}

builder.Services.AddDbContext<VintageRugsDbContext>(opt =>
{
    opt.UseNpgsql(connectionString);
    if (builder.Environment.IsDevelopment())
    {
        opt
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }
});

builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

builder.Services.AddControllers()
    .AddJsonOptions(configure =>
    {
        configure.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        configure.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // handle circular references
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
