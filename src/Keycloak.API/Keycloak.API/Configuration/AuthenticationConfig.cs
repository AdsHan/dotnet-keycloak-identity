using Keycloak.API.Authentication.Configuration;
using Keycloak.API.Authentication.Interfaces;
using Keycloak.API.Authentication.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Keycloak.API.Configuration;

public static class AuthenticationConfig
{
    public static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<KeycloakConfigurations>(configuration.GetSection("Keycloak"));

        var authConfigurations = new AuthConfigurations();
        new ConfigureFromConfigurationOptions<AuthConfigurations>(configuration.GetSection("Authentication")).Configure(authConfigurations);

        services.AddSingleton(authConfigurations);

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    SaveSigninToken = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidAudience = authConfigurations.Audience,
                    ValidIssuer = authConfigurations.ValidIssuer,
                };
                options.MetadataAddress = authConfigurations.MetadataUrl;
                options.RequireHttpsMetadata = authConfigurations.RequireHttpsMetadata;
            });

        services.AddTransient<KeycloakDelegatingHandler>();

        services.AddHttpClient<IKeycloakService, KeycloakService>().AddHttpMessageHandler<KeycloakDelegatingHandler>();

        services.AddHttpClient<ITokenService, TokenService>();

        return services;
    }

    public static IApplicationBuilder UseAuthenticationConfiguration(this IApplicationBuilder app)
    {
        app.UseAuthentication();

        app.UseAuthorization();

        return app;
    }
}
