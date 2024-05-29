namespace PokerHands.Models;
public class GameResults
{
    public required string GameResult { get; init; }
    public required IReadOnlyList<Player> PlayerResults { get; init; }
}
