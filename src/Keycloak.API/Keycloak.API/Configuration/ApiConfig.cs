
using FluentValidation.AspNetCore;
using Keycloak.API.Application.Messages.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Keycloak.API.Configuration;

public static class ApiConfig
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<CreateUserValidator>());

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var messagesErros = context.ModelState.SelectMany(ms => ms.Value.Errors).Select(e => e.ErrorMessage).ToList();

                return new BadRequestObjectResult(new
                {
                    Title = "Invalid arguments to the API",
                    Status = 400,
                    Errors = new { Messages = messagesErros }
                });
            };
        });

        services.AddEndpointsApiExplorer();

        services.AddCors(options =>
        {
            options.AddPolicy("Total",
                builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
        });

        return services;
    }

    public static WebApplication UseApiConfiguration(this WebApplication app)
    {
        app.UseHttpsRedirection();

        app.MapControllers();

        return app;
    }
}



