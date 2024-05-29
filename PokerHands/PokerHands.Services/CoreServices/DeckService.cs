﻿using PokerHands.Models;
using PokerHands.Models.Enums;
using PokerHands.Services.Interfaces;

namespace PokerHands.Services.CoreServices;
internal class DeckService : IDeckService
{
    public IReadOnlyList<Card> GetPokerDeck()
    {
        var pokerDeck = new List<Card>();
        foreach(CardSuit cardSuit in Enum.GetValues(typeof(CardSuit)))
        {
            foreach(CardValue cardValue in Enum.GetValues(typeof(CardValue)))
            {
                pokerDeck.Add(new Card() { Suit = cardSuit, Value = cardValue });
            }
        }
        return pokerDeck.AsReadOnly();
    }

    public IReadOnlyList<Card> ShuffleDeck(IReadOnlyList<Card> deck)
    {
        var deckToShuffle = deck.ToList();

        //Fisher–Yates Shuffle Algorithm
        Random random = new Random();
        int n = deckToShuffle.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            Card card = deckToShuffle[k];
            deckToShuffle[k] = deckToShuffle[n];
            deckToShuffle[n] = card;
        }

        return deckToShuffle.AsReadOnly();
    }

    public void DealDeckToPlayers(IReadOnlyList<Card> deck, IReadOnlyList<Player> players)
    {
        //Calculate number of cards to deal
        var cardsToDeal = players.Count * PlayerHand.GetMaxNumberOfCardsAllowedInHand();

        var playerIndex = 0;
        //Loop on number of cards to deal
        for (int cardIndex = 0; cardIndex < cardsToDeal; cardIndex++)
        {
            //Reset player index to first player once we've given a card to each player
            if (playerIndex == players.Count) playerIndex = 0;

            var card = deck[cardIndex];
            var player = players[playerIndex];
            player.Hand.AddCardToHand(card);
            playerIndex++;
        }
    }
}