using Domain.CryptoCurrency.ValueObjects;

namespace Tests.UnitTests;

using Xunit;

public sealed class MoneyTests
{
	[Theory]
	[InlineData(100.00, "USD")]
	[InlineData(50.50, "EUR")]
	[InlineData(75.99, "GBP")]
	[InlineData(200.00, "BRL")]
	public void Create_WithValidParameters_ShouldCreateMoney(decimal amount, string currency)
	{
		Money money = Money.Create(amount, currency);

		Assert.Equal(amount, money.Amount);
		Assert.Equal(currency.ToUpperInvariant(), money.Currency);
	}

	[Theory]
	[InlineData("JPY")]
	[InlineData("CAD")]
	[InlineData("")]
	public void Create_WithInvalidCurrency_ShouldThrowArgumentException(string invalidCurrency)
	{
		ArgumentException exception = Assert.Throws<ArgumentException>(() =>
			Money.Create(100, invalidCurrency));

		Assert.Equal("Invalid currency code.", exception.Message);
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(-100.50)]
	[InlineData(-999.99)]
	public void Create_WithNegativeAmount_ShouldThrowArgumentException(decimal negativeAmount)
	{
		ArgumentException exception = Assert.Throws<ArgumentException>(() =>
			Money.Create(negativeAmount, "USD"));

		Assert.Equal("Amount cannot be negative.", exception.Message);
	}

	[Fact]
	public void Equality_WithSameValues_ShouldBeEqual()
	{
		Money money1 = Money.Create(100.00m, "USD");
		Money money2 = Money.Create(100.00m, "USD");

		Assert.Equal(money1, money2);
		Assert.True(money1.Equals(money2));
	}

	[Fact]
	public void Equality_WithDifferentValues_ShouldNotBeEqual()
	{
		Money money1 = Money.Create(100.00m, "USD");
		Money money2 = Money.Create(100.00m, "EUR");
		Money money3 = Money.Create(200.00m, "USD");

		Assert.NotEqual(money1, money2);
		Assert.NotEqual(money1, money3);
		Assert.False(money1.Equals(money2));
		Assert.False(money1.Equals(money3));
	}

	[Fact]
	public void GetHashCode_WithSameValues_ShouldBeEqual()
	{
		Money money1 = Money.Create(100.00m, "USD");
		Money money2 = Money.Create(100.00m, "USD");

		Assert.Equal(money1.GetHashCode(), money2.GetHashCode());
	}

	[Fact]
	public void GetHashCode_WithDifferentValues_ShouldNotBeEqual()
	{
		Money money1 = Money.Create(100.00m, "USD");
		Money money2 = Money.Create(100.00m, "EUR");

		Assert.NotEqual(money1.GetHashCode(), money2.GetHashCode());
	}

	[Theory]
	[InlineData("usd", "USD")]
	[InlineData("eur", "EUR")]
	[InlineData("GbP", "GBP")]
	[InlineData("brl", "BRL")]
	public void Create_WithDifferentCaseCurrency_ShouldNormalizeToCaps(string input, string expected)
	{
		Money money = Money.Create(100, input);

		Assert.Equal(expected, money.Currency);
	}
}