﻿using PokerHands.Models;

namespace PokerHands.Services.Interfaces;
public interface IDeckService
{
    public IReadOnlyList<Card> GetPokerDeck();
    public IReadOnlyList<Card> ShuffleDeck(IReadOnlyList<Card> deck);
    public void DealDeckToPlayers(IReadOnlyList<Card> deck, IReadOnlyList<Player> players);
}
