using Domain.CryptoCurrency.ValueObjects;

namespace Application.Common.Interfaces.Coins;

public sealed record CoinResponse(
    Guid Id,
    string Symbol,
    int RankByMarketCap,
    decimal PercentageChangeInOneHour,
    Price Price,
    Price MarketCap,
    Price TradingVolume);