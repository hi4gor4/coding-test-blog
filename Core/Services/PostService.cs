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
    public async Task<Result<User>> AddAsync(User user, string confirmPassword, CancellationToken cancellationToken)
    {

        if (user.Password != confirmPassword)
            return Result<User>.Error("As senhas devem ser iguais Invalida");

        var validateUser = _createUserValidator.Validate(user);

        if (!validateUser.IsValid)
            return Result<User>.Error(validateUser.Errors.First().ErrorMessage);

        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

        var resultUser = await _userRepository.AddAsync(user, cancellationToken);

        return Result<User>.Success(resultUser);
    }

    public async Task<Result> CreatePost(Post post, CancellationToken cancellationToken)
    {
        if(post.Content == null)
            return Result.Error("O Conteudo não pode ser vazio");

       await _postRepository.AddAsync(post, cancellationToken);

        return Result.Success();
    }
}
