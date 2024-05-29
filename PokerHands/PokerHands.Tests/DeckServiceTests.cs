using PokerHands.Models;
using PokerHands.Models.Enums;
using PokerHands.Services.CoreServices;
using PokerHands.Services.Interfaces;

namespace PokerHands.Tests;

[TestClass]
public class DeckServiceTests
{
    private readonly IDeckService _deckService;

    public DeckServiceTests()
    {
        _deckService = new DeckService();
    }


    [TestMethod]
    public void GetPokerDeck_ReturnsFullDeckOf52CardsWithCorrectValuesAndSuits()
    {
        // Act
        var pokerDeck = PokerDeck.GetDeck();

        // Assert
        Assert.IsNotNull(pokerDeck);
        Assert.AreEqual(52, pokerDeck.Count);

        var expectedSuits = Enum.GetValues(typeof(CardSuit)).Cast<CardSuit>().ToList();
        var expectedValues = Enum.GetValues(typeof(CardValue)).Cast<CardValue>().ToList();

        foreach (var suit in expectedSuits)
        {
            var cardsOfSuit = pokerDeck.Where(card => card.Suit == suit).ToList();
            Assert.AreEqual(expectedValues.Count, cardsOfSuit.Count);

            foreach (var value in expectedValues)
            {
                var cardOfValue = cardsOfSuit.FirstOrDefault(card => card.Value == value);
                Assert.IsNotNull(cardOfValue);
            }
        }
    }

    [TestMethod]
    public void GetPokerDeck_ReturnsDistinctCards()
    {
        // Act
        var pokerDeck = PokerDeck.GetDeck();

        // Assert
        CollectionAssert.AllItemsAreUnique(pokerDeck.ToList());
    }

    [TestMethod]
    public void ShuffleDeck_ReturnsSameSizeAsDeck()
    {
        // Arrange
        var pokerDeck = PokerDeck.GetDeck();

        // Act
        var shuffledDeck = _deckService.ShuffleDeck(pokerDeck);

        // Assert
        Assert.AreEqual(pokerDeck.Count, shuffledDeck.Count);
    }

    [TestMethod]
    public void ShuffleDeck_ShufflesCardsInDeck()
    {
        // Arrange
        var pokerDeck = PokerDeck.GetDeck();

        // Act
        var shuffledDeck = _deckService.ShuffleDeck(pokerDeck);

        // Assert
        CollectionAssert.AreNotEqual(pokerDeck.ToList(), shuffledDeck.ToList());
    }

    [TestMethod]
    public void DealDeckToPlayers_DealsCorrectNumberOfCardsToEachPlayer()
    {
        // Arrange
        var pokerDeck = PokerDeck.GetDeck();
        var players = new List<Player>
        {
            new Player { Name = "Alice" },
            new Player { Name = "Bob" },
            new Player { Name = "Charlie" }
        };

        // Act
        _deckService.DealDeckToPlayers(pokerDeck, players);

        // Assert
        foreach (var player in players)
        {
            Assert.AreEqual(PlayerHand.GetMaxNumberOfCardsAllowedInHand, player.Hand.Cards.Count);
        }
    }
}