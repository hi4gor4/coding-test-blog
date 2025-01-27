using Core.Entities.Posts;
using Core.Entities.Users;
using Core.Interfaces.Repositories;
using Infrastructure.Database.Contexts;

namespace Infrastructure.Repositories;

public class LikeRepository : BaseRepository<Like>, ILikeRepository
{
    private readonly DatabaseContext _databaseContext;

    public LikeRepository(DatabaseContext dbContext)
        : base(dbContext)
    {
        _databaseContext = dbContext;
    }
}
