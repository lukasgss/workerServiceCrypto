using Domain.CryptoCurrency.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public sealed class CoinConfigurations : IEntityTypeConfiguration<Coin>
{
	public void Configure(EntityTypeBuilder<Coin> builder)
	{
		builder.Property(c => c.Symbol)
			.IsRequired()
			.HasMaxLength(10);

		builder.Property(c => c.RankByMarketCap)
			.IsRequired();

		builder.Property(c => c.PercentageChangeInOneHour)
			.IsRequired()
			.HasPrecision(10, 2);

		builder.ComplexProperty(c => c.Money, priceBuilder =>
		{
			priceBuilder.Property(p => p.Amount).IsRequired();
			priceBuilder.Property(p => p.Currency)
				.IsRequired()
				.HasMaxLength(3);
		});

		builder.ComplexProperty(c => c.MarketCap, mcBuilder =>
		{
			mcBuilder.Property(p => p.Amount).IsRequired();
			mcBuilder.Property(p => p.Currency)
				.IsRequired()
				.HasMaxLength(3);
		});

		builder.ComplexProperty(c => c.TradingVolume, tvBuilder =>
		{
			tvBuilder.Property(p => p.Amount).IsRequired();
			tvBuilder.Property(p => p.Currency)
				.IsRequired()
				.HasMaxLength(3);
		});

		builder.HasIndex(c => c.Symbol);
	}
}