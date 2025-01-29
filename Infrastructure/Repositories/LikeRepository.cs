using Core.Entities.Posts;
using Core.Entities.Users;
using Core.Interfaces.Repositories;
using Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class LikeRepository : BaseRepository<Like>, ILikeRepository
{
    private readonly DatabaseContext _databaseContext;

    public LikeRepository(DatabaseContext dbContext)
        : base(dbContext)
    {
        _databaseContext = dbContext;
    }

    public Task<Like?> GetByIdsAsync(long userId, long postId, CancellationToken cancellationToken)
    {
        return _databaseContext
            .Likes
            .Where(x => x.UserId == userId && x.PostId == postId)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
