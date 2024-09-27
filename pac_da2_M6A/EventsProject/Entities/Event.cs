public class Event
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime Date { get; set; }
    public string? Location { get; set; }
    public int Capacity { get; set; }
    public int AvailableSlots { get; set; }

    public List<Registration> Registrations { get; set; } = new List<Registration>();
}