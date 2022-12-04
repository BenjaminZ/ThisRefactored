using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThisRefactored.Domain.Entities;

namespace ThisRefactored.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.Id)
               .IsRequired();
        builder.Property(x => x.Description)
               .IsRequired()
               .HasMaxLength(VarCharPropertyLengths.Description);
        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(VarCharPropertyLengths.Name);
        builder.Property(x => x.Price)
               .IsRequired();
        builder.Property(x => x.DeliveryPrice)
               .IsRequired();

        builder.HasMany(x => x.ProductOptions)
               .WithOne(x => x.Product)
               .HasForeignKey(x => x.ProductId);
    }
}