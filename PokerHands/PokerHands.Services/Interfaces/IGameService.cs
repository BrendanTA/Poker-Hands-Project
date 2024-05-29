using PokerHands.Models;

namespace PokerHands.Services.Interfaces;
public interface IGameService
{
    public GameResults PlayPoker(NewGameParams newGameParams);
}
