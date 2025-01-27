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
    private readonly IValidator<User> _createUserValidator;

    public PostService(IUserRepository userRepository,
        IPostRepository postRepository,
        IServiceProvider serviceProvider)
    {
        _userRepository = userRepository;
        _postRepository = postRepository;
        _createUserValidator = serviceProvider.GetService<CreateUserValidator>();

    }

    public async Task<Result> CreatePost(Post post, CancellationToken cancellationToken)
    {
        if(post.Content == null)
            return Result.Error("O Conteudo não pode ser vazio");

       await _postRepository.AddAsync(post, cancellationToken);

        return Result.Success();
    }

    public Task<List<Post>> GetFeed(CancellationToken cancellationToken)
    {
        return _postRepository.GetFeed(cancellationToken);
    }
}
