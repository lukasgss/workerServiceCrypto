using Domain.Common;
using Domain.CryptoCurrency.ValueObjects;

namespace Domain.CryptoCurrency.Entities;

public sealed class Exchange : BaseEntity, IAggregateRoot
{
    [Obsolete]
    private Exchange()
    {
    }

    public Exchange(string name, Price volume, string country, string url, int activePairs)
    {
        ValidateInvariantsOnCreation(name, country, url, activePairs);

        Name = name;
        Volume = volume;
        Country = country;
        Url = url;
        ActivePairs = activePairs;
    }

    public string Name { get; private set; }
    public Price Volume { get; private set; }
    public string Country { get; private set; }
    public string Url { get; private set; }
    public int ActivePairs { get; private set; }

    public void UpdateMarketData(Price volume, string country, string url, int activePairs)
    {
        if (string.IsNullOrEmpty(country.Trim()))
        {
            throw new ArgumentException("Invalid country name.");
        }

        if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
        {
            throw new ArgumentException("Invalid exchange URL.");
        }

        if (activePairs < 0)
        {
            throw new ArgumentException("Invalid active pairs.");
        }

        Volume = volume;
        Country = country;
        Url = url;
        ActivePairs = activePairs;
    }

    private static void ValidateInvariantsOnCreation(string name, string country, string url, int activePairs)
    {
        if (string.IsNullOrEmpty(name.Trim()))
        {
            throw new ArgumentException("Invalid exchange name.");
        }

        if (string.IsNullOrEmpty(country.Trim()))
        {
            throw new ArgumentException("Invalid country name.");
        }

        if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
        {
            throw new ArgumentException("Invalid exchange URL.");
        }

        if (activePairs < 0)
        {
            throw new ArgumentException("Invalid active pairs.");
        }
    }
}