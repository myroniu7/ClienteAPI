using ClienteAPI.Data;
using ClienteAPI.Data.Repositories;
using ClienteAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configuración completa de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Cliente API", 
        Version = "v1",
        Description = "API para gestión de clientes con SQL Server",
        Contact = new OpenApiContact
        {
            Name = "Tu Nombre",
            Email = "tu.email@dominio.com"
        }
    });
});

// Entity Framework Configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cliente API v1");
        c.RoutePrefix = "swagger"; // Esto hace que swagger esté en /swagger
        c.DocumentTitle = "Cliente API Documentation";
    });
}

app.UseCors("AllowAll");

app.UseRouting(); // ← Asegúrate de tener esto

app.UseAuthorization();

app.MapControllers();

// Mapear la ruta raíz a Swagger
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();