using Application.Common;
using Application.Common.Extensions;
using Application.Common.Interfaces.Coins;
using Application.Common.Interfaces.Pagination;
using Domain.CryptoCurrency.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public sealed class CoinRepository : GenericRepository<Coin>, ICoinRepository
{
    private readonly AppDbContext _dbContext;

    public CoinRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CoinResponse?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Coins
            .AsNoTracking()
            .Select(c => new CoinResponse(
                c.Id,
                c.Symbol,
                c.RankByMarketCap,
                c.PercentageChangeInOneHour,
                c.Price,
                c.MarketCap,
                c.TradingVolume))
            .SingleOrDefaultAsync(c => c.Id == id);
    }

    public async Task<PaginatedList<CoinResponse>> GetPaginatedCoinsAsync(int pageNumber, int pageSize)
    {
        return await _dbContext.Coins
            .AsNoTracking()
            .Select(c => new CoinResponse(
                c.Id,
                c.Symbol,
                c.RankByMarketCap,
                c.PercentageChangeInOneHour,
                c.Price,
                c.MarketCap,
                c.TradingVolume))
            .ToPaginatedListAsync(pageNumber, pageSize);
    }

    public async Task<IReadOnlyDictionary<string, Coin>> GetExistingCoinsAsync(IEnumerable<string> symbols)
    {
        return await _dbContext.Coins
            .Where(c => symbols.Contains(c.Symbol))
            .ToDictionaryAsync(c => c.Symbol);
    }
}