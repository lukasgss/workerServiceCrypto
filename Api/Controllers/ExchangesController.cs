using Application.Common.Interfaces.Exchanges;
using Application.Common.Interfaces.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("/api/exchanges")]
public sealed class ExchangesController : ControllerBase
{
    private readonly IExchangeService _exchangeService;

    public ExchangesController(IExchangeService exchangeService)
    {
        _exchangeService = exchangeService;
    }

    [HttpGet]
    public async Task<PaginatedList<ExchangeResponse>> GetAll([FromQuery] Query query)
    {
        return await _exchangeService.GetPaginatedExchangesAsync(query.PageNumber, query.PageSize);
    }

    [HttpGet("{id:guid}")]
    public async Task<ExchangeResponse> GetById(Guid id)
    {
        return await _exchangeService.GetExchangeByIdAsync(id);
    }

    [HttpPost]
    public async Task<ActionResult> Upsert(List<CreateExchangeRequest> exchanges)
    {
        await _exchangeService.UpsertExchangesAsync(exchanges);

        return Ok();
    }
}