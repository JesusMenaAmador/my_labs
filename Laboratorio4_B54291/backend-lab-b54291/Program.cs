var builder = WebApplication.CreateBuilder(args);

// Agregar los servicios al contenedor
builder.Services.AddControllers();

// Configurar Swagger
builder.Services.AddSwaggerGen(); // Aquí se agrega Swagger para la documentación de la API

var app = builder.Build();

// Configuración del pipeline de la solicitud HTTP
if (app.Environment.IsDevelopment())
{
    // Habilitar Swagger y la UI para desarrollo
    app.UseSwagger(); // Genera la definición de la API
    app.UseSwaggerUI(); // Habilita la UI de Swagger
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
