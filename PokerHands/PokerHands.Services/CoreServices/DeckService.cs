using PokerHands.Models;
using PokerHands.Services.Interfaces;

namespace PokerHands.Services.CoreServices;
public class DeckService : IDeckService
{
    public IReadOnlyList<Card> GetPokerDeck()
    {
        return PokerDeck.GetDeck();
    }
    public IReadOnlyList<Card> ShuffleDeck(IReadOnlyList<Card> deck)
    {
        var deckToShuffle = deck.ToList();

        //Fisher–Yates Shuffle Algorithm
        var random = new Random();
        var n = deckToShuffle.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            (deckToShuffle[n], deckToShuffle[k]) = (deckToShuffle[k], deckToShuffle[n]);
        }

        return deckToShuffle.AsReadOnly();
    }

    public List<Player> DealDeckToPlayers(IReadOnlyList<Card> deck, IReadOnlyList<Player> players)
    {
        var updatedPlayers = players.ToList();

        //Calculate number of cards to deal
        var cardsToDeal = players.Count * PlayerHand.GetMaxNumberOfCardsAllowedInHand;
        var playerIndex = 0;
        //Loop on number of cards to deal
        for (var cardIndex = 0; cardIndex < cardsToDeal; cardIndex++)
        {
            //Reset player index to first player once we've given a card to each player
            if (playerIndex == players.Count) playerIndex = 0;

            var card = deck[cardIndex];
            var player = updatedPlayers[playerIndex];
            player.Hand.AddCardToHand(card);
            playerIndex++;
        }

        return updatedPlayers;
    }
}
