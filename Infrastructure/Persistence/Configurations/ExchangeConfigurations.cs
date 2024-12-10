using Domain.CryptoCurrency.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public sealed class ExchangeConfigurations : IEntityTypeConfiguration<Exchange>
{
	public void Configure(EntityTypeBuilder<Exchange> builder)
	{
		builder.Property(e => e.Name)
			.IsRequired()
			.HasMaxLength(80);

		builder.Property(e => e.NameId)
			.IsRequired()
			.HasMaxLength(80);

		builder.ComplexProperty(e => e.Volume, volBuilder =>
		{
			volBuilder.Property(p => p.Amount).IsRequired();
			volBuilder.Property(p => p.Currency)
				.IsRequired()
				.HasMaxLength(3);
		});

		builder.Property(e => e.Country)
			.HasMaxLength(50);

		builder.Property(e => e.Url)
			.HasMaxLength(100);

		builder.Property(e => e.ActivePairs)
			.IsRequired();

		builder.HasIndex(c => c.Name);
	}
}