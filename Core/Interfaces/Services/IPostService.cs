using Ardalis.Result;
using Core.Entities.Posts;

namespace Core.Interfaces.Services
{
    public interface IPostService
    {
        Task<Result> CreatePost (Post post, CancellationToken cancellationToken);
        Task<List<Post>> GetFeed(CancellationToken cancellationToken);
        Task<Result> DeletePost(long postId, CancellationToken cancellationToken);
    }
}
