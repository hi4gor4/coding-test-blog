using Ardalis.Result;
using Core.Entities.Posts;

namespace Core.Interfaces.Services
{
    public interface IPostService
    {
        Task<Result> CreatePost (Post post, CancellationToken cancellationToken);
    }
}
