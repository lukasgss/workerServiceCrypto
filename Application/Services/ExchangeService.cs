using Application.Common.Exceptions;
using Application.Common.Interfaces.Exchanges;
using Application.Common.Interfaces.Pagination;
using Domain.CryptoCurrency.Entities;
using Domain.CryptoCurrency.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public sealed class ExchangeService : IExchangeService
{
    private readonly IExchangeRepository _exchangeRepository;
    private readonly ILogger<ExchangeService> _logger;

    public ExchangeService(IExchangeRepository exchangeRepository, ILogger<ExchangeService> logger)
    {
        _exchangeRepository = exchangeRepository;
        _logger = logger;
    }

    public async Task<PaginatedList<ExchangeResponse>> GetPaginatedExchangesAsync(int pageNumber, int pageSize)
    {
        return await _exchangeRepository.GetPaginatedExchangesAsync(pageNumber, pageSize);
    }

    public async Task<ExchangeResponse> GetExchangeByIdAsync(Guid id)
    {
        ExchangeResponse exchange = await _exchangeRepository.GetExchangeByIdAsync(id)
                                    ?? throw new NotFoundException("Exchange with the id specified does not exist.");

        return exchange;
    }

    public async Task UpsertExchangesAsync(List<CreateExchangeRequest> exchanges)
    {
        var names = exchanges.Select(r => r.Name);
        var existingCoins = await _exchangeRepository.GetExistingExchangesAsync(names);

        List<Exchange> exchangesToAdd = [];
        List<Exchange> exchangesToUpdate = [];

        foreach (CreateExchangeRequest exchange in exchanges)
        {
            if (existingCoins.TryGetValue(exchange.Name, out var existingExchange))
            {
                existingExchange.UpdateMarketData(
                    Price.Create(exchange.Volume.Amount, exchange.Volume.Currency),
                    exchange.Country,
                    exchange.Url,
                    exchange.ActivePairs);

                exchangesToUpdate.Add(existingExchange);
            }
            else
            {
                Exchange newExchange = new(
                    exchange.Name,
                    Price.Create(exchange.Volume.Amount, exchange.Volume.Currency),
                    exchange.Country,
                    exchange.Url,
                    exchange.ActivePairs);

                exchangesToAdd.Add(newExchange);
            }
        }

        _exchangeRepository.AddRange(exchangesToAdd);
        _exchangeRepository.UpdateRange(exchangesToUpdate);

        await _exchangeRepository.CommitAsync();

        var insertedExchangeNames = exchangesToAdd.Select(c => c.Name);
        var updatedExchangeNames = exchangesToUpdate.Select(c => c.Name);
        _logger.LogInformation("Inserted exchange names: {}", insertedExchangeNames);
        _logger.LogInformation("Updated exchange names: {}", updatedExchangeNames);
    }
}