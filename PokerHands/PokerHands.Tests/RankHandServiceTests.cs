using PokerHands.Models;
using PokerHands.Models.Enums;
using PokerHands.Services.CoreServices;

namespace PokerHands.Tests;

[TestClass]
public class RankHandServiceTests
{
    private readonly RankHandService _rankHandService = new();

    [TestMethod]
    public void EvaluateHand_StraightFlush()
    {
        var playerHand = new PlayerHand();
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.Two });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.Three });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.Four });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.Five });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.Six });

        var result = _rankHandService.EvaluateHand(playerHand);

        Assert.AreEqual(HandRank.StraightFlush, result);
    }

    [TestMethod]
    public void EvaluateHand_FourOfAKind()
    {
        var playerHand = new PlayerHand();
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.Two });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Diamonds, Value = CardValue.Two });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Clubs, Value = CardValue.Two });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Spades, Value = CardValue.Two });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.King });

        var result = _rankHandService.EvaluateHand(playerHand);

        Assert.AreEqual(HandRank.FourOfAKind, result);
    }

    [TestMethod]
    public void EvaluateHand_FullHouse()
    {
        var playerHand = new PlayerHand();
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.Two });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Diamonds, Value = CardValue.Two });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Clubs, Value = CardValue.Two });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Spades, Value = CardValue.King });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.King });

        var result = _rankHandService.EvaluateHand(playerHand);

        Assert.AreEqual(HandRank.FullHouse, result);
    }

    [TestMethod]
    public void EvaluateHand_Flush()
    {
        var playerHand = new PlayerHand();
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.Two });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.Five });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.Seven });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.Nine });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.King });

        var result = _rankHandService.EvaluateHand(playerHand);

        Assert.AreEqual(HandRank.Flush, result);
    }

    [TestMethod]
    public void EvaluateHand_Straight()
    {
        var playerHand = new PlayerHand();
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.Two });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Diamonds, Value = CardValue.Three });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Clubs, Value = CardValue.Four });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Spades, Value = CardValue.Five });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.Six });

        var result = _rankHandService.EvaluateHand(playerHand);

        Assert.AreEqual(HandRank.Straight, result);
    }

    [TestMethod]
    public void EvaluateHand_ThreeOfAKind()
    {
        var playerHand = new PlayerHand();
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.Two });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Diamonds, Value = CardValue.Two });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Clubs, Value = CardValue.Two });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Spades, Value = CardValue.Five });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.King });

        var result = _rankHandService.EvaluateHand(playerHand);

        Assert.AreEqual(HandRank.ThreeOfAKind, result);
    }

    [TestMethod]
    public void EvaluateHand_TwoPairs()
    {
        var playerHand = new PlayerHand();
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.Two });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Diamonds, Value = CardValue.Two });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Clubs, Value = CardValue.Five });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Spades, Value = CardValue.Five });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.King });

        var result = _rankHandService.EvaluateHand(playerHand);

        Assert.AreEqual(HandRank.TwoPairs, result);
    }

    [TestMethod]
    public void EvaluateHand_Pair()
    {
        var playerHand = new PlayerHand();
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.Two });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Diamonds, Value = CardValue.Two });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Clubs, Value = CardValue.Five });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Spades, Value = CardValue.Seven });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.King });

        var result = _rankHandService.EvaluateHand(playerHand);

        Assert.AreEqual(HandRank.Pair, result);
    }

    [TestMethod]
    public void EvaluateHand_HighCard()
    {
        var playerHand = new PlayerHand();
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.Two });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Diamonds, Value = CardValue.Four });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Clubs, Value = CardValue.Five });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Spades, Value = CardValue.Seven });
        playerHand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.King });

        var result = _rankHandService.EvaluateHand(playerHand);

        Assert.AreEqual(HandRank.HighCard, result);
    }

    [TestMethod]
    public void RankPlayerHands_AssignsRankToPlayerHands()
    {
        var players = new List<Player>
        {
            new Player { Name = "Player 1" },
            new Player { Name = "Player 2" }
        };

        players[0].Hand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.Two });
        players[0].Hand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.Three });
        players[0].Hand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.Four });
        players[0].Hand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.Five });
        players[0].Hand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.Six });


        players[1].Hand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.Two });
        players[1].Hand.AddCardToHand(new Card { Suit = CardSuit.Diamonds, Value = CardValue.Two });
        players[1].Hand.AddCardToHand(new Card { Suit = CardSuit.Clubs, Value = CardValue.Five });
        players[1].Hand.AddCardToHand(new Card { Suit = CardSuit.Spades, Value = CardValue.Seven });
        players[1].Hand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.King });

        _rankHandService.RankPlayerHands(players);

        Assert.AreEqual(HandRank.StraightFlush, players[0].Hand.HandRank);
        Assert.AreEqual(HandRank.Pair, players[1].Hand.HandRank);
    }

    //[TestMethod]
    //public void RankPlayerHands_AssignsRankToPlayerHands()
    //{
    //    var players = new List<Player>
    //    {
    //        new Player
    //        {
    //            Name = "Ted",
    //            Hand = new PlayerHand()
    //        },
    //        new Player
    //        {
    //            Name = "Louis",
    //            Hand = new PlayerHand()
    //        }
    //    };

    //    players[0].Hand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.Two });
    //    players[0].Hand.AddCardToHand(new Card { Suit = CardSuit.Diamonds, Value = CardValue.Three });
    //    players[0].Hand.AddCardToHand(new Card { Suit = CardSuit.Spades, Value = CardValue.Five });
    //    players[0].Hand.AddCardToHand(new Card { Suit = CardSuit.Clubs, Value = CardValue.Nine });
    //    players[0].Hand.AddCardToHand(new Card { Suit = CardSuit.Diamonds, Value = CardValue.King });


    //    players[1].Hand.AddCardToHand(new Card { Suit = CardSuit.Clubs, Value = CardValue.Two });
    //    players[1].Hand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.Three });
    //    players[1].Hand.AddCardToHand(new Card { Suit = CardSuit.Spades, Value = CardValue.Four });
    //    players[1].Hand.AddCardToHand(new Card { Suit = CardSuit.Clubs, Value = CardValue.Eight });
    //    players[1].Hand.AddCardToHand(new Card { Suit = CardSuit.Hearts, Value = CardValue.Ace });

    //    _rankHandService.RankPlayerHands(players);

    //    Assert.AreEqual(HandRank.HighCard, players[0].Hand.HandRank);
    //    Assert.AreEqual(HandRank.HighCard, players[1].Hand.HandRank);
    //}
}