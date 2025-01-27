using Core.Interfaces.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Ifrastructure;

public static class InfrastructureInjector
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
    {
        RegisterRepositories(services);
        return services;
    }

    private static void RegisterRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
    }
}