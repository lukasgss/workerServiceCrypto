using System.Net.Http.Json;

namespace WorkerService.ExternalServices;

public sealed class CryptoClient : ICryptoClient
{
	private readonly IHttpClientFactory _httpClientFactory;
	private readonly ILogger<CryptoClient> _logger;

	public CryptoClient(IHttpClientFactory httpClientFactory, ILogger<CryptoClient> logger)
	{
		_httpClientFactory = httpClientFactory;
		_logger = logger;
	}

	public async Task<CoinResponse?> GetCoinsAsync()
	{
		try
		{
			HttpClient client = _httpClientFactory.CreateClient(CryptoConfig.ClientKey);

			return await client.GetFromJsonAsync<CoinResponse>("/api/tickers/?start=0&limit=100");
		}
		catch (HttpRequestException ex)
		{
			_logger.LogError("An error occurred when obtaining coins: {Exception}", ex);
			return null;
		}
	}

	public async Task<Dictionary<string, IndividualExchangeResponse>?> GetExchangesAsync()
	{
		try
		{
			HttpClient client = _httpClientFactory.CreateClient(CryptoConfig.ClientKey);

			return await client.GetFromJsonAsync<Dictionary<string, IndividualExchangeResponse>>("/api/exchanges/");
		}
		catch (HttpRequestException ex)
		{
			_logger.LogError("An error occurred when obtaining coins: {Exception}", ex);
			return null;
		}
	}
}