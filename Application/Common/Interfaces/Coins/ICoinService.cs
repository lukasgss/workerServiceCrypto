using Application.Common.Interfaces.Pagination;

namespace Application.Common.Interfaces.Coins;

public interface ICoinService
{
    Task<CoinResponse> GetCoinByIdAsync(Guid id);
    Task<PaginatedList<CoinResponse>> GetPaginatedCoinsAsync(int pageNumber, int pageSize);
    Task UpsertCoinsAsync(List<CreateCoinRequest> coins);
}