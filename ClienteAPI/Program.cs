using ClienteAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar la Base de Datos
builder.Services.AddDbContext<BdClientesContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ClienteDB")));

// 2. IMPORTANTE: Esto permite que la API use tus nuevos controladores
builder.Services.AddControllers();

// 3. IMPORTANTE: Esto genera la interfaz de Swagger que viste en la foto
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 4. Activar Swagger fuera del IF para que funcione en Docker
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

// 5. IMPORTANTE: Esto mapea las rutas de Clientes y Documentos
app.MapControllers();

// --- Código de ejemplo del Clima (Opcional) ---
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

// Definición para el código del clima
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}