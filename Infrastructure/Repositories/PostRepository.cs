using Core.Entities.Posts;
using Core.Entities.Users;
using Core.Interfaces.Repositories;
using Infrastructure.Database.Contexts;

namespace Infrastructure.Repositories;

public class PostRepository : BaseRepository<Post>, IPostRepository
{
    private readonly DatabaseContext _databaseContext;

    public PostRepository(DatabaseContext dbContext)
        : base(dbContext)
    {
        _databaseContext = dbContext;
    }
}
