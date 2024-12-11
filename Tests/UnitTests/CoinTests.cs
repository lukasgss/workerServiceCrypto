using Domain.CryptoCurrency.Entities;
using Domain.CryptoCurrency.ValueObjects;

namespace Tests.UnitTests;

public sealed class CoinTests
{
	private readonly Money _defaultMoney = Money.Create(100, "USD");
	private readonly Money _defaultMarketCap = Money.Create(1000000, "USD");
	private readonly Money _defaultTradingVolume = Money.Create(500000, "USD");

	[Fact]
	public void Create_WithValidParameters_ShouldCreateCoin()
	{
		string symbol = "BTC";
		int rankByMarketCap = 1;
		decimal percentageChange = 5.5m;

		Coin coin = Coin.Create(
			symbol,
			rankByMarketCap,
			percentageChange,
			_defaultMoney,
			_defaultMarketCap,
			_defaultTradingVolume);

		Assert.NotNull(coin);
		Assert.Equal(symbol, coin.Symbol);
		Assert.Equal(rankByMarketCap, coin.RankByMarketCap);
		Assert.Equal(percentageChange, coin.PercentageChangeInOneHour);
		Assert.Equal(_defaultMoney, coin.Money);
		Assert.Equal(_defaultMarketCap, coin.MarketCap);
		Assert.Equal(_defaultTradingVolume, coin.TradingVolume);
	}

	[Theory]
	[InlineData("")]
	[InlineData(" ")]
	public void Create_WithInvalidSymbol_ShouldThrowArgumentException(string invalidSymbol)
	{
		ArgumentException exception = Assert.Throws<ArgumentException>(() => Coin.Create(
			invalidSymbol,
			1,
			5.5m,
			_defaultMoney,
			_defaultMarketCap,
			_defaultTradingVolume));

		Assert.Equal("Invalid coin symbol.", exception.Message);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(-1)]
	[InlineData(-100)]
	public void Create_WithInvalidRank_ShouldThrowArgumentException(int invalidRank)
	{
		ArgumentException exception = Assert.Throws<ArgumentException>(() => Coin.Create(
			"BTC",
			invalidRank,
			5.5m,
			_defaultMoney,
			_defaultMarketCap,
			_defaultTradingVolume));

		Assert.Equal("Invalid rank by market cap value.", exception.Message);
	}

	[Fact]
	public void UpdateMarketData_WithValidParameters_ShouldUpdateProperties()
	{
		Coin coin = Coin.Create(
			"BTC",
			1,
			5.5m,
			_defaultMoney,
			_defaultMarketCap,
			_defaultTradingVolume);

		Money newMarketCap = Money.Create(2000000, "USD");
		Money newTradingVolume = Money.Create(750000, "USD");
		decimal newPercentageChange = 6.7m;
		int newRank = 2;

		coin.UpdateMarketData(newMarketCap, newTradingVolume, newPercentageChange, newRank);

		Assert.Equal(newMarketCap, coin.MarketCap);
		Assert.Equal(newTradingVolume, coin.TradingVolume);
		Assert.Equal(newPercentageChange, coin.PercentageChangeInOneHour);
		Assert.Equal(newRank, coin.RankByMarketCap);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(-1)]
	[InlineData(-100)]
	public void UpdateMarketData_WithInvalidRank_ShouldThrowArgumentException(int invalidRank)
	{
		Coin coin = Coin.Create(
			"BTC",
			1,
			5.5m,
			_defaultMoney,
			_defaultMarketCap,
			_defaultTradingVolume);

		ArgumentException exception = Assert.Throws<ArgumentException>(() =>
			coin.UpdateMarketData(
				_defaultMarketCap,
				_defaultTradingVolume,
				5.5m,
				invalidRank));

		Assert.Equal("Invalid rank by market cap value.", exception.Message);
	}
}