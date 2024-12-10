using System.Text.Json.Serialization;

namespace WorkerService.ExternalServices;

public sealed class IndividualExchangeResponse
{
	[JsonPropertyName("name")]
	public string Name { get; init; } = null!;

	[JsonPropertyName("name_id")]
	public string NameId { get; init; } = null!;

	[JsonPropertyName("volume_usd")]
	public decimal VolumeUsd { get; init; }

	[JsonPropertyName("active_pairs")]
	public int ActivePairs { get; init; }

	[JsonPropertyName("url")]
	public string Url { get; init; } = null!;

	[JsonPropertyName("country")]
	public string Country { get; init; } = null!;
}