using PokerHands.Models.Enums;

namespace PokerHands.Models;
public class PlayerHand
{
    public IReadOnlyList<Card> Cards { get; private set; } = new List<Card>();

    public HandRank HandRank { get; private set; }

    public string HandRankDisplayName => Enum.GetName(typeof(HandRank), HandRank) ?? string.Empty;

    public static int GetMaxNumberOfCardsAllowedInHand()
    {
        return 5;
    }

    public void AddCardToHand(Card card)
    {
        var copiedCards = Cards.ToList();
        if(copiedCards.Count == 5)
        {
            throw new Exception($"PlayerHand: Cannot add more than {GetMaxNumberOfCardsAllowedInHand()} cards to hand.");
        }
        copiedCards.Add(card);
        Cards = copiedCards.AsReadOnly();
    }

    public Card GetHighCard()
    {
        var highCard = Card.GetDefaultCard();
        foreach(Card card in Cards)
        {
            if (card.Value <= highCard.Value) continue;
            highCard = card;
        }
        return highCard;
    }

    public void AssignHandRank(HandRank handRank) { 
        HandRank = handRank;
    }
}
