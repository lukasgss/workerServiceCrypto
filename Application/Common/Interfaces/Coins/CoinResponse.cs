using Domain.CryptoCurrency.ValueObjects;

namespace Application.Common.Interfaces.Coins;

public sealed record CoinResponse(
	Guid Id,
	string Symbol,
	int RankByMarketCap,
	decimal PercentageChangeInOneHour,
	Money Price,
	Money MarketCap,
	Money TradingVolume);