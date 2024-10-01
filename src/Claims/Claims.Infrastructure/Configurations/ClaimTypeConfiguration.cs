using Claims.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arch.Infrastructure.Configurations;


public sealed class ClaimTypeConfiguration : IEntityTypeConfiguration<Claim>
{
    public void Configure(EntityTypeBuilder<Claim> builder)
    {
        builder.ToTable("Claims");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).IsRequired().HasColumnType("uniqueidentifier").ValueGeneratedOnAdd();
        builder.Property(c => c.CoverId).IsRequired();
        builder.Property(c => c.Created).IsRequired();
        builder.Property(c => c.Name).IsRequired().HasMaxLength(100).IsUnicode(true);
        builder.Property(c => c.Type).IsRequired();
        builder.Property(c => c.DamageCost).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(e => e.CreatedAt).IsRequired(true).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
        builder.Property(e => e.ModifiedAt).IsRequired(false).HasColumnType("datetime");
        builder.Property(e => e.IsActive).IsRequired(true);
        builder.Property(e => e.IsDeleted).IsRequired(true);

        builder.HasOne(c => c.Cover)
               .WithMany(c => c.Claims)
               .HasForeignKey(c => c.CoverId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}