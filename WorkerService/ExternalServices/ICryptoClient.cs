namespace WorkerService.ExternalServices;

public interface ICryptoClient
{
	Task<CoinResponse?> GetCoinsAsync();
}