using WorkerService.InternalServices.Upsert;

namespace WorkerService.InternalServices;

public interface IInternalApiClient
{
	Task UpsertCoinsAsync(IEnumerable<UpsertCoinRequest> request);
	Task UpsertExchangesAsync(IEnumerable<UpsertExchangeRequest> request);
}