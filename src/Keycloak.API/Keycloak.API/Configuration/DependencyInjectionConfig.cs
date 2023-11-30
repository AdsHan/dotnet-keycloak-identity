using Keycloak.API.Application.Messages.Commands.User;
using Keycloak.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Keycloak.API.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection AddDependencyConfiguration(this IServiceCollection services)
    {
        services.AddDbContext<KeycloakDbContext>(options => options.UseInMemoryDatabase("UsersDB"));

        services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly);
        });

        return services;
    }
}
