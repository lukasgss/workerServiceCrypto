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

        builder.ComplexProperty(e => e.Volume, volBuilder =>
        {
            volBuilder.Property(p => p.Amount).IsRequired();
            volBuilder.Property(p => p.Code)
                .IsRequired()
                .HasMaxLength(3);
        });

        builder.Property(e => e.Country)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Url)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.ActivePairs)
            .IsRequired();

        builder.HasIndex(c => c.Name);
    }
}