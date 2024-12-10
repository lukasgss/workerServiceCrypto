using Domain.Common;
using Domain.CryptoCurrency.ValueObjects;

namespace Domain.CryptoCurrency.Entities;

public sealed class Coin : BaseEntity, IAggregateRoot
{
	[Obsolete]
	private Coin()
	{
	}

	private Coin(
		string symbol,
		int rankByMarketCap,
		decimal percentageChangeInOneHour,
		Money money,
		Money marketCap,
		Money tradingVolume)
	{
		Symbol = symbol;
		RankByMarketCap = rankByMarketCap;
		PercentageChangeInOneHour = percentageChangeInOneHour;
		Money = money;
		MarketCap = marketCap;
		TradingVolume = tradingVolume;
	}

	public string Symbol { get; private set; } = null!;
	public int RankByMarketCap { get; private set; }
	public decimal PercentageChangeInOneHour { get; private set; }
	public Money Money { get; private set; } = null!;
	public Money MarketCap { get; private set; } = null!;
	public Money TradingVolume { get; private set; } = null!;

	public static Coin Create(string symbol, int rankByMarketCap, decimal percentageChangeInOneHour,
		Money money, Money marketCap, Money tradingVolume)
	{
		if (string.IsNullOrEmpty(symbol.Trim()))
		{
			throw new ArgumentException("Invalid coin symbol.");
		}

		ValidateRankByMarketCap(rankByMarketCap);

		return new Coin(symbol, rankByMarketCap, percentageChangeInOneHour, money, marketCap, tradingVolume);
	}

	public void UpdateMarketData(Money marketCap, Money tradingVolume, decimal percentageChangeInOneHour,
		int rankByMarketCap)
	{
		ValidateRankByMarketCap(rankByMarketCap);

		MarketCap = marketCap;
		TradingVolume = tradingVolume;
		PercentageChangeInOneHour = percentageChangeInOneHour;
		RankByMarketCap = rankByMarketCap;
	}

	private static void ValidateRankByMarketCap(int rankByMarketCap)
	{
		if (rankByMarketCap < 1)
		{
			throw new ArgumentException("Invalid rank by market cap value.");
		}
	}
}