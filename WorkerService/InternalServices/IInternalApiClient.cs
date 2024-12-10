namespace WorkerService.InternalServices;

public interface IInternalApiClient
{
	Task UpsertCoinsAsync(IEnumerable<UpsertCoinsRequest> request);
}