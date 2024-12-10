namespace WorkerService.ExternalServices;

public interface ICryptoClient
{
	Task<CoinResponse?> GetCoinsAsync();
	Task<Dictionary<string, IndividualExchangeResponse>?> GetExchangesAsync();
}