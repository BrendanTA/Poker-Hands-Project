using PokerHands.Models;

namespace PokerHands.Services.Interfaces;
public interface ICompareHandsService
{
    public Player? DetermineWinningPlayer(IReadOnlyList<Player> players);
}
