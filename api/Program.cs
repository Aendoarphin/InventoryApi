using Api.Data;
using Api.Interfaces;
using Api.Repositories;
using Api.Services;
using Microsoft.EntityFrameworkCore;

string AllowReactClient = "_allowReactClient";

var builder = WebApplication.CreateBuilder(args);

// Get connection string from configuration
var connectionString = builder.Configuration["CONN_STR_DEV"]
    ?? throw new InvalidOperationException("Connection string 'CONN_STR_DEV' not found.");
// Allowed origins for CORS
var allowedOrigins = builder.Configuration["ALLOWED_ORIGINS"]
    ?? throw new InvalidOperationException("Allowed origins 'ALLOWED_ORIGINS' not found.");

// Add services to the container.
builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddScoped<IRecordValidationRepository, RecordValidationRepository>();
builder.Services.AddScoped<INetworkService, NetworkService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowReactClient, policy =>
    {
        policy.WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// **************   Swagger   ******************
builder.Services.AddEndpointsApiExplorer(); // *
builder.Services.AddSwaggerGen();           // *
// *********************************************

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(AllowReactClient);
app.UseHttpsRedirection();
app.MapControllers();

app.MapGet("/", () => "Inventory API v1.2.1 (5)");

app.Run();