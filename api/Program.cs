using api.Data;
using api.Interfaces;
using api.Repositories;
using dotenv.net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

DotEnv.Load();
var envVars = DotEnv.Read();

string AllowReactClient = "_allowReactClient";
string[] AllowedOrigins = [
    "http://localhost:3000",
    "https://localhost:3000",
    "http://192.168.0.162:3000",
    "http://192.168.0.156:3000",
    "http://10.8.1.21:3000"
];

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<InventoryDbContext>(options => options.UseSqlServer(envVars["CONN_STR_DEV"]));
builder.Services.AddControllers();
builder.Services.AddScoped<IRecordValidationRepository, RecordValidationRepository>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowReactClient, policy =>
    {
        policy.WithOrigins(AllowedOrigins)
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
app.MapControllers();

// Routes

app.Run();
