using Ardalis.Result;
using Core.Entities.Users;

namespace Core.Interfaces.Services;

public interface IUserService
{
    Task<Result<User>> AddAsync(User user, string confirmPassword, CancellationToken cancellationToken);
    Task<Result<User>> LoginAsync(string email, string password, CancellationToken cancellationToken);

}
