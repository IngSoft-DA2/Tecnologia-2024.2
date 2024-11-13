public class Partida
{
    public int Id { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public bool EstaFinalizada { get; set; }
    public int Intentos { get; set; }
    public string PosicionNicoFeliz { get; set; }
    public ICollection<Intento> IntentosPartida { get; set; }
    public string PlayerName { get; set; }

    // Nueva propiedad para almacenar el puntaje
    public int Score { get; set; }
}