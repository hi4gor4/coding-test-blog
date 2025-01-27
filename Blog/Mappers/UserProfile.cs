using AutoMapper;
using Core.Entities.Users;

namespace Blog.Mappers;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserModel, User>();
    }
}

