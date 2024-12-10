using Application.Common.Extensions;
using Application.Common.Interfaces.Coins;
using Application.Common.Interfaces.Pagination;
using Domain.CryptoCurrency.Entities;
using Domain.CryptoCurrency.ValueObjects;
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
			.Where(c => c.Id == id)
			.Select(c => new CoinResponse(
				c.Id,
				c.Symbol,
				c.RankByMarketCap,
				c.PercentageChangeInOneHour,
				Price.Create(c.Price.Amount, c.Price.Code),
				Price.Create(c.MarketCap.Amount, c.MarketCap.Code),
				Price.Create(c.TradingVolume.Amount, c.TradingVolume.Code)))
			.SingleOrDefaultAsync();
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