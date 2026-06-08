using AcademicPlatformApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la conexión a MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AcademicoDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Definimos los documentos individuales para el filtrado en Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1-todos", new OpenApiInfo { Title = "API - Todo el Sistema", Version = "v1" });
    c.SwaggerDoc("v1-estudiantes", new OpenApiInfo { Title = "Módulo - Estudiantes", Version = "v1" });
    c.SwaggerDoc("v1-docentes", new OpenApiInfo { Title = "Módulo - Docentes", Version = "v1" });
    c.SwaggerDoc("v1-cursos", new OpenApiInfo { Title = "Módulo - Cursos", Version = "v1" });
    c.SwaggerDoc("v1-horarios", new OpenApiInfo { Title = "Módulo - Horarios", Version = "v1" });
    c.SwaggerDoc("v1-matriculas", new OpenApiInfo { Title = "Módulo - Matrículas", Version = "v1" });

    // Criterio de asignación de rutas a cada grupo basado en el nombre del controlador
    c.DocInclusionPredicate((docName, apiDesc) =>
    {
        if (docName == "v1-todos") return true;

        var controllerName = apiDesc.ActionDescriptor.RouteValues["controller"]?.ToLower();
        return docName switch
        {
            "v1-estudiantes" => controllerName == "estudiantes",
            "v1-docentes" => controllerName == "docentes",
            "v1-cursos" => controllerName == "cursos",
            "v1-horarios" => controllerName == "horarios",
            "v1-matriculas" => controllerName == "matriculas",
            _ => false
        };
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    // Registramos los endpoints de la interfaz para que aparezcan en el menú desplegable
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1-todos/swagger.json", "Todo el Sistema");
        c.SwaggerEndpoint("/swagger/v1-estudiantes/swagger.json", "Solo Estudiantes");
        c.SwaggerEndpoint("/swagger/v1-docentes/swagger.json", "Solo Docentes");
        c.SwaggerEndpoint("/swagger/v1-cursos/swagger.json", "Solo Cursos");
        c.SwaggerEndpoint("/swagger/v1-horarios/swagger.json", "Solo Horarios");
        c.SwaggerEndpoint("/swagger/v1-matriculas/swagger.json", "Solo Matrículas");
    });
}

app.UseAuthorization();
app.MapControllers();

app.Run();