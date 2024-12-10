using Application.Common.Interfaces.GenericRepository;
using Application.Common.Interfaces.Pagination;
using Domain.CryptoCurrency.Entities;

namespace Application.Common.Interfaces.Exchanges;

public interface IExchangeRepository : IGenericRepository<Exchange>
{
    Task<IReadOnlyDictionary<string, Exchange>> GetExistingExchangesAsync(IEnumerable<string> names);
    Task<PaginatedList<ExchangeResponse>> GetPaginatedExchangesAsync(int pageNumber, int pageSize);
    Task<ExchangeResponse?> GetExchangeByIdAsync(Guid id);
}