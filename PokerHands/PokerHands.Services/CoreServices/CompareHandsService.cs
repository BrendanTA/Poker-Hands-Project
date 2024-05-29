using PokerHands.Models;
using PokerHands.Models.Enums;
using PokerHands.Services.Interfaces;

namespace PokerHands.Services.CoreServices;
public class CompareHandsService : ICompareHandsService
{
    public Player? DetermineWinningPlayer(IReadOnlyList<Player> players)
    {
        Player? winningPlayer = null;
        var highestHandRank = HandRank.HighCard;
        var highestCardValues = new List<CardValue>();

        foreach (var player in players)
        {
            var playerHandRank = player.Hand.HandRank;
            var playerCardValues = GetRelevantCardValues(player.Hand);

            if (playerHandRank > highestHandRank)
            {
                highestHandRank = playerHandRank;
                highestCardValues = playerCardValues;
                winningPlayer = player;
            }
            else if (playerHandRank == highestHandRank)
            {
                var compareResult = CompareCardValues(playerCardValues, highestCardValues);

                if (compareResult > 0)
                {
                    highestCardValues = playerCardValues;
                    winningPlayer = player;
                }
                else if (compareResult == 0)
                {
                    winningPlayer = null;
                }
            }
        }

        return winningPlayer;
    }

    private List<CardValue> GetRelevantCardValues(PlayerHand hand)
    {
        var cardsGroupedByValue = hand.Cards.GroupBy(card => card.Value);

        switch (hand.HandRank)
        {
            case HandRank.StraightFlush:
            case HandRank.Straight:
            case HandRank.Flush:
            case HandRank.HighCard:
                return cardsGroupedByValue.Select(group => group.Key).OrderByDescending(value => value).ToList();
            case HandRank.FourOfAKind:
                return cardsGroupedByValue.OrderByDescending(group => group.Count()).ThenByDescending(group => group.Key).Select(group => group.Key).ToList();
            case HandRank.FullHouse:
            case HandRank.ThreeOfAKind:
                return cardsGroupedByValue.OrderByDescending(group => group.Count()).ThenByDescending(group => group.Key).Select(group => group.Key).ToList();
            case HandRank.TwoPairs:
                var pairValues = cardsGroupedByValue
                    .Where(group => group.Count() == 2)
                    .OrderByDescending(group => group.Key)
                    .Select(group => group.Key)
                    .ToList();

                var remainingValue = cardsGroupedByValue
                    .Where(group => group.Count() == 1)
                    .Select(group => group.Key)
                    .First();

                pairValues.Add(remainingValue);
                return pairValues;
            case HandRank.Pair:
                return cardsGroupedByValue
                    .Where(group => group.Count() == 2)
                    .Select(group => group.Key)
                    .Concat(cardsGroupedByValue.Where(group => group.Count() == 1).OrderByDescending(group => group.Key).Select(group => group.Key))
                    .ToList();
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private int CompareCardValues(List<CardValue> playerCardValues, List<CardValue> highestCardValues)
    {
        // Determine the minimum length of the two lists to avoid index out of range errors
        int minCount = Math.Min(playerCardValues.Count, highestCardValues.Count);

        // Compare each element in both lists up to the minCount
        for (int i = 0; i < minCount; i++)
        {
            if (playerCardValues[i] > highestCardValues[i])
                return 1;
            else if (playerCardValues[i] < highestCardValues[i])
                return -1;
        }

        // If all compared elements are equal, compare the lengths of the lists
        if (playerCardValues.Count > highestCardValues.Count)
            return 1; // New Winning Player
        else if (playerCardValues.Count < highestCardValues.Count)
            return -1; // Lost to Existing Highest Values

        // If both lists are of the same length and all elements are equal
        return 0; // Players are Tied
    }

}
