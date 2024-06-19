using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using APIwebRBA.Data;
using APIwebRBA.Controllers;
var builder = WebApplication.CreateBuilder(args);

// Configuración del contexto de la base de datos
builder.Services.AddDbContext<APIwebRBAContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("APIwebRBAContext") ?? throw new InvalidOperationException("Connection string 'APIwebRBAContext' not found.")));

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Add services to the container.

builder.Services.AddControllers();

// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "APIwebRBA Burger", Version = "v1" });
});

var app = builder.Build();

// Configuración del pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIwebRBA Burger v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

// Aplicar CORS
app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();


// Mapeo de los endpoints minimalistas
app.MapBurgerEndpoints();

app.Run();

