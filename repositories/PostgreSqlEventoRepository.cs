namespace dsw_pr02.Repositories;

using Microsoft.EntityFrameworkCore;
using dsw_pr02.Models;
using dsw_pr02.Data;

public class EventoPostgreSqlRepository : IEventoRepository
{
    private readonly PostgreSqlDbContext _context;

    public EventoPostgreSqlRepository(PostgreSqlDbContext context)
    {
        _context = context;
    }

    public List<Evento> ObtenerTodos()
    {
        return _context.Eventos.AsNoTracking().ToList();
    }

    public Evento? ObtenerPorId(int id)
    {
        return _context.Eventos.AsNoTracking().FirstOrDefault(e => e.Id == id);
    }

    public void Agregar(Evento evento)
    {
        evento.Id = 0;

        _context.Eventos.Add(evento);
        _context.SaveChanges();
    }

    public void Actualizar(Evento evento)
    {
        var existingEvento = _context.Eventos.Find(evento.Id);
        if (existingEvento == null)
        {
            return;
        }

        existingEvento.Nombre = evento.Nombre;
        existingEvento.Categoria = evento.Categoria;
        existingEvento.Lugar = evento.Lugar;
        existingEvento.NumeroMaximo = evento.NumeroMaximo;
        existingEvento.Activo = evento.Activo;

        _context.SaveChanges();
    }

    public void Eliminar(int id)
    {
        var evento = _context.Eventos.Find(id);
        if (evento == null)
        {
            return;
        }

        _context.Eventos.Remove(evento);
        _context.SaveChanges();
    }
}
