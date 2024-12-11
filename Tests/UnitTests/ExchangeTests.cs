using Domain.CryptoCurrency.Entities;
using Domain.CryptoCurrency.ValueObjects;

namespace Tests.UnitTests;

using Xunit;

public sealed class ExchangeTests
{
	private const string ValidName = "Binance";
	private const string ValidNameId = "binance";
	private readonly Money _defaultVolume = Money.Create(1000000, "USD");
	private const string ValidCountry = "Malta";
	private const string ValidUrl = "https://binance.com";
	private const int ValidActivePairs = 100;

	[Fact]
	public void Constructor_WithValidParameters_ShouldCreateExchange()
	{
		Exchange exchange = new(
			ValidName,
			ValidNameId,
			_defaultVolume,
			ValidCountry,
			ValidUrl,
			ValidActivePairs);

		Assert.Equal(ValidName, exchange.Name);
		Assert.Equal(ValidNameId, exchange.NameId);
		Assert.Equal(_defaultVolume, exchange.Volume);
		Assert.Equal(ValidCountry, exchange.Country);
		Assert.Equal(ValidUrl, exchange.Url);
		Assert.Equal(ValidActivePairs, exchange.ActivePairs);
	}

	[Theory]
	[InlineData("")]
	[InlineData(" ")]
	public void Constructor_WithInvalidName_ShouldThrowArgumentException(string invalidName)
	{
		ArgumentException exception = Assert.Throws<ArgumentException>(() => new Exchange(
			invalidName,
			ValidNameId,
			_defaultVolume,
			ValidCountry,
			ValidUrl,
			ValidActivePairs));

		Assert.Equal("Invalid exchange name.", exception.Message);
	}

	[Theory]
	[InlineData("")]
	[InlineData(" ")]
	public void Constructor_WithInvalidNameId_ShouldThrowArgumentException(string invalidNameId)
	{
		ArgumentException exception = Assert.Throws<ArgumentException>(() => new Exchange(
			ValidName,
			invalidNameId,
			_defaultVolume,
			ValidCountry,
			ValidUrl,
			ValidActivePairs));

		Assert.Equal("Invalid exchange name id.", exception.Message);
	}

	[Theory]
	[InlineData("not-valid-url")]
	public void Constructor_WithInvalidUrl_ShouldThrowArgumentException(string invalidUrl)
	{
		ArgumentException exception = Assert.Throws<ArgumentException>(() => new Exchange(
			ValidName,
			ValidNameId,
			_defaultVolume,
			ValidCountry,
			invalidUrl,
			ValidActivePairs));

		Assert.Equal("Invalid exchange URL.", exception.Message);
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(-100)]
	public void Constructor_WithNegativeActivePairs_ShouldThrowArgumentException(int invalidActivePairs)
	{
		ArgumentException exception = Assert.Throws<ArgumentException>(() => new Exchange(
			ValidName,
			ValidNameId,
			_defaultVolume,
			ValidCountry,
			ValidUrl,
			invalidActivePairs));

		Assert.Equal("Invalid active pairs.", exception.Message);
	}

	[Fact]
	public void Constructor_WithNullCountry_ShouldSetCountryToNull()
	{
		Exchange exchange = new(
			ValidName,
			ValidNameId,
			_defaultVolume,
			null,
			ValidUrl,
			ValidActivePairs);

		Assert.Null(exchange.Country);
	}

	[Fact]
	public void Constructor_WithEmptyCountry_ShouldSetCountryToNull()
	{
		Exchange exchange = new(
			ValidName,
			ValidNameId,
			_defaultVolume,
			string.Empty,
			ValidUrl,
			ValidActivePairs);

		Assert.Null(exchange.Country);
	}

	[Fact]
	public void UpdateMarketData_WithValidParameters_ShouldUpdateProperties()
	{
		Exchange exchange = new(
			ValidName,
			ValidNameId,
			_defaultVolume,
			ValidCountry,
			ValidUrl,
			ValidActivePairs);

		Money newVolume = Money.Create(2000000, "USD");
		string newCountry = "Singapore";
		string newUrl = "https://newbinance.com";
		int newActivePairs = 200;

		exchange.UpdateMarketData(newVolume, newCountry, newUrl, newActivePairs);

		Assert.Equal(newVolume, exchange.Volume);
		Assert.Equal(newCountry, exchange.Country);
		Assert.Equal(newUrl, exchange.Url);
		Assert.Equal(newActivePairs, exchange.ActivePairs);
	}

	[Theory]
	[InlineData("not-a-url")]
	public void UpdateMarketData_WithInvalidUrl_ShouldThrowArgumentException(string invalidUrl)
	{
		Exchange exchange = new(
			ValidName,
			ValidNameId,
			_defaultVolume,
			ValidCountry,
			ValidUrl,
			ValidActivePairs);

		ArgumentException exception = Assert.Throws<ArgumentException>(() =>
			exchange.UpdateMarketData(_defaultVolume, ValidCountry, invalidUrl, ValidActivePairs));

		Assert.Equal("Invalid exchange URL.", exception.Message);
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(-100)]
	public void UpdateMarketData_WithNegativeActivePairs_ShouldThrowArgumentException(int invalidActivePairs)
	{
		Exchange exchange = new(
			ValidName,
			ValidNameId,
			_defaultVolume,
			ValidCountry,
			ValidUrl,
			ValidActivePairs);

		ArgumentException exception = Assert.Throws<ArgumentException>(() =>
			exchange.UpdateMarketData(_defaultVolume, ValidCountry, ValidUrl, invalidActivePairs));

		Assert.Equal("Invalid active pairs.", exception.Message);
	}

	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData(" ")]
	public void UpdateMarketData_WithNullOrEmptyCountry_ShouldSetCountryToNull(string? country)
	{
		Exchange exchange = new Exchange(
			ValidName,
			ValidNameId,
			_defaultVolume,
			ValidCountry,
			ValidUrl,
			ValidActivePairs);

		exchange.UpdateMarketData(_defaultVolume, country, ValidUrl, ValidActivePairs);

		Assert.Null(exchange.Country);
	}

	[Theory]
	[InlineData(null)]
	[InlineData("")]
	public void UpdateMarketData_WithNullOrEmptyUrl_ShouldSetUrlToNull(string? url)
	{
		Exchange exchange = new(
			ValidName,
			ValidNameId,
			_defaultVolume,
			ValidCountry,
			ValidUrl,
			ValidActivePairs);

		exchange.UpdateMarketData(_defaultVolume, ValidCountry, url, ValidActivePairs);

		Assert.Null(exchange.Url);
	}
}