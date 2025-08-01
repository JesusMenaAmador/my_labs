using backend_expendedora.Application.Handlers;
using backend_expendedora.Services;

var MyallowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name:MyallowSpecificOrigins, policy =>
        policy
            .WithOrigins("http://localhost:8080", "https://localhost:8080")
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPagoService, PagoService>();
builder.Services.AddScoped<IConfirmarPagoService, ConfirmarPagoService>();
builder.Services.AddScoped<ConfirmarPagoHandler>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyallowSpecificOrigins);

app.UseAuthorization();
app.MapControllers();

app.Run();
