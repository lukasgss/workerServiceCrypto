namespace WorkerService.InternalServices.Upsert;

public sealed record UpsertCoinRequest(
	string Symbol,
	int RankByMarketCap,
	decimal PercentageChangeInOneHour,
	PriceRequest Price,
	PriceRequest MarketCap,
	PriceRequest TradingVolume
);