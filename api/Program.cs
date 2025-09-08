using api.Data;
using dotenv.net;
using Microsoft.EntityFrameworkCore;

DotEnv.Load();
var envVars = DotEnv.Read();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<InventoryDbContext>(options => options.UseSqlServer(envVars["CONN_STR_DEV"]));
builder.Services.AddControllers();
// Swagger stuff
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

// Routes

app.Run();
