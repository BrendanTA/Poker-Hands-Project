using PokerHands.Models.Enums;

namespace PokerHands.Models;
public class Card
{
    public required CardSuit Suit { get; init; }
    public required CardValue Value { get; init; }
    public string CardDisplayName => $"{Enum.GetName(typeof(CardValue), Value) ?? string.Empty} {Enum.GetName(typeof(CardSuit), Suit) ?? string.Empty}";
    public static Card GetDefaultCard()
    {
        return new Card() { Suit = 0, Value = 0 };
    }
}
