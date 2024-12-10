using Application.Common.Interfaces.Coins;
using Application.Common.Interfaces.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("/api/coins")]
public sealed class CoinsController : ControllerBase
{
    private readonly ICoinService _coinService;

    public CoinsController(ICoinService coinService)
    {
        _coinService = coinService;
    }

    [HttpGet]
    public async Task<PaginatedList<CoinResponse>> GetAllPaginated([FromQuery] Query query)
    {
        return await _coinService.GetPaginatedCoinsAsync(query.PageNumber, query.PageSize);
    }

    [HttpGet("{id:guid}")]
    public async Task<CoinResponse> GetById(Guid id)
    {
        return await _coinService.GetCoinByIdAsync(id);
    }

    [HttpPost]
    public async Task<ActionResult> Upsert(List<CreateCoinRequest> coins)
    {
        await _coinService.UpsertCoinsAsync(coins);

        return Ok();
    }
}