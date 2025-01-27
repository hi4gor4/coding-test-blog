using Core.Entities.Users;
using Core.Interfaces.Services;
using Core.Services;
using Core.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core;

public static class CoreInjector
{
    public static void RegisterCoreServices(this IServiceCollection services)
    {
        RegisterServices(services);
        RegisterValidators(services);
    }
    private static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPostService, PostService>();
    }

    private static void RegisterValidators(IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
    }
}
