
var builder = WebApplication.CreateBuilder(args);

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVue",
        policy => policy
            .WithOrigins("http://localhost:5173") // origen de tu frontend
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

builder.Services.AddControllers();

var app = builder.Build();

// Usar CORS antes de MapControllers
app.UseCors("AllowVue");

app.UseAuthorization();
app.MapControllers();
app.Run();