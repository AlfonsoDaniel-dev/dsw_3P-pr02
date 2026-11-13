namespace dsw_pr02.Services;

using dsw_pr02.Models;
using dsw_pr02.Repositories;

public interface IEventoService
{
    public List<Evento> ObtenerTodos();
    public Evento? ObtenerPorId(int id);
    public void Agregar(Evento evento);
    public void Actualizar(Evento evento);
    public void Eliminar(int id);
}
