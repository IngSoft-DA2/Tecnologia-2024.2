using System.ComponentModel.DataAnnotations;

public class Game
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string PlayerName { get; set; }
    [Required]
    public string Word { get; set; } // Palabra que se debe adivinar
    [Required]
    public string GuessedLetters { get; set; } // Letras acertadas
    public string IncorrectLetters { get; set; } // Letras incorrectas
    [Required]
    public int AttemptsLeft { get; set; } // Intentos restantes
    public bool IsFinished { get; set; } // Indica si el juego ha terminado
    public bool IsWon { get; set; } // Indica si el juego fue ganado
}