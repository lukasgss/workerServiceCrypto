using Application.Common.DTOs;

namespace Application.Common.Interfaces.Coins;

public sealed record CreateCoinRequest(
    string Symbol,
    int RankByMarketCap,
    decimal PercentageChangeInOneHour,
    PriceRequest Price,
    PriceRequest MarketCap,
    PriceRequest TradingVolume);