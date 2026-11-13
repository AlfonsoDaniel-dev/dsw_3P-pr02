using Microsoft.EntityFrameworkCore;
using dsw_pr02.Data;
using dsw_pr02.Repositories;
using dsw_pr02.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Swagger (Swashbuckle) con la documentación XML de los controladores
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Title = "API de Eventos",
        Version = "v1",
        Description = "API para gestionar eventos: consulta, alta, modificación y eliminación."
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
});

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IEventoRepository, EventoSqlRepository>();
builder.Services.AddScoped<IEventoService, EventoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Eventos v1");
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
