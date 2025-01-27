using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

internal abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.CreatedAt).IsRequired().HasDefaultValue(DateTimeOffset.UtcNow);
        builder.Property(x => x.UpdatedAt).IsRequired().HasDefaultValue(DateTimeOffset.UtcNow);
        builder.Property(x => x.Deleted).IsRequired().HasDefaultValue(false);
        builder.HasQueryFilter(x => !x.Deleted);
    }
}
