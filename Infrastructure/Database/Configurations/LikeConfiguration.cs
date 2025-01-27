using Core.Entities.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

internal class LikeConfiguration : BaseEntityConfiguration<Like>
{
    public void Configure(EntityTypeBuilder<Like> builder)
    {
        builder.HasKey(l => new { l.PostId, l.UserId });

        builder.HasOne(l => l.Post)
            .WithMany() 
            .HasForeignKey(l => l.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(l => l.User)
            .WithMany() 
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
