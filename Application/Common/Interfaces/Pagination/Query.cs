namespace Application.Common.Interfaces.Pagination;

public abstract class Query
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 25;
}