namespace dsw_pr02.Data;

using Microsoft.EntityFrameworkCore;
using dsw_pr02.Models;

public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
    {
        
    }

    public DbSet<Evento> Eventos => Set<Evento>();
        }
