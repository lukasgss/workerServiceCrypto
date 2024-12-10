using Application.Common.Extensions;
using Application.Common.Interfaces.Exchanges;
using Application.Common.Interfaces.Pagination;
using Domain.CryptoCurrency.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public sealed class ExchangeRepository : GenericRepository<Exchange>, IExchangeRepository
{
    private readonly AppDbContext _dbContext;

    public ExchangeRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyDictionary<string, Exchange>> GetExistingExchangesAsync(IEnumerable<string> names)
    {
        return await _dbContext.Exchanges
            .Where(c => names.Contains(c.Name))
            .ToDictionaryAsync(c => c.Name);
    }

    public async Task<PaginatedList<ExchangeResponse>> GetPaginatedExchangesAsync(int pageNumber, int pageSize)
    {
        return await _dbContext.Exchanges
            .AsNoTracking()
            .Select(e => new ExchangeResponse(e.Id, e.Name, e.Volume, e.Country, e.Url, e.ActivePairs))
            .ToPaginatedListAsync(pageNumber, pageSize);
    }

    public async Task<ExchangeResponse?> GetExchangeByIdAsync(Guid id)
    {
        return await _dbContext.Exchanges
            .AsNoTracking()
            .Select(e => new ExchangeResponse(e.Id, e.Name, e.Volume, e.Country, e.Url, e.ActivePairs))
            .SingleOrDefaultAsync(e => e.Id == id);
    }
}