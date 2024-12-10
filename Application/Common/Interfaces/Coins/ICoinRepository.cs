using Application.Common.Interfaces.GenericRepository;
using Application.Common.Interfaces.Pagination;
using Domain.CryptoCurrency.Entities;

namespace Application.Common.Interfaces.Coins;

public interface ICoinRepository : IGenericRepository<Coin>
{
    Task<CoinResponse?> GetByIdAsync(Guid id);
    Task<PaginatedList<CoinResponse>> GetPaginatedCoinsAsync(int pageNumber, int pageSize);
    Task<IReadOnlyDictionary<string, Coin>> GetExistingCoinsAsync(IEnumerable<string> symbols);
}