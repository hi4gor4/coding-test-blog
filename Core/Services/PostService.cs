using Ardalis.Result;
using Core.Entities.Posts;
using Core.Entities.Users;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Services;

public class PostService : IPostService
{
    private readonly IUserRepository _userRepository;
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IValidator<User> _createUserValidator;

    public PostService(IUserRepository userRepository,
        IPostRepository postRepository,
        ICommentRepository commentRepository,
        IServiceProvider serviceProvider)
    {
        _userRepository = userRepository;
        _postRepository = postRepository;
        _commentRepository = commentRepository;
        _createUserValidator = serviceProvider.GetService<CreateUserValidator>();

    }

    public async Task<Result> CreatePost(Post post, CancellationToken cancellationToken)
    {
        if(post.Content == null)
            return Result.Error("O Conteudo não pode ser vazio");

       await _postRepository.AddAsync(post, cancellationToken);

        return Result.Success();
    }

    public async Task<List<Post>> GetFeed(CancellationToken cancellationToken)
    {
        var posts = await  _postRepository.GetFeed(cancellationToken);

        foreach (var item in posts)
        {
            item.Comments = await _commentRepository.GetCommentsByPostIdAsync(item.Id, cancellationToken);
        }

        return posts;
    }

    public async Task<Result> DeletePost(long postId, CancellationToken cancellationToken)
    {
        var post = await  _postRepository.GetByIdAsync(postId, cancellationToken);

        if (post == null)
            Result.Error("Post não encontrado");

        await _postRepository.DeleteAsync(post, cancellationToken);

        return Result.Success();
    }
}
