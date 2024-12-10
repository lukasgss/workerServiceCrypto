namespace WorkerService.ExternalServices;

public static class CryptoConfig
{
	public const string ClientKey = "Crypto";
	public static readonly Uri BaseUrl = new("https://api.coinlore.net");
}