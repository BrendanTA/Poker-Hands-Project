using PokerHands.Models.Enums;

namespace PokerHands.Models;
public class PlayerHand
{
    public IReadOnlyList<Card> Cards { get; private set; } = new List<Card>();

    public HandRank HandRank { get; private set; }

    public string HandRankDisplayName => Enum.GetName(typeof(HandRank), HandRank) ?? string.Empty;

    public const int GetMaxNumberOfCardsAllowedInHand = 5;

    public void AddCardsToHand(List<Card> cards)
    {
        var copiedCards = Cards.ToList();

        copiedCards.AddRange(cards);
        ValidateCardCount(copiedCards.Count);

        Cards = copiedCards.AsReadOnly();
    }

    public void AddCardToHand(Card card)
    {
        var copiedCards = Cards.ToList();

        copiedCards.Add(card);
        ValidateCardCount(copiedCards.Count);

        Cards = copiedCards.AsReadOnly();
    }

    public void AssignHandRank(HandRank handRank) { 
        HandRank = handRank;
    }

    private void ValidateCardCount(int cardCount)
    {
        if (cardCount > GetMaxNumberOfCardsAllowedInHand)
        {
            throw new Exception($"PlayerHand: Cannot add more than {GetMaxNumberOfCardsAllowedInHand} cards to hand.");
        }
    }
}
