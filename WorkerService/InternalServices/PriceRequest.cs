namespace WorkerService.InternalServices;

public sealed record PriceRequest(string Currency, decimal Amount);