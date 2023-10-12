
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Models
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(p => p.ID);
            builder.Property(p => p.ID).ValueGeneratedOnAdd();

            builder.Property(p => p.Name).HasMaxLength(1000).IsRequired();
            builder.Property(p => p.Description).HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(p => p.Quantity).IsRequired().HasDefaultValue(1);
            builder.Property(p => p.Price).IsRequired();
            
            builder
            .HasOne(p => p.Category)
            .WithMany(e => e.Products)
            .HasForeignKey(e => e.CategoryID)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        }
    }
}
