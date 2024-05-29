using PokerHands.Models;
using PokerHands.Models.Enums;
using PokerHands.Services.Interfaces;

namespace PokerHands.Services.CoreServices;
internal class CompareHandsService : ICompareHandsService
{
    public Player? DetermineWinningPlayer(IReadOnlyList<Player> players)
    {
        Player? winningPlayer = null;
        var highestHandRank = HandRank.HighCard;
        var highestCardValue = CardValue.Two;
        foreach (var player in players)
        {
            var playerHandRank = player.Hand.HandRank;
            var playerHighCardValue = player.Hand.GetHighCard().Value;

            //If a player has a higher hand rank, then we should use their highest card value and they're the current winner
            if (playerHandRank > highestHandRank)
            {
                highestHandRank = playerHandRank;
                highestCardValue = playerHighCardValue;
                winningPlayer = player;
            }
            else if (playerHandRank == highestHandRank)
            {
                //If a player has the same hand rank as the highest, we should compare highest card value
                if (playerHighCardValue > highestCardValue)
                {
                    highestCardValue = playerHighCardValue;
                    winningPlayer = player;
                }
                else if (playerHighCardValue == highestCardValue)
                {
                    //If someone has the same rank as highest but the same value that means we have a tie
                    winningPlayer = null;
                }
            }
        }

        return winningPlayer;
    }
}
