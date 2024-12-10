using Application.Common.DTOs;

namespace Application.Common.Interfaces.Exchanges;

public sealed record CreateExchangeRequest(
    string Name,
    PriceRequest Volume,
    int ActivePairs,
    string Url,
    string Country);