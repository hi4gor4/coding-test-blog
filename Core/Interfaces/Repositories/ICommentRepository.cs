using Core.Entities.Posts;

namespace Core.Interfaces.Repositories;

public interface ICommentRepository : IBaseRepository<Comment>
{
    Task<Comment> AddCommentAsync(Comment comment, CancellationToken cancellationToken);
    Task<List<Comment>> GetCommentsByPostIdAsync(long postId, CancellationToken cancellationToken);
}
