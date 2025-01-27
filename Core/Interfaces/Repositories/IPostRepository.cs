using Core.Entities.Posts;

namespace Core.Interfaces.Repositories;

public interface IPostRepository : IBaseRepository<Post>
{

    Task<List<Post>> GetFeed(CancellationToken cancellationToken);
}
