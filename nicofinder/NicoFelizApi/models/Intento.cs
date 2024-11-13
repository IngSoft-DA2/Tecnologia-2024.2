public class Intento
{
    public int Id { get; set; }
    public int PartidaId { get; set; }
    public Partida Partida { get; set; }
    public string PosicionLetra { get; set; }
    public int PosicionNumero { get; set; }
    public bool EsNicoFeliz { get; set; }
}