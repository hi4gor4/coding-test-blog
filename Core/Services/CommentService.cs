using Ardalis.Result;
using Core.Entities.Posts;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Validators; // Se você tiver validadores para comentários
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;

    public CommentService(ICommentRepository commentRepository,
        IServiceProvider serviceProvider)
    {
        _commentRepository = commentRepository;
    }

    public async Task<Result<Comment>> AddCommentAsync(long postId, long userId, string content, CancellationToken cancellationToken)
    {
        var comment = new Comment
        {
            PostId = postId,
            UserId = userId,
            Content = content,
            CreatedAt = DateTimeOffset.Now
        };


        var result = await _commentRepository.AddCommentAsync(comment, cancellationToken);

        return Result<Comment>.Success(result);
    }
}