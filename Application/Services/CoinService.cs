using Application.Common.Exceptions;
using Application.Common.Interfaces.Coins;
using Application.Common.Interfaces.Pagination;
using Domain.CryptoCurrency.Entities;
using Domain.CryptoCurrency.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public sealed class CoinService : ICoinService
{
	private readonly ICoinRepository _coinRepository;
	private readonly ILogger<CoinService> _logger;

	public CoinService(ICoinRepository coinRepository, ILogger<CoinService> logger)
	{
		_coinRepository = coinRepository;
		_logger = logger;
	}

	public async Task<CoinResponse> GetCoinByIdAsync(Guid id)
	{
		CoinResponse coin = await _coinRepository.GetByIdAsync(id)
		                    ?? throw new NotFoundException("Coin with the specified id does not exist.");

		return coin;
	}

	public async Task<PaginatedList<CoinResponse>> GetPaginatedCoinsAsync(int pageNumber, int pageSize)
	{
		return await _coinRepository.GetPaginatedCoinsAsync(pageNumber, pageSize);
	}

	public async Task UpsertCoinsAsync(List<CreateCoinRequest> coins)
	{
		var symbols = coins.Select(r => r.Symbol);
		var existingCoins = await _coinRepository.GetExistingCoinsAsync(symbols);

		List<Coin> coinsToAdd = [];
		List<Coin> coinsToUpdate = [];

		foreach (CreateCoinRequest coin in coins)
		{
			if (existingCoins.TryGetValue(coin.Symbol, out var existingCoin))
			{
				existingCoin.UpdateMarketData(
					Price.Create(coin.MarketCap.Amount, coin.MarketCap.Currency),
					Price.Create(coin.TradingVolume.Amount, coin.TradingVolume.Currency),
					coin.PercentageChangeInOneHour,
					coin.RankByMarketCap);

				coinsToUpdate.Add(existingCoin);
			}
			else
			{
				coinsToAdd.Add(Coin.Create(
					coin.Symbol,
					coin.RankByMarketCap,
					coin.PercentageChangeInOneHour,
					Price.Create(coin.Price.Amount, coin.Price.Currency),
					Price.Create(coin.MarketCap.Amount, coin.MarketCap.Currency),
					Price.Create(coin.TradingVolume.Amount, coin.TradingVolume.Currency)));
			}
		}

		_coinRepository.AddRange(coinsToAdd);
		_coinRepository.UpdateRange(coinsToUpdate);

		await _coinRepository.CommitAsync();

		var insertedCoinsSymbols = coinsToAdd.Select(c => c.Symbol);
		var updatedCoinsSymbols = coinsToUpdate.Select(c => c.Symbol);

		_logger.LogInformation("Inserted coin symbols: {InsertedCoins}", insertedCoinsSymbols);
		_logger.LogInformation("Updated coin symbols: {UpdatedCoins}", updatedCoinsSymbols);
	}
}