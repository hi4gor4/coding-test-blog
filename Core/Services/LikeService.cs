using Ardalis.Result;
using Core.Entities.Posts;
using Core.Entities.Users;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services;

public class LikeService : ILikeService
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILikeRepository _likeRepository;

    public LikeService(ILikeRepository likeRepository,
        IUserRepository userRepository,
        IPostRepository postRepository)
    {
        _likeRepository = likeRepository;
        _userRepository = userRepository;
        _postRepository = postRepository;   
    }
    public async Task<Result> LikeOrDeslike(long userId, long postId, CancellationToken cancellationToken)
    {
        try
        {
            await _likeRepository.AddLikeAsync(userId, postId, cancellationToken);
            return Result.Success();
        }
        catch 
        {
            return Result.Error();
        }


    }
}
