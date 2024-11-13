using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Partida> Partidas { get; set; }
    public DbSet<Intento> Intentos { get; set; }
}