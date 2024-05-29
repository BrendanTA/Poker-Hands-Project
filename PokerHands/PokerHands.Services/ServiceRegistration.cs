using Microsoft.Extensions.DependencyInjection;
using PokerHands.Services.CoreServices;
using PokerHands.Services.Interfaces;

namespace PokerHands.Services;
public static class ServiceRegistration
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IGameService, GameService>();
        services.AddScoped<IDeckService, DeckService>();
        services.AddScoped<IRankHandService, RankHandService>();
        services.AddScoped<ICompareHandsService, CompareHandsService>();

        return services;
    }
}
