namespace WorkerService.InternalServices.Upsert;

public sealed record UpsertExchangeRequest(
	string Name,
	string NameId,
	PriceRequest Volume,
	int ActivePairs,
	string Url,
	string Country);