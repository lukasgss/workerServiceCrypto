using Domain.CryptoCurrency.ValueObjects;

namespace Application.Common.Interfaces.Exchanges;

public sealed record ExchangeResponse(
	Guid Id,
	string Name,
	Money Volume,
	string? Country,
	string? Url,
	int ActivePairs);