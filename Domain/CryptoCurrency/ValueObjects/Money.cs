using Domain.Common;

namespace Domain.CryptoCurrency.ValueObjects;

public sealed class Money : ValueObject
{
	[Obsolete]
	private Money()
	{
	}

	private Money(decimal amount, string code)
	{
		Amount = decimal.Round(amount, 2, MidpointRounding.ToEven);
		Currency = code.ToUpperInvariant();
	}

	public decimal Amount { get; }
	public string Currency { get; } = null!;

	private static readonly HashSet<string> ValidCurrencies = ["USD", "EUR", "GBP", "BRL"];

	public static Money Create(decimal amount, string code)
	{
		if (!ValidCurrencies.Contains(code.ToUpperInvariant()))
		{
			throw new ArgumentException($"Invalid currency code.");
		}

		if (amount < 0)
		{
			throw new ArgumentException("Amount cannot be negative.");
		}

		return new Money(amount, code.ToUpperInvariant());
	}

	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Amount;
		yield return Currency;
	}
}