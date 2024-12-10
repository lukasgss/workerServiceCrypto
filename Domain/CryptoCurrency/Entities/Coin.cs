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
        Price price,
        Price marketCap,
        Price tradingVolume)
    {
        Symbol = symbol;
        RankByMarketCap = rankByMarketCap;
        PercentageChangeInOneHour = percentageChangeInOneHour;
        Price = price;
        MarketCap = marketCap;
        TradingVolume = tradingVolume;
    }

    public string Symbol { get; private set; }
    public int RankByMarketCap { get; private set; }
    public decimal PercentageChangeInOneHour { get; private set; }
    public Price Price { get; private set; }
    public Price MarketCap { get; private set; }
    public Price TradingVolume { get; private set; }

    public static Coin Create(string symbol, int rankByMarketCap, decimal percentageChangeInOneHour,
        Price price, Price marketCap, Price tradingVolume)
    {
        if (string.IsNullOrEmpty(symbol.Trim()))
        {
            throw new ArgumentException("Invalid coin symbol.");
        }

        ValidateRankByMarketCap(rankByMarketCap);

        return new Coin(symbol, rankByMarketCap, percentageChangeInOneHour, price, marketCap, tradingVolume);
    }

    public void UpdateMarketData(Price marketCap, Price tradingVolume, decimal percentageChangeInOneHour,
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