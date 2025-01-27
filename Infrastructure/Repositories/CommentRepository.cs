using Core.Entities.Posts;
using Core.Entities.Users;
using Core.Interfaces.Repositories;
using Infrastructure.Database.Contexts;

namespace Infrastructure.Repositories;

public class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
    private readonly DatabaseContext _databaseContext;

    public CommentRepository(DatabaseContext dbContext)
        : base(dbContext)
    {
        _databaseContext = dbContext;
    }
}
