namespace dsw_pr02.Services;

using dsw_pr02.Models;
using dsw_pr02.Repositories;

public interface IEventoService
{
    public List<Evento> GetAll();
    public Evento? GetById(int id);
    public void Add(Evento evento);
    public void Update(Evento evento);
    public void Delete(int id);
}