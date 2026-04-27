using ClienteAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar la Base de Datos
builder.Services.AddDbContext<BdClientesContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("ClienteDB")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- CONFIGURACIÓN PARA CODESPACES / DOCKER ---

// Forzamos Swagger en cualquier entorno (Production/Development)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cliente API v1");
    c.RoutePrefix = "swagger"; // Esto asegura que la ruta sea /swagger
});

// COMENTAMOS ESTO: Es la causa principal del error 401/502 en Codespaces
// app.UseHttpsRedirection(); 

app.UseAuthorization(); // Solo por si acaso algún controlador lo requiere
app.MapControllers();

// Endpoint de prueba rápido para verificar si la API responde sin Swagger
app.MapGet("/", () => "API Funcionando correctamente en el puerto 8080");

app.Run();