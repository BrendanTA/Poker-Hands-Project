using PokerHands.Models.Enums;

namespace PokerHands.Models;
public class PokerDeck
{
    public static IReadOnlyList<Card> GetDeck()
    {
        var pokerDeck = new List<Card>();
        foreach (CardSuit cardSuit in Enum.GetValues(typeof(CardSuit)))
        {
            foreach (CardValue cardValue in Enum.GetValues(typeof(CardValue)))
            {
                pokerDeck.Add(new Card() { Suit = cardSuit, Value = cardValue });
            }
        }
        return pokerDeck.AsReadOnly();
    }
}
