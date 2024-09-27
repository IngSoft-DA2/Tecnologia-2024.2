public class EventService : IEventService
{
    private readonly EventContext _context;

    public EventService(EventContext context)
    {
        _context = context;
    }

    public void CreateEvent(Event eventEntity)
    {
        if (eventEntity == null)
            throw new ArgumentNullException(nameof(eventEntity), "El evento no puede ser nulo.");

        if (string.IsNullOrWhiteSpace(eventEntity.Name))
            throw new ArgumentException("El nombre del evento no puede estar vacío.");

        _context.Events.Add(eventEntity);
        _context.SaveChanges();
    }

    public Event? GetEventById(int id)
    {
        var eventEntity = _context.Events.FirstOrDefault(e => e.Id == id);
        if (eventEntity == null)
            throw new InvalidOperationException($"No se encontró un evento con el ID {id}.");
        return eventEntity;
    }

    public IEnumerable<Event> GetAllEvents()
    {
        return _context.Events.ToList();
    }

    public void UpdateEvent(Event eventEntity)
    {
        if (eventEntity == null)
            throw new ArgumentNullException(nameof(eventEntity), "El evento no puede ser nulo.");

        if (string.IsNullOrWhiteSpace(eventEntity.Name))
            throw new ArgumentException("El nombre del evento no puede estar vacío.");

        _context.Events.Update(eventEntity);
        _context.SaveChanges();
    }

    public void DeleteEvent(int id)
    {
        var eventToDelete = _context.Events.Find(id);
        if (eventToDelete == null)
            throw new InvalidOperationException($"No se encontró un evento con el ID {id} para eliminar.");

        _context.Events.Remove(eventToDelete);
        _context.SaveChanges();
    }

    public void RegisterUserToEvent(int eventId, string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("El nombre y apellido no pueden estar vacíos.");

        var eventEntity = _context.Events.Find(eventId);
        if (eventEntity == null)
            throw new InvalidOperationException($"No se encontró un evento con el ID {eventId}.");

        if (eventEntity.AvailableSlots <= 0)
            throw new InvalidOperationException("No hay cupos disponibles para este evento.");

        if (eventEntity.Registrations.Any(r => r.FirstName == firstName && r.LastName == lastName))
            throw new InvalidOperationException($"El usuario {firstName} {lastName} ya está registrado en el evento.");

        var registration = new Registration
        {
            FirstName = firstName,
            LastName = lastName,
            RegistrationDate = DateTime.Now
        };

        eventEntity.Registrations.Add(registration);
        eventEntity.AvailableSlots--;
        _context.SaveChanges();
    }

    public void CancelRegistration(int eventId, string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("El nombre y apellido no pueden estar vacíos.");

        var eventEntity = _context.Events.Find(eventId);
        if (eventEntity == null)
            throw new InvalidOperationException($"No se encontró un evento con el ID {eventId}.");

        var registration = eventEntity.Registrations
            .FirstOrDefault(r => r.FirstName == firstName && r.LastName == lastName);

        if (registration == null)
            throw new InvalidOperationException($"El usuario {firstName} {lastName} no está registrado en el evento.");

        eventEntity.Registrations.Remove(registration);
        eventEntity.AvailableSlots++;
        _context.SaveChanges();
    }

    public int GetAvailableSlots(int eventId)
    {
        var eventEntity = _context.Events.Find(eventId);
        if (eventEntity == null)
            throw new InvalidOperationException($"No se encontró un evento con el ID {eventId}.");
        return eventEntity.AvailableSlots;
    }

    public bool IsAuthenticated(string token)
    {
        return !string.IsNullOrWhiteSpace(token);
    }
}