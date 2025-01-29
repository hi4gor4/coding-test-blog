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

    public async Task AddLikeAsync(long userId, long postId, CancellationToken cancellationToken)
    {
        var post = await _databaseContext.Posts.FindAsync(new object[] { postId }, cancellationToken);
        if (post == null)
        {
            throw new ArgumentException("Post não encontrado.");
        }

        var user = await _databaseContext.Users.FindAsync(new object[] { userId }, cancellationToken);
        if (user == null)
        {
            throw new ArgumentException("Usuário não encontrado.");
        }

        var existingLike = await GetByIdsAsync(userId, postId, cancellationToken);

        if (existingLike != null)
        {
            existingLike.Deleted = !existingLike.Deleted; 
            existingLike.UpdatedAt = DateTimeOffset.Now; 
        }
        else
        {
            var like = new Like
            {
                PostId = postId,
                UserId = userId,
                Post = post,
                User = user,
            };

            await _databaseContext.Likes.AddAsync(like, cancellationToken);
        }

        await _databaseContext.SaveChangesAsync(cancellationToken);
    }
}
