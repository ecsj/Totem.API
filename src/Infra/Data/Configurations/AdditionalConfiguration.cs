using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Configurations;

public class AdditionalConfiguration : IEntityTypeConfiguration<Additional>
{
    public void Configure(EntityTypeBuilder<Additional> builder)
    {
        builder.HasKey(a => a.ProductId);
        builder.Property(a => a.ProductId);

        builder.Property(a => a.Price)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        builder.HasOne(a => a.Product);
    }
}