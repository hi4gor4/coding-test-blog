using Ardalis.Result;

namespace Core.Interfaces.Services;

public interface ILikeService
{
    Task<Result> LikeOrDeslike(long userId, long postId, CancellationToken cancellationToken);
}
