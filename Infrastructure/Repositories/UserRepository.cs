using Core.Entities.Users;
using Core.Interfaces.Repositories;
using Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly DatabaseContext _databaseContext;

    public UserRepository(DatabaseContext dbContext)
        : base(dbContext)
    {
        _databaseContext = dbContext;
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _databaseContext.Users
       .AsNoTracking()
       .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }
}
