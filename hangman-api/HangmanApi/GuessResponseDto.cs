public class GuessResponseDto
{
    public string GuessedLetters { get; set; }
    public string IncorrectLetters { get; set; }
    public int AttemptsLeft { get; set; }
    public bool IsFinished { get; set; }
    public bool IsWon { get; set; }
}