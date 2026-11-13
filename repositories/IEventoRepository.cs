namespace dsw_pr02.Repositories;

using dsw_pr02.Models;

public interface IEventoRepository
{
    public List<Evento> ObtenerTodos();
    public Evento? ObtenerPorId(int id);
    public void Agregar(Evento evento);
    public void Actualizar(Evento evento);
    public void Eliminar(int id);
}
