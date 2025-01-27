using AutoMapper;
using Core.Entities.Posts;
using Core.Entities.Users;

namespace Blog.Mappers;
public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<PostModel, Post>().ReverseMap();
    }
}

