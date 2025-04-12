var builder = WebApplication.CreateBuilder(args);

// Agregar los servicios al contenedor
builder.Services.AddControllers();

// Configurar Swagger
builder.Services.AddSwaggerGen(); // Aqu� se agrega Swagger para la documentaci�n de la API

var app = builder.Build();

// Configuraci�n del pipeline de la solicitud HTTP
if (app.Environment.IsDevelopment())
{
    // Habilitar Swagger y la UI para desarrollo
    app.UseSwagger(); // Genera la definici�n de la API
    app.UseSwaggerUI(); // Habilita la UI de Swagger
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
