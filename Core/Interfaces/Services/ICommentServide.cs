using Ardalis.Result;
using Core.Entities.Posts;

namespace Core.Interfaces.Services;

public interface ICommentService
{
    Task<Result<Comment>> AddCommentAsync(long postId, long userId, string content, CancellationToken cancellationToken);
}
