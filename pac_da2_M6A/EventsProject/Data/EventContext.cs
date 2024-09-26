using Microsoft.EntityFrameworkCore;

public class EventContext : DbContext
{
    public DbSet<Event> Events { get; set; }

    public EventContext(DbContextOptions<EventContext> options) : base(options)
    {
    }
}