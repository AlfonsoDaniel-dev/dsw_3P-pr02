namespace dsw_pr02.Data;

using Microsoft.EntityFrameworkCore;
using dsw_pr02.Models;

public class PostgreSqlDbContext : DbContext
{
    public PostgreSqlDbContext(DbContextOptions<PostgreSqlDbContext> options) : base(options)
    {
        
    }

    public DbSet<Evento> Eventos => Set<Evento>();

}
