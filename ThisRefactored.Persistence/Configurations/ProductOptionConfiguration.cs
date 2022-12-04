using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThisRefactored.Domain.Entities;

namespace ThisRefactored.Persistence.Configurations;

public class ProductOptionConfiguration : IEntityTypeConfiguration<ProductOption>
{
    public void Configure(EntityTypeBuilder<ProductOption> builder)
    {
        builder.Property(x => x.Id)
               .IsRequired();
        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(VarCharPropertyLengths.Name);
        builder.Property(x => x.Description)
               .IsRequired()
               .HasMaxLength(VarCharPropertyLengths.Description);
    }
}