using System.Text;
using Newtonsoft.Json;

namespace WorkerService.InternalServices;

public sealed class InternalApiClient : IInternalApiClient
{
	private readonly IHttpClientFactory _httpClient;
	private readonly ILogger<InternalApiClient> _logger;

	public InternalApiClient(IHttpClientFactory httpClient, ILogger<InternalApiClient> logger)
	{
		_httpClient = httpClient;
		_logger = logger;
	}

	public async Task UpsertCoinsAsync(IEnumerable<UpsertCoinsRequest> request)
	{
		try
		{
			HttpClient client = _httpClient.CreateClient(InternalApiConfig.ClientKey);

			string json = JsonConvert.SerializeObject(request);
			StringContent content = new(json, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await client.PostAsync("/api/coins", content);
			response.EnsureSuccessStatusCode();
		}
		catch (Exception ex)
		{
			_logger.LogError("An error occurred upserting coins into the interl api. {Exception}", ex);
		}
	}
}