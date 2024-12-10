using Application.Common.Interfaces.Pagination;

namespace Application.Common.Interfaces.Exchanges;

public interface IExchangeService
{
    Task<PaginatedList<ExchangeResponse>> GetPaginatedExchangesAsync(int pageNumber, int pageSize);
    Task<ExchangeResponse> GetExchangeByIdAsync(Guid id);
    Task UpsertExchangesAsync(List<CreateExchangeRequest> exchanges);
}