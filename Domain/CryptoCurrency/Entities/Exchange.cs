using Domain.Common;
using Domain.CryptoCurrency.ValueObjects;

namespace Domain.CryptoCurrency.Entities;

public sealed class Exchange : BaseEntity, IAggregateRoot
{
	[Obsolete]
	private Exchange()
	{
	}

	public Exchange(string name, string nameId, Money volume, string? country, string url, int activePairs)
	{
		ValidateInvariantsOnCreation(name, nameId, url, activePairs);

		Name = name;
		NameId = nameId;
		Country = string.IsNullOrEmpty(country) ? null : country;
		Volume = volume;
		Url = url;
		ActivePairs = activePairs;
	}

	public string Name { get; private set; } = null!;
	public string NameId { get; private set; } = null!;
	public Money Volume { get; private set; } = null!;
	public string? Country { get; private set; }
	public string? Url { get; private set; }
	public int ActivePairs { get; private set; }

	public void UpdateMarketData(Money volume, string? country, string? url, int activePairs)
	{
		if (!string.IsNullOrEmpty(url?.Trim()) && !Uri.IsWellFormedUriString(url, UriKind.Absolute))
		{
			throw new ArgumentException("Invalid exchange URL.");
		}

		if (activePairs < 0)
		{
			throw new ArgumentException("Invalid active pairs.");
		}

		Volume = volume;
		Country = string.IsNullOrEmpty(country?.Trim()) ? null : country;
		Url = string.IsNullOrEmpty(url) ? null : url;
		ActivePairs = activePairs;
	}

	private static void ValidateInvariantsOnCreation(string name, string nameId, string? url, int activePairs)
	{
		if (string.IsNullOrEmpty(name.Trim()))
		{
			throw new ArgumentException("Invalid exchange name.");
		}

		if (string.IsNullOrEmpty(nameId.Trim()))
		{
			throw new ArgumentException("Invalid exchange name id.");
		}

		if (!string.IsNullOrEmpty(url) && !Uri.IsWellFormedUriString(url, UriKind.Absolute))
		{
			throw new ArgumentException("Invalid exchange URL.");
		}

		if (activePairs < 0)
		{
			throw new ArgumentException("Invalid active pairs.");
		}
	}
}