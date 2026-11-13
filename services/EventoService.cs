namespace dsw_pr02.Services;

using dsw_pr02.Models;
using dsw_pr02.Repositories;

public class EventoService : IEventoService
{
    private readonly IEventoRepository _eventoRepository;

    public EventoService(IEventoRepository eventoRepository)
    {
        _eventoRepository = eventoRepository;
    }

    public List<Evento> GetAll()
    {
        return _eventoRepository.GetAll();
    }

    public Evento? GetById(int id)
    {
        return _eventoRepository.GetById(id);
    }

    public void Add(Evento evento)
    {
        ArgumentNullException.ThrowIfNull(evento);

        Validar(evento);

        evento.Activo = true;

        _eventoRepository.Add(evento);
    }

    public void Update(Evento evento)
    {
        ArgumentNullException.ThrowIfNull(evento);

        if (_eventoRepository.GetById(evento.Id) == null)
        {
            throw new KeyNotFoundException($"No existe un evento con id {evento.Id}.");
        }


        _eventoRepository.Update(evento);
    }

    public void Delete(int id)
    {
        var evento = _eventoRepository.GetById(id);
        if (evento == null)
        {
            throw new KeyNotFoundException($"No existe un evento con id {id}.");
        }

        if (evento.Activo)
        {
            throw new InvalidOperationException("No se puede eliminar un evento activo. Desactívelo primero.");
        }

        _eventoRepository.Delete(id);
    }

    private static void Validar(Evento evento)
    {
        if (string.IsNullOrWhiteSpace(evento.Nombre))
        {
            throw new ArgumentException("El nombre del evento es obligatorio.", nameof(evento));
        }

        if (string.IsNullOrWhiteSpace(evento.Categoria))
        {
            throw new ArgumentException("La categoría del evento es obligatoria.", nameof(evento));
        }

        if (string.IsNullOrWhiteSpace(evento.Lugar))
        {
            throw new ArgumentException("El lugar del evento es obligatorio.", nameof(evento));
        }

        if (evento.NumeroMaximo <= 0)
        {
            throw new ArgumentException("El número máximo de asistentes debe ser mayor que cero.", nameof(evento));
        }
    }


}
