using Ardalis.Result;
using Core.Entities.Users;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<User> _createUserValidator;

    public UserService(IUserRepository userRepository,
        IServiceProvider serviceProvider)
    {
        _userRepository = userRepository;
        _createUserValidator = serviceProvider.GetService<CreateUserValidator>();
        
    }
    public async Task<Result<User>> AddAsync(User user, string confirmPassword, CancellationToken cancellationToken)
    {

        if (user.Password != confirmPassword)
            return Result<User>.Error("As senhas devem ser iguais Invalida");

       var validateUser = _createUserValidator.Validate(user);

        if (!validateUser.IsValid)
            return Result<User>.Error(validateUser.Errors.First().ErrorMessage);

        user.Password =  BCrypt.Net.BCrypt.HashPassword(user.Password);

        var resultUser = await _userRepository.AddAsync(user, cancellationToken);

        return Result<User>.Success(resultUser);
    }

    public async Task<Result<User>> LoginAsync(string email, string password, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(email, cancellationToken);

        if (user == null)
            return Result<User>.Error("Usuario não encontrado");

        if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            return Result<User>.Error("Senha inválida");

        return Result<User>.Success(user);
    }
}
