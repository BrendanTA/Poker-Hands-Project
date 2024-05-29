using PokerHands.Models;
using PokerHands.Models.Enums;
using PokerHands.Services.Interfaces;

namespace PokerHands.Services.CoreServices;
internal class RankHandService : IRankHandService
{
    public void RankPlayerHands(List<Player> players)
    {
        foreach(var player in players)
        {
            player.Hand.AssignHandRank(EvaluateHand(player.Hand));
        }
    }

    private HandRank EvaluateHand(PlayerHand playerHand)
    {
        //This could be optimized to not loop as much, but considering the hand size is 5, there shouldn't be an issue here.
        var cardsGroupedByValue = playerHand.Cards.GroupBy(card => card.Value);
        var cardsGroupedBySuit = playerHand.Cards.GroupBy(card => card.Suit);

        bool isFlush = cardsGroupedBySuit.Any(group => group.Count() == 5);
        bool isStraight = IsStraight(cardsGroupedByValue.Select(group => group.Key));

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

    private static bool IsStraight(IEnumerable<CardValue> values)
    {
        List<CardValue> sortedValues = values.OrderBy(v => v).ToList();
        for (int i = 1; i < sortedValues.Count; i++)
        {
            if ((int)sortedValues[i] - (int)sortedValues[i - 1] != 1)
                return false;
        }
        return true;
    }
}
