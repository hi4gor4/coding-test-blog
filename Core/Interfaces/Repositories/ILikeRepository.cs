using Ardalis.Result;
using Core.Entities.Posts;

namespace Core.Interfaces.Repositories;

public interface ILikeRepository : IBaseRepository<Like>
{
    Task<Like?> GetByIdsAsync(long userId, long postId, CancellationToken cancellationToken);
}
