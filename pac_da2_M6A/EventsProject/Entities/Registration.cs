public class Registration
{
    public int Id { get; set; } // Clave primaria
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime RegistrationDate { get; set; }

    public int EventId { get; set; }
    public Event? Event { get; set; } 
}