using PokerHands.Models;
using PokerHands.Models.Enums;
using PokerHands.Services.Interfaces;

namespace PokerHands.Services.CoreServices;
public class RankHandService : IRankHandService
{
    public void RankPlayerHands(List<Player> players)
    {
        foreach(var player in players)
        {
            player.Hand.AssignHandRank(EvaluateHand(player.Hand));
        }
    }

    public HandRank EvaluateHand(PlayerHand playerHand)
    {
        //This could be optimized to not loop as much, but considering the hand size is 5, there shouldn't be an issue here.
        var cardsGroupedByValue = playerHand.Cards.GroupBy(card => card.Value);
        var cardsGroupedBySuit = playerHand.Cards.GroupBy(card => card.Suit);

        var isFlush = cardsGroupedBySuit.Any(group => group.Count() == 5);
        var isStraight = IsStraight(cardsGroupedByValue.Select(group => group.Key));

        if (isStraight && isFlush)
            return HandRank.StraightFlush;

        if (cardsGroupedByValue.Any(group => group.Count() == 4))
            return HandRank.FourOfAKind;

        if (cardsGroupedByValue.Any(group => group.Count() == 3) && cardsGroupedByValue.Any(group => group.Count() == 2))
            return HandRank.FullHouse;

        if (isFlush)
            return HandRank.Flush;

        if (isStraight)
            return HandRank.Straight;

        if (cardsGroupedByValue.Any(group => group.Count() == 3))
            return HandRank.ThreeOfAKind;

        if (cardsGroupedByValue.Count(group => group.Count() == 2) == 2)
            return HandRank.TwoPairs;

        if (cardsGroupedByValue.Any(group => group.Count() == 2))
            return HandRank.Pair;

        return HandRank.HighCard;
    }

    private static bool IsStraight(IEnumerable<CardValue> cardValues)
    {
        var sortedCardValues = cardValues.OrderBy(v => v).ToList();
        for (int i = 1; i < sortedCardValues.Count; i++)
        {
            if ((int)sortedCardValues[i] - (int)sortedCardValues[i - 1] != 1)
                return false;
        }
        return true;
    }
}
