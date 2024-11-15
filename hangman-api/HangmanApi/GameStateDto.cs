public class GameStateDto
{
    public int GameId { get; set; }
    public string PlayerName { get; set; }
    public string GuessedLetters { get; set; }
    public string IncorrectLetters { get; set; }
    public int AttemptsLeft { get; set; }
    public bool IsFinished { get; set; }
    public bool IsWon { get; set; }
    public string Word { get; set; } // Nueva propiedad
}