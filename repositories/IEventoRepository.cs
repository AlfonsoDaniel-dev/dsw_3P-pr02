namespace dsw_pr02.Repositories;

using dsw_pr02.Models;

public interface IEventoRepository
{
    public List<Evento> GetAll();
    public Evento? GetById(int id);
    public void Add(Evento evento);
    public void Update(Evento evento);
    public void Delete(int id);
}