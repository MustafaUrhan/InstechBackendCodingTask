using Claims.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arch.Infrastructure.Configurations;

public sealed class CoverTypeConfiguration : IEntityTypeConfiguration<Cover>
{
    public void Configure(EntityTypeBuilder<Cover> builder)
    {
        builder.ToTable("Covers");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).IsRequired().HasColumnType("uniqueidentifier").ValueGeneratedOnAdd();; 
        builder.Property(c => c.StartDate).IsRequired().HasColumnType("date"); 
        builder.Property(c => c.EndDate).IsRequired().HasColumnType("date");
        builder.Property(c => c.Type).IsRequired();
        builder.Property(c => c.Premium).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(e => e.CreatedAt).IsRequired(true).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
        builder.Property(e => e.ModifiedAt).IsRequired(false).HasColumnType("datetime");
        builder.Property(e => e.IsActive).IsRequired(true);
        builder.Property(e => e.IsDeleted).IsRequired(true);
    }
}