using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class GamesController : ControllerBase
{
    private readonly AppDbContext _context;
    private const int MaxAttempts = 10;
    private readonly string[] ValidColumns = { "A", "B", "C", "D", "E", "F" };
    private const int MaxRows = 6;

    public GamesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult> StartGame([FromBody] StartGameDto startGameDto)
    {
        var game = new Partida
        {
            FechaInicio = DateTime.UtcNow,
            EstaFinalizada = false,
            Intentos = 0,
            PosicionNicoFeliz = GenerateRandomPosition(),
            PlayerName = startGameDto.PlayerName
        };

        _context.Partidas.Add(game);
        await _context.SaveChangesAsync();

        var columns = ValidColumns;
        var rows = Enumerable.Range(1, MaxRows).ToArray();

        return CreatedAtAction(nameof(StartGame), new
        {
            id = game.Id,
            columns = columns,
            rows = rows,
            maxAttempts = MaxAttempts
        });
    }

    private string GenerateRandomPosition()
    {
        var randomColumn = ValidColumns[new Random().Next(ValidColumns.Length)];
        var randomRow = new Random().Next(1, MaxRows + 1);
        return $"{randomColumn}{randomRow}";
    }

    [HttpPost("{gameId}/attempts")]
    public async Task<ActionResult> RegisterAttempt(int gameId, [FromBody] AttemptDto attemptDto)
    {
        var game = await _context.Partidas.FindAsync(gameId);
        if (game == null || game.EstaFinalizada)
            return NotFound("Game not found or already finished.");

        if (!ValidColumns.Contains(attemptDto.Column) || attemptDto.Row < 1 || attemptDto.Row > MaxRows)
        {
            return BadRequest(new
            {
                message = "Invalid attempt. The cell does not exist on the board."
            });
        }

        var attempt = new Intento
        {
            PartidaId = gameId,
            PosicionLetra = attemptDto.Column,
            PosicionNumero = attemptDto.Row,
            EsNicoFeliz = $"{attemptDto.Column}{attemptDto.Row}" == game.PosicionNicoFeliz
        };

        game.Intentos++;

        if (attempt.EsNicoFeliz)
        {
            game.EstaFinalizada = true;
            game.FechaFin = DateTime.UtcNow;
            game.Score = CalculateScore(game.Intentos, true);
            _context.Intentos.Add(attempt);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                isNicoFeliz = true,
                message = "You found Nico Feliz! The game is now finished.",
                imageUrl = "https://i.ibb.co/LtKsXgM/image.png",
                score = game.Score
            });
        }

        if (game.Intentos >= MaxAttempts)
        {
            game.EstaFinalizada = true;
            game.FechaFin = DateTime.UtcNow;
            game.Score = CalculateScore(game.Intentos, false);
            _context.Intentos.Add(attempt);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                isNicoFeliz = false,
                message = "Maximum attempts reached. The game is now finished.",
                imageUrl = "https://i.ibb.co/DYqdMvn/nicoenojado.jpg",
                score = game.Score
            });
        }

        _context.Intentos.Add(attempt);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            isNicoFeliz = false,
            message = "Keep trying!",
            imageUrl = "https://i.ibb.co/DYqdMvn/nicoenojado.jpg"
        });
    }

    private int CalculateScore(int attempts, bool foundNico)
    {
        int baseScore = 1000;
        int attemptPenalty = 100;

        if (foundNico)
        {
            return baseScore - (attempts * attemptPenalty);
        }
        else
        {
            return baseScore / 2 - (attempts * attemptPenalty);
        }
    }

    [HttpGet("ranking")]
    public async Task<ActionResult<IEnumerable<object>>> GetRanking()
    {
        var ranking = await _context.Partidas
            .Where(game => game.EstaFinalizada)
            .OrderByDescending(game => game.Score)
            .Take(10)
            .Select(game => new
            {
                playerName = game.PlayerName,
                score = game.Score,
                attempts = game.Intentos,
                date = game.FechaFin
            })
            .ToListAsync();

        return Ok(ranking);
    }

    [HttpDelete("{gameId}")]
    public async Task<IActionResult> FinishGame(int gameId)
    {
        var game = await _context.Partidas.FindAsync(gameId);
        if (game == null)
            return NotFound();

        game.EstaFinalizada = true;
        game.FechaFin = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        var message = game.PosicionNicoFeliz == null ?
            "The game is finished. You lost by not finding Nico Feliz." :
            "The game is finished. You won by finding Nico Feliz!";

        return Ok(new
        {
            message = message,
            attempts = game.Intentos
        });
    }

    [HttpGet("{gameId}/nico-position")]
    public async Task<ActionResult> GetNicoPosition(int gameId)
    {
        var game = await _context.Partidas.FindAsync(gameId);
        if (game == null)
            return NotFound(new { message = "Game not found." });

        return Ok(new
        {
            gameId = game.Id,
            nicoPosition = game.PosicionNicoFeliz
        });
    }

    [HttpGet("by-player/{playerName}")]
    public async Task<ActionResult<IEnumerable<object>>> GetGamesByPlayer(string playerName)
    {
        var games = await _context.Partidas
            .Where(game => game.PlayerName == playerName)
            .Select(game => new
            {
                gameId = game.Id,
                playerName = game.PlayerName,
                startDate = game.FechaInicio,
                endDate = game.FechaFin,
                attempts = game.Intentos,
                isFinished = game.EstaFinalizada,
                score = game.Score
            })
            .ToListAsync();

        if (!games.Any())
        {
            return NotFound(new { message = "No games found for the specified player." });
        }

        return Ok(games);
    }
}