using Microsoft.EntityFrameworkCore;
using dsw_pr02.Data;
using dsw_pr02.Repositories;
using dsw_pr02.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Base de datos (SQL Server)
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Inyección de dependencias: repositorio y servicio de Evento
builder.Services.AddScoped<IEventoRepository, EventoSqlRepository>();
builder.Services.AddScoped<IEventoService, EventoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
