namespace Application.Common.Interfaces.Pagination;

public sealed class Query
{
	public int PageNumber { get; init; } = 1;
	public int PageSize { get; init; } = 25;
}