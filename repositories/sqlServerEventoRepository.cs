namespace dsw_pr02.Repositories;

using dsw_pr02.Models;
using dsw_pr02.Data;

public class EventoSqlRepository : IEventoRepository
{

    private readonly AppDBContext _context;

    public EventoSqlRepository(AppDBContext context)
    {
        _context = context;
    }

    public List<Evento> GetAll()
    {
        return _context.Eventos.ToList();
    }

    public Evento? GetById(int id)
    {
        return _context.Eventos.Find(id);
    }

    public void Add(Evento evento)
    {
        evento.Id = 0;

        _context.Eventos.Add(evento);
        _context.SaveChanges();
    }

    public void Update(Evento evento)
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

    public void Delete(int id)
    {
        var evento = _context.Eventos.Find(id);
        if (evento == null)
        {
            return;
        }

        _context.Eventos.Remove(evento);
        _context.SaveChanges();
    }

    public List<Evento> GetByCategoria(string categoria)
    {
        return _context.Eventos
            .Where(e => e.Categoria == categoria)
            .ToList();
    }
}
