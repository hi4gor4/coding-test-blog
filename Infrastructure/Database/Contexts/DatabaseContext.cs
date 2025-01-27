using Core.Entities.Posts;
using Core.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Contexts;



public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> opts) : base(opts)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Like> Likes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
