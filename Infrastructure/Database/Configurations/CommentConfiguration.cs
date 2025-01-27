using Core.Entities.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

internal class CommentConfiguration : BaseEntityConfiguration<Comment>
{
    public override void Configure(EntityTypeBuilder<Comment> builder)
    {
        base.Configure(builder);

        builder.Property(c => c.Content)
            .IsRequired()
            .HasMaxLength(2000); 

        builder.HasOne(c => c.Post)
            .WithMany() 
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.User)
            .WithMany(u => u.Comments) 
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
