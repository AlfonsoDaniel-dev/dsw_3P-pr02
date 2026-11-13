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

    public List<Evento> ObtenerTodos()
    {
        return _eventoRepository.ObtenerTodos();
    }

    public Evento? ObtenerPorId(int id)
    {
        return _eventoRepository.ObtenerPorId(id);
    }

    public void Agregar(Evento evento)
    {
        ArgumentNullException.ThrowIfNull(evento);

        Validar(evento);

        evento.Activo = true;

        _eventoRepository.Agregar(evento);
    }

    public void Actualizar(Evento evento)
    {
        ArgumentNullException.ThrowIfNull(evento);

        if (_eventoRepository.ObtenerPorId(evento.Id) == null)
        {
            throw new KeyNotFoundException($"No existe un evento con id {evento.Id}.");
        }

        _eventoRepository.Actualizar(evento);
    }

    public void Eliminar(int id)
    {
        var evento = _eventoRepository.ObtenerPorId(id);
        if (evento == null)
        {
            throw new KeyNotFoundException($"No existe un evento con id {id}.");
        }

        if (evento.Activo)
        {
            throw new InvalidOperationException("No se puede eliminar un evento activo. Desactívelo primero.");
        }

        _eventoRepository.Eliminar(id);
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
