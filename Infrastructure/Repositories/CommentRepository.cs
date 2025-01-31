using Core.Entities.Posts;
using Core.Interfaces.Repositories;
using Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
    private readonly DatabaseContext _databaseContext;

    public CommentRepository(DatabaseContext dbContext)
        : base(dbContext)
    {
        _databaseContext = dbContext;
    }

    public async Task<Comment> AddCommentAsync(Comment comment, CancellationToken cancellationToken)
    {
        var result = await _databaseContext.Comments.AddAsync(comment, cancellationToken);

        await _databaseContext.SaveChangesAsync(cancellationToken);

        return result.Entity;
    }

    public async Task<List<Comment>> GetCommentsByPostIdAsync(long postId, CancellationToken cancellationToken)
    {
        return await _databaseContext.Comments
            .AsNoTracking()
            .Include(c => c.User) 
            .Where(c => c.PostId == postId)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}