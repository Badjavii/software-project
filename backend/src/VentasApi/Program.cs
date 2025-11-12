using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VentasApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS: allow frontend during development to call the API
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowFrontendDev",
        policy =>
        {
            policy.AllowAnyHeader()
                  .AllowAnyMethod()
                  // Allow any origin for now (development). For production lock this down.
                  .AllowAnyOrigin();
        });
});

// Register the JSON data store as singleton
builder.Services.AddSingleton<JsonDataStore>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
// Use CORS policy
app.UseCors("AllowFrontendDev");

app.UseAuthorization();

app.MapControllers();

app.Run();

// Allow Program.cs to be testable
public partial class Program { }
