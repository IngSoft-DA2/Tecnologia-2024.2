public interface IEventService
{
    void CreateEvent(Event eventEntity);
    Event? GetEventById(int id);
    IEnumerable<Event> GetAllEvents();
    void UpdateEvent(Event eventEntity);
    void DeleteEvent(int id);
    void RegisterUserToEvent(int eventId, string firstName, string lastName);
    void CancelRegistration(int eventId, string firstName, string lastName);
    int GetAvailableSlots(int eventId);
}