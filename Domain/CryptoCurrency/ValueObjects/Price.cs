using Domain.Common;

namespace Domain.CryptoCurrency.ValueObjects;

public sealed class Price : ValueObject
{
    [Obsolete]
    private Price()
    {
    }

    private Price(decimal amount, string code)
    {
        Amount = decimal.Round(amount, 2, MidpointRounding.ToEven);
        Code = code.ToUpperInvariant();
    }

    public decimal Amount { get; }
    public string Code { get; }

    private static readonly HashSet<string> ValidCurrencies = ["USD", "EUR", "GBP", "BRL"];

    public static Price Create(decimal amount, string code)
    {
        if (!ValidCurrencies.Contains(code.ToUpperInvariant()))
        {
            throw new ArgumentException($"Invalid currency code {code}.");
        }

        if (amount < 0)
        {
            throw new ArgumentException("Amount cannot be negative.");
        }

        return new Price(amount, code);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Code;
    }
}