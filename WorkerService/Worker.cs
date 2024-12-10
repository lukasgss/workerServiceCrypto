using WorkerService.ExternalServices;
using WorkerService.InternalServices;

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

	public async Task Execute()
	{
		CoinResponse? coins = await _cryptoClient.GetCoinsAsync();
		if (coins is null)
		{
			return;
		}

		var upsertCoinsRequests = coins.Data
			.Select(c => new UpsertCoinsRequest(
				c.Symbol,
				c.Rank,
				decimal.Parse(c.PercentChange1H),
				new PriceRequest(Currency, decimal.Parse(c.PriceUsd)),
				new PriceRequest(Currency, decimal.Parse(c.MarketCapUsd)),
				new PriceRequest(Currency, c.Volume24)));

		await _internalApiClient.UpsertCoinsAsync(upsertCoinsRequests);
	}
}