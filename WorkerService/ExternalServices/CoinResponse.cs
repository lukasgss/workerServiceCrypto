using System.Text.Json.Serialization;

namespace WorkerService.ExternalServices;

public sealed class IndividualCoinResponse
{
	[JsonPropertyName("id")]
	public string Id { get; init; } = null!;

	[JsonPropertyName("symbol")]
	public string Symbol { get; init; } = null!;

	[JsonPropertyName("name")]
	public string Name { get; init; } = null!;

	[JsonPropertyName("nameid")]
	public string NameId { get; init; } = null!;

	[JsonPropertyName("rank")]
	public int Rank { get; init; }

	[JsonPropertyName("price_usd")]
	public string PriceUsd { get; init; } = null!;

	[JsonPropertyName("percent_change_24h")]
	public string PercentChange24H { get; init; } = null!;

	[JsonPropertyName("percent_change_1h")]
	public string PercentChange1H { get; init; } = null!;

	[JsonPropertyName("market_cap_usd")]
	public string MarketCapUsd { get; init; } = null!;

	[JsonPropertyName("volume24")]
	public decimal Volume24 { get; init; }
}

public sealed class CoinResponse
{
	[JsonPropertyName("data")]
	public IEnumerable<IndividualCoinResponse> Data { get; init; } = [];
}