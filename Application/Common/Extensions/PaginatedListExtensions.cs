using Application.Common.Interfaces.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Extensions;

public static class PaginatedListExtensions
{
    public static Task<PaginatedList<TDestination>> ToPaginatedListAsync<TDestination>(
        this IQueryable<TDestination> queryable, int pageNumber, int pageSize) where TDestination : class
    {
        return PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize);
    }
}