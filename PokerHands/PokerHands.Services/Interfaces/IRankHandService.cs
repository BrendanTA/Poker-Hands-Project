﻿using PokerHands.Models;
using PokerHands.Models.Enums;

namespace PokerHands.Services.Interfaces;
public interface IRankHandService
{
    public void RankPlayerHands(List<Player> players);
    public HandRank EvaluateHand(PlayerHand playerHand);
}
