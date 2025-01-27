using Core.Entities.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

internal class UserConfiguration : BaseEntityConfiguration<User> 
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder); 


        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(255); 

        builder.Property(u => u.Surname)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(255); 

        builder.Property(u => u.Phone)
            .HasMaxLength(20);

        builder.Property(u => u.Document)
            .HasMaxLength(20);

        builder.Property(u => u.BirthDate)
            .IsRequired();
    }
}
