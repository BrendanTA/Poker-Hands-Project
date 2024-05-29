using Moq;
using PokerHands.Models;
using PokerHands.Models.Enums;
using PokerHands.Services.CoreServices;
using PokerHands.Services.Interfaces;

namespace PokerHands.Tests;

[TestClass]
public class GameServiceTests
{
    private readonly IGameService _gameService;
    private readonly IDeckService _deckService;
    private readonly IRankHandService _rankHandService;
    private readonly ICompareHandsService _compareHandsService;

    public GameServiceTests()
    {
        _deckService = new DeckService();
        _rankHandService = new RankHandService();
        _compareHandsService = new CompareHandsService();
        _gameService = new GameService(_deckService, _rankHandService, _compareHandsService);
    }

    [TestMethod]
    public void PlayPoker_CreatesCorrectNumberOfPlayers()
    {
        // Arrange
        var newGameParams = new NewGameParams { PlayerNames = new List<string> { "Alice", "Bob", "Charlie" } };

        // Act
        var gameResults = _gameService.PlayPoker(newGameParams);

        // Assert
        Assert.IsNotNull(gameResults);
        Assert.AreEqual(newGameParams.PlayerNames.Count, gameResults.PlayerResults.Count);
    }

    [TestMethod]
    public void PlayPoker_DealsCorrectNumberOfCardsToEachPlayer()
    {
        // Arrange
        var newGameParams = new NewGameParams { PlayerNames = new List<string> { "Alice", "Bob" } };

        // Act
        var gameResults = _gameService.PlayPoker(newGameParams);

        // Assert
        foreach (var player in gameResults.PlayerResults)
        {
            Assert.AreEqual(PlayerHand.GetMaxNumberOfCardsAllowedInHand, player.Hand.Cards.Count);
        }
    }

    [TestMethod]
    public void PlayPoker_RanksPlayerHands()
    {
        // Arrange
        var newGameParams = new NewGameParams { PlayerNames = new List<string> { "Alice", "Bob" } };

        // Act
        var gameResults = _gameService.PlayPoker(newGameParams);

        // Assert
        foreach (var player in gameResults.PlayerResults)
        {
            Assert.AreEqual(_rankHandService.EvaluateHand(player.Hand), player.Hand.HandRank);
        }
    }

    [TestMethod]
    public void PlayPoker_DeterminesWinningPlayer()
    {
        // Arrange
        var newGameParams = new NewGameParams { PlayerNames = new List<string> { "Alice", "Bob" } };

        // Act
        var gameResults = _gameService.PlayPoker(newGameParams);

        // Assert
        Assert.IsTrue(gameResults.GameResult.Contains("has won.") || gameResults.GameResult == "Tie Game.");
    }

    [TestMethod]
    public void PlayPoker_ReturnsCorrectGameResults()
    {
        // Arrange
        var newGameParams = new NewGameParams { PlayerNames = new List<string> { "Alice", "Bob" } };

        // Act
        var gameResults = _gameService.PlayPoker(newGameParams);

        // Assert
        Assert.IsNotNull(gameResults);
        Assert.IsNotNull(gameResults.GameResult);
        Assert.IsNotNull(gameResults.PlayerResults);
        Assert.AreEqual(newGameParams.PlayerNames.Count, gameResults.PlayerResults.Count);
    }

    [TestMethod]
    public void PlayPoker_WithFixedResults_ConfirmsResults()
    {
        // Arrange
        var playerNames = new List<string> { "Alice", "Bob" };
        var newGameParams = new NewGameParams { PlayerNames = playerNames };

        var fixedPokerDeck = new List<Card>
        {
            new Card { Suit = CardSuit.Hearts, Value = CardValue.Two },
            new Card { Suit = CardSuit.Clubs, Value = CardValue.Three },
            new Card { Suit = CardSuit.Diamonds, Value = CardValue.Four },
            new Card { Suit = CardSuit.Spades, Value = CardValue.Five },
            new Card { Suit = CardSuit.Hearts, Value = CardValue.Six },
            new Card { Suit = CardSuit.Clubs, Value = CardValue.Ten },
            new Card { Suit = CardSuit.Clubs, Value = CardValue.Jack },
            new Card { Suit = CardSuit.Clubs, Value = CardValue.Queen },
            new Card { Suit = CardSuit.Hearts, Value = CardValue.King },
            new Card { Suit = CardSuit.Spades, Value = CardValue.Ace }
        };

        var expectedWinnerName = "Bob";

        var mockDeckService = new Mock<IDeckService>();
        mockDeckService.Setup(ds => ds.GetPokerDeck()).Returns(fixedPokerDeck);
        mockDeckService.Setup(ds => ds.ShuffleDeck(It.IsAny<IReadOnlyList<Card>>())).Returns(fixedPokerDeck);
        mockDeckService.Setup(ds => ds.DealDeckToPlayers(It.IsAny<IReadOnlyList<Card>>(), It.IsAny<IReadOnlyList<Player>>()))
        .Returns<IReadOnlyList<Card>, List<Player>>((deck, players) =>
        {
            var updatedPlayers = new List<Player>(players);
            for (int i = 0; i < 5; i++)
            {
                //Populate Player 1's cards
                var card = fixedPokerDeck[i];
                updatedPlayers[0].Hand.AddCardToHand(card);
            }
            for (int i = 5; i < 9; i++)
            {
                //Populate Player 2's cards
                var card = fixedPokerDeck[i];
                updatedPlayers[1].Hand.AddCardToHand(card);
            }
            return updatedPlayers;
        });

        var gameService = new GameService(mockDeckService.Object, new RankHandService(), new CompareHandsService());

        // Act
        var gameResults = gameService.PlayPoker(newGameParams);

        // Assert
        Assert.IsNotNull(gameResults);
        Assert.AreEqual(playerNames.Count, gameResults.PlayerResults.Count);

        var winningPlayer = gameResults.PlayerResults.FirstOrDefault(p => p.Name == expectedWinnerName);
        Assert.IsNotNull(winningPlayer);
        Assert.AreEqual(HandRank.Straight, winningPlayer.Hand.HandRank);

        var player1 = gameResults.PlayerResults[0];
        Assert.AreEqual("Alice", player1.Name);
        Assert.AreEqual(HandRank.Straight, player1.Hand.HandRank);

        var player2 = gameResults.PlayerResults[1];
        Assert.AreEqual("Bob", player2.Name);
        Assert.AreEqual(HandRank.Straight, player2.Hand.HandRank);

        Assert.AreEqual($"{expectedWinnerName} has won.", gameResults.GameResult);
    }
}