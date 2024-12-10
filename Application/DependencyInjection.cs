using Application.Common.Interfaces.Coins;
using Application.Common.Interfaces.Exchanges;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICoinService, CoinService>();
        services.AddScoped<IExchangeService, ExchangeService>();

        return services;
    }
}