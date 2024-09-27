using Microsoft.EntityFrameworkCore;

public class EventContext : DbContext
{
    public DbSet<Event> Events { get; set; }
    public DbSet<Registration> Registrations { get; set; }

    public EventContext(DbContextOptions<EventContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurar la relación uno a muchos entre Event y Registration
        modelBuilder.Entity<Event>()
            .HasMany(e => e.Registrations)
            .WithOne(r => r.Event)
            .HasForeignKey(r => r.EventId);

        base.OnModelCreating(modelBuilder);
    }
}