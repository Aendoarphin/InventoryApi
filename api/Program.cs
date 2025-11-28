using api.Data;
using api.Interfaces;
using api.Repositories;
using Microsoft.EntityFrameworkCore;

string AllowReactClient = "_allowReactClient";
string[] AllowedOrigins = [
    "http://localhost:3000",
    "https://localhost:3000",
    "http://192.168.0.162:3000",
    "http://192.168.0.156:3000",
    "http://10.8.1.21:3000",
    "http://localhost:4173",
];

var builder = WebApplication.CreateBuilder(args);

// Get connection string from configuration
var connectionString = builder.Configuration["CONN_STR_DEV"]
    ?? throw new InvalidOperationException("Connection string 'CONN_STR_DEV' not found.");

// Add services to the container.
builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddScoped<IRecordValidationRepository, RecordValidationRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowReactClient, policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyHeader().AllowAnyMethod();
    });
});

// Swagger stuff
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(AllowReactClient);
app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapControllers();

app.Run();