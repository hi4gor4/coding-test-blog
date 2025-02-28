﻿using Core.Entities.Posts;
using Core.Entities.Users;
using Core.Interfaces.Repositories;
using Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PostRepository : BaseRepository<Post>, IPostRepository
{
    private readonly DatabaseContext _databaseContext;

    public PostRepository(DatabaseContext dbContext)
        : base(dbContext)
    {
        _databaseContext = dbContext;
    }

    public async Task<List<Post>> GetFeed(CancellationToken cancellationToken)
    {
        return await _databaseContext.Posts
            .AsNoTracking()
            .Include(x => x.User)
            .Include(x => x.Comments)
                .ThenInclude(c => c.User)
            .Include(x => x.Likes)
            .OrderByDescending(x => x.CreatedAt)
            .Where(x => !x.Deleted)
            .ToListAsync(cancellationToken);
    }
}
