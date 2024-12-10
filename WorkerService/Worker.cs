using WorkerService.ExternalServices;
using WorkerService.InternalServices;
using WorkerService.InternalServices.Upsert;

namespace WorkerService;

public sealed class Worker
{
	private readonly ICryptoClient _cryptoClient;
	private readonly IInternalApiClient _internalApiClient;

	private const string Currency = "USD";

	public Worker(ICryptoClient cryptoClient, IInternalApiClient internalApiClient)
	{
		_cryptoClient = cryptoClient;
		_internalApiClient = internalApiClient;
	}

	public async Task ExecuteCoins()
	{
		CoinResponse? coins = await _cryptoClient.GetCoinsAsync();
		if (coins is null)
		{
			return;
		}

		var upsertCoinsRequest = coins.Data
			.Select(c => new UpsertCoinRequest(
				c.Symbol,
				c.Rank,
				decimal.Parse(c.PercentChange1H),
				new PriceRequest(Currency, decimal.Parse(c.PriceUsd)),
				new PriceRequest(Currency, decimal.Parse(c.MarketCapUsd)),
				new PriceRequest(Currency, c.Volume24)));

		await _internalApiClient.UpsertCoinsAsync(upsertCoinsRequest);
	}

	public async Task ExecuteExchanges()
	{
		var exchanges = await _cryptoClient.GetExchangesAsync();
		if (exchanges is null)
		{
			return;
		}

		var upsertExchangesRequest = exchanges
			.Select(e => new UpsertExchangeRequest(
				e.Value.Name,
				e.Value.NameId,
				new PriceRequest(Currency, e.Value.VolumeUsd),
				e.Value.ActivePairs,
				e.Value.Url,
				e.Value.Country));

		await _internalApiClient.UpsertExchangesAsync(upsertExchangesRequest);
	}
}