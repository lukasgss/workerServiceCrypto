namespace WorkerService.InternalServices;

public sealed record UpsertCoinsRequest(
	string Symbol,
	int RankByMarketCap,
	decimal PercentageChangeInOneHour,
	PriceRequest Price,
	PriceRequest MarketCap,
	PriceRequest TradingVolume
);