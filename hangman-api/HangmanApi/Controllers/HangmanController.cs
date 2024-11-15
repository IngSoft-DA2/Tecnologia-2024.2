using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

[Route("api/[controller]")]
[ApiController]
public class HangmanController : ControllerBase
{
    private readonly AppDbContext _context;
    private const int MaxAttempts = 10;

    public HangmanController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("start")]
    public async Task<ActionResult<StartGameResponseDto>> StartGame([FromBody] StartGameDto startGameDto)
    {
        if (string.IsNullOrWhiteSpace(startGameDto.PlayerName))
        {
            return BadRequest(new { message = "The playerName field is required." });
        }

        var randomWord = await GenerateRandomWordAsync();
        var game = new Game
        {
            PlayerName = startGameDto.PlayerName,
            Word = randomWord,
            AttemptsLeft = MaxAttempts,
            GuessedLetters = new string('_', randomWord.Length),
            IncorrectLetters = string.Empty,
            IsFinished = false,
            IsWon = false
        };

        _context.Games.Add(game);
        await _context.SaveChangesAsync();

        var response = new StartGameResponseDto
        {
            Id = game.Id,
            WordLength = randomWord.Length,
            AttemptsLeft = game.AttemptsLeft
        };

        return Ok(response);
    }

    [HttpPost("{gameId}/guess")]
    public async Task<ActionResult<GuessResponseDto>> MakeGuess(int gameId, [FromBody] GuessDto guessDto)
    {
        if (string.IsNullOrWhiteSpace(guessDto.Letter) || guessDto.Letter.Length > 1)
        {
            return BadRequest(new { message = "You must provide a single letter." });
        }

        var game = await _context.Games.FindAsync(gameId);
        if (game == null || game.IsFinished)
        {
            return NotFound(new { message = "Game not found or already finished." });
        }

        string letter = guessDto.Letter;

        if (game.Word.Contains(letter, StringComparison.OrdinalIgnoreCase))
        {
            var indices = Enumerable.Range(0, game.Word.Length)
                                    .Where(i => game.Word[i].ToString().Equals(letter, StringComparison.OrdinalIgnoreCase))
                                    .ToArray();

            foreach (var index in indices)
            {
                var guessedArray = game.GuessedLetters.ToCharArray();
                guessedArray[index] = game.Word[index];
                game.GuessedLetters = new string(guessedArray);
            }

            if (!game.GuessedLetters.Contains('_'))
            {
                game.IsFinished = true;
                game.IsWon = true;
            }
        }
        else
        {
            if (!game.IncorrectLetters.Contains(letter, StringComparison.OrdinalIgnoreCase))
            {
                game.IncorrectLetters += letter;
                game.AttemptsLeft--;
            }

            if (game.AttemptsLeft <= 0)
            {
                game.IsFinished = true;
                game.IsWon = false;
            }
        }

        await _context.SaveChangesAsync();

        var response = new GuessResponseDto
        {
            GuessedLetters = game.GuessedLetters,
            IncorrectLetters = game.IncorrectLetters,
            AttemptsLeft = game.AttemptsLeft,
            IsFinished = game.IsFinished,
            IsWon = game.IsWon
        };

        return Ok(response);
    }

[HttpGet("{gameId}")]
public async Task<ActionResult<GameStateDto>> GetGame(int gameId)
{
    var game = await _context.Games.FindAsync(gameId);
    if (game == null)
    {
        return NotFound(new { message = "Game not found." });
    }

    var response = new GameStateDto
    {
        GameId = game.Id,
        PlayerName = game.PlayerName,
        GuessedLetters = game.GuessedLetters,
        IncorrectLetters = game.IncorrectLetters,
        AttemptsLeft = game.AttemptsLeft,
        IsFinished = game.IsFinished,
        IsWon = game.IsWon,
        Word = game.Word,

    };

    return Ok(response);
}

    [HttpGet("ranking")]
    public async Task<ActionResult<IEnumerable<RankingEntryDto>>> GetRanking()
    {
        var ranking = await _context.Games
            .Where(g => g.IsFinished && g.IsWon)
            .OrderByDescending(g => g.AttemptsLeft)
            .Select(g => new RankingEntryDto
            {
                PlayerName = g.PlayerName,
                AttemptsLeft = g.AttemptsLeft
            })
            .ToListAsync();

        return Ok(ranking);
    }

private async Task<string> GenerateRandomWordAsync()
{
    using (var httpClient = new HttpClient())
    {
        try
        {
            Console.WriteLine("Iniciando solicitud a la API de palabras aleatorias...");

            // Solicitar una palabra aleatoria en español desde la API
            var response = await httpClient.GetStringAsync("https://clientes.api.greenborn.com.ar/public-random-word");

            // Log para verificar la respuesta en bruto
            Console.WriteLine($"Respuesta recibida de la API: {response}");

            // Deserializar la respuesta como un arreglo de strings
            var words = JsonSerializer.Deserialize<string[]>(response);

            // Validar si el arreglo tiene elementos y retornar el primero
            if (words != null && words.Length > 0)
            {
                var word = words[0];
                Console.WriteLine($"Palabra obtenida: {word}");
                return word;
            }

            Console.WriteLine("El arreglo de palabras está vacío o es nulo.");
            return "default";
        }
        catch (Exception ex)
        {
            // Loggear el error y retornar una palabra predeterminada
            Console.WriteLine($"Error al obtener palabra aleatoria: {ex.Message}");
            return "default";
        }
    }
}

}