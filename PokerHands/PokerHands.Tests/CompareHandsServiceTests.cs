using PokerHands.Models.Enums;
using PokerHands.Models;
using PokerHands.Services.CoreServices;
using PokerHands.Services.Interfaces;

namespace PokerHands.Tests;

[TestClass]
public class CompareHandsServiceTests
{
    private readonly IRankHandService _rankHandService;
    private readonly ICompareHandsService _compareHandsService;

    public CompareHandsServiceTests()
    {
        _rankHandService = new RankHandService();
        _compareHandsService = new CompareHandsService();
    }

    [TestMethod]
    public void DetermineWinningPlayer_HighCardVsHighCard_ReturnsPlayerWithHigherCard()
    {
        // Arrange
        var player1 = new Player { Name = "Ted" };
        player1.Hand.AddCardsToHand(new List<Card>
        {
            new Card { Suit = CardSuit.Hearts, Value = CardValue.Two },
            new Card { Suit = CardSuit.Diamonds, Value = CardValue.Three },
            new Card { Suit = CardSuit.Spades, Value = CardValue.Five },
            new Card { Suit = CardSuit.Clubs, Value = CardValue.Nine },
            new Card { Suit = CardSuit.Diamonds, Value = CardValue.King }
        });

        var player2 = new Player { Name = "Louis" };
        player2.Hand.AddCardsToHand(new List<Card>
        {
            new Card { Suit = CardSuit.Clubs, Value = CardValue.Two },
            new Card { Suit = CardSuit.Hearts, Value = CardValue.Three },
            new Card { Suit = CardSuit.Spades, Value = CardValue.Four },
            new Card { Suit = CardSuit.Clubs, Value = CardValue.Eight },
            new Card { Suit = CardSuit.Hearts, Value = CardValue.Ace }
        });


        // Act
        var players = new List<Player> { player1, player2 };
        _rankHandService.RankPlayerHands(players);
        var winner = _compareHandsService.DetermineWinningPlayer(players);

        // Assert
        Assert.IsNotNull(winner);
        Assert.AreEqual("Louis", winner.Name);
    }

    [TestMethod]
    public void DetermineWinningPlayer_StraightFlushVsTwoPair_ReturnsPlayerWithStraightFlush()
    {
        // Arrange
        var player1 = new Player { Name = "Black" };
        player1.Hand.AddCardsToHand(new List<Card>
        {
            new Card { Suit = CardSuit.Hearts, Value = CardValue.Two },
            new Card { Suit = CardSuit.Hearts, Value = CardValue.Three },
            new Card { Suit = CardSuit.Hearts, Value = CardValue.Four },
            new Card { Suit = CardSuit.Hearts, Value = CardValue.Five },
            new Card { Suit = CardSuit.Hearts, Value = CardValue.Six }
        });

        var player2 = new Player { Name = "White" };
        player2.Hand.AddCardsToHand(new List<Card>
        {
            new Card { Suit = CardSuit.Clubs, Value = CardValue.Two },
            new Card { Suit = CardSuit.Hearts, Value = CardValue.Two },
            new Card { Suit = CardSuit.Hearts, Value = CardValue.Three },
            new Card { Suit = CardSuit.Clubs, Value = CardValue.Three },
            new Card { Suit = CardSuit.Hearts, Value = CardValue.King }
        });

        // Act
        var players = new List<Player> { player1, player2 };
        _rankHandService.RankPlayerHands(players);
        var winner = _compareHandsService.DetermineWinningPlayer(players);

        // Assert
        Assert.IsNotNull(winner);
        Assert.AreEqual("Black", winner.Name);
    }

    [TestMethod]
    public void DetermineWinningPlayer_SameHandRankSameHighCard_ReturnsNull()
    {
        // Arrange
        var player1 = new Player { Name = "Alice" };
        player1.Hand.AddCardsToHand(new List<Card>
        {
            new Card { Suit = CardSuit.Hearts, Value = CardValue.Two },
            new Card { Suit = CardSuit.Diamonds, Value = CardValue.Three },
            new Card { Suit = CardSuit.Spades, Value = CardValue.Four },
            new Card { Suit = CardSuit.Clubs, Value = CardValue.Five },
            new Card { Suit = CardSuit.Diamonds, Value = CardValue.Ace }
        });

        var player2 = new Player { Name = "Bob" };
        player2.Hand.AddCardsToHand(new List<Card>
        {
            new Card { Suit = CardSuit.Clubs, Value = CardValue.Two },
            new Card { Suit = CardSuit.Hearts, Value = CardValue.Three },
            new Card { Suit = CardSuit.Spades, Value = CardValue.Four },
            new Card { Suit = CardSuit.Hearts, Value = CardValue.Five },
            new Card { Suit = CardSuit.Hearts, Value = CardValue.Ace }
        });

        // Act
        var players = new List<Player> { player1, player2 };
        _rankHandService.RankPlayerHands(players);
        var winner = _compareHandsService.DetermineWinningPlayer(players);

        // Assert
        Assert.IsNull(winner);
    }

    [TestMethod]
    public void DetermineWinningPlayer_PairVsPair_ReturnsPlayerWithHigherPair()
    {
        // Arrange
        var player1 = new Player { Name = "Alice" };
        player1.Hand.AddCardsToHand(new List<Card>
        {
            new Card { Suit = CardSuit.Hearts, Value = CardValue.King },
            new Card { Suit = CardSuit.Clubs, Value = CardValue.King },
            new Card { Suit = CardSuit.Diamonds, Value = CardValue.Ten },
            new Card { Suit = CardSuit.Spades, Value = CardValue.Eight },
            new Card { Suit = CardSuit.Hearts, Value = CardValue.Four }
        });

        var player2 = new Player { Name = "Bob" };
        player2.Hand.AddCardsToHand(new List<Card>
        {
            new Card { Suit = CardSuit.Clubs, Value = CardValue.King },
            new Card { Suit = CardSuit.Hearts, Value = CardValue.King },
            new Card { Suit = CardSuit.Spades, Value = CardValue.Nine },
            new Card { Suit = CardSuit.Diamonds, Value = CardValue.Seven },
            new Card { Suit = CardSuit.Hearts, Value = CardValue.Six }
        });

        // Act
        var players = new List<Player> { player1, player2 };
        _rankHandService.RankPlayerHands(players);
        var winner = _compareHandsService.DetermineWinningPlayer(players);

        // Assert
        Assert.IsNotNull(winner);
        Assert.AreEqual("Alice", winner.Name);
    }

    [TestMethod]
    public void DetermineWinningPlayer_TwoPairsVsTwoPairs_ReturnsPlayerWithHigherTwoPairs()
    {
        // Arrange
        var player1 = new Player { Name = "Alice" };
        player1.Hand.AddCardsToHand(new List<Card>
        {
            new Card { Suit = CardSuit.Hearts, Value = CardValue.Queen },
            new Card { Suit = CardSuit.Clubs, Value = CardValue.Queen },
            new Card { Suit = CardSuit.Diamonds, Value = CardValue.Eight },
            new Card { Suit = CardSuit.Spades, Value = CardValue.Eight },
            new Card { Suit = CardSuit.Hearts, Value = CardValue.Four }
        });

        var player2 = new Player { Name = "Bob" };
        player2.Hand.AddCardsToHand(new List<Card>
        {
            new Card { Suit = CardSuit.Clubs, Value = CardValue.Queen },
            new Card { Suit = CardSuit.Hearts, Value = CardValue.Queen },
            new Card { Suit = CardSuit.Spades, Value = CardValue.Seven },
            new Card { Suit = CardSuit.Diamonds, Value = CardValue.Seven },
            new Card { Suit = CardSuit.Hearts, Value = CardValue.Six }
        });

        // Act
        var players = new List<Player> { player1, player2 };
        _rankHandService.RankPlayerHands(players);
        var winner = _compareHandsService.DetermineWinningPlayer(players);

        // Assert
        Assert.IsNotNull(winner);
        Assert.AreEqual("Alice", winner.Name);
    }

    [TestMethod]
    public void DetermineWinningPlayer_ThreeOfAKindVsThreeOfAKind_ReturnsPlayerWithHigherThreeOfAKind()
    {
        // Arrange
        var player1 = new Player { Name = "Alice" };
        player1.Hand.AddCardsToHand(new List<Card>
        {
            new Card { Suit = CardSuit.Hearts, Value = CardValue.Ten },
            new Card { Suit = CardSuit.Clubs, Value = CardValue.Ten },
            new Card { Suit = CardSuit.Diamonds, Value = CardValue.Ten },
            new Card { Suit = CardSuit.Spades, Value = CardValue.Seven },
            new Card { Suit = CardSuit.Hearts, Value = CardValue.Four }
        });

        var player2 = new Player { Name = "Bob" };
        player2.Hand.AddCardsToHand(new List<Card>
        {
            new Card { Suit = CardSuit.Clubs, Value = CardValue.Eight },
            new Card { Suit = CardSuit.Hearts, Value = CardValue.Eight },
            new Card { Suit = CardSuit.Spades, Value = CardValue.Eight },
            new Card { Suit = CardSuit.Diamonds, Value = CardValue.Queen },
            new Card { Suit = CardSuit.Hearts, Value = CardValue.Six }
        });

        // Act
        var players = new List<Player> { player1, player2 };
        _rankHandService.RankPlayerHands(players);
        var winner = _compareHandsService.DetermineWinningPlayer(players);

        // Assert
        Assert.IsNotNull(winner);
        Assert.AreEqual("Alice", winner.Name);
    }
}