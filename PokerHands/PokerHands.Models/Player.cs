namespace PokerHands.Models;
public class Player
{
    public required string Name { get; init; }
    public PlayerHand Hand { get; init; } = new PlayerHand();
}
