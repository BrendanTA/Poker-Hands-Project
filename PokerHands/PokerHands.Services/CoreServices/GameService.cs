using PokerHands.Models;
using PokerHands.Services.Interfaces;

namespace PokerHands.Services.CoreServices;
public class GameService : IGameService
{
    private readonly IDeckService _deckService;
    private readonly IRankHandService _rankHandService;
    private readonly ICompareHandsService _compareHandService;

    public GameService(IDeckService deckService, IRankHandService rankHandService, ICompareHandsService compareHandService)
    {
        _deckService = deckService;
        _rankHandService = rankHandService;
        _compareHandService = compareHandService;
    }

    public GameResults PlayPoker(NewGameParams newGameParams)
    {
        //Create all the new players
        var players = new List<Player>();
        foreach(var playerName in newGameParams.PlayerNames)
        {
            players.Add(new Player { Name = playerName });
        }

        //Get default deck and shuffle it
        var pokerDeck = _deckService.GetPokerDeck();
        var shuffledPokerDeck = _deckService.ShuffleDeck(pokerDeck);

        //Deal the cards
        var updatedPlayers = _deckService.DealDeckToPlayers(shuffledPokerDeck, players);

        //Rank the hands
        _rankHandService.RankPlayerHands(updatedPlayers);

        //Compare the hands
        var winningPlayer = _compareHandService.DetermineWinningPlayer(players);

        //Return the game results
        var gameResultString = winningPlayer == null ? "Tie Game." : $"{winningPlayer.Name} has won.";
        return new GameResults() { GameResult = gameResultString, PlayerResults = players };
    }
}
