using Microsoft.AspNetCore.Mvc;
using PokerHands.Models;
using PokerHands.Services.Interfaces;

namespace PokerHands.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PokerHandsController : ControllerBase
{
    private readonly IGameService _gameService;

    public PokerHandsController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpPost]
    public GameResults PlayPoker([FromBody] NewGameParams newGameParams)
    {
        return _gameService.PlayPoker(newGameParams);
    }
}
