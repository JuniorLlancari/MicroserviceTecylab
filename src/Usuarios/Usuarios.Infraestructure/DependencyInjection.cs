using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Usuarios.Application.Abstractions.Data;
using Usuarios.Application.Abstractions.Email;
using Usuarios.Application.Abstractions.Time;
using Usuarios.Application.Events.Docente;
using Usuarios.Application.Events.Usuario;
using Usuarios.Application.Jwt;
using Usuarios.Application.Services;
using Usuarios.Domain.Abstractions;
using Usuarios.Domain.Roles;
using Usuarios.Domain.Usuarios;
using Usuarios.Infraestructure.Data;
using Usuarios.Infraestructure.Email;
using Usuarios.Infraestructure.Jwt;
using Usuarios.Infraestructure.Repositories;
using Usuarios.Infraestructure.Time;
using Usuarios.Infrastructure.services;

namespace Usuarios.Infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IEmailService, EmailService>(); // tiempo de vida corto

        var connectionString = configuration.GetConnectionString("Database")
        ?? throw new ArgumentNullException(nameof(configuration));

        var urlRabbit = configuration["UrlRabbit"];

        services.AddDbContext<ApplicationDbContext>(
            options =>
            {
                options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
            });

        services.AddSingleton<IEventBus, RabbitMQEventBus>(_ => new RabbitMQEventBus(urlRabbit!, services.BuildServiceProvider().GetRequiredService<IPublisher>()));
        services.AddScoped<INotificationHandler<UserDocenteCreateEvent>, UserDocenteCreateEventHandler>();
        services.AddScoped<INotificationHandler<DocenteFailCreatedEvent>, DocenteFailCreatedEventHandler>();

        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IRolRepository, RolRepository>(); // tiempo de vida intermedio

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IAutorizationService, AutorizacionService>();

        // tiempo de vida largo (por aplicacion)
        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));

        services.addJwtConfiguration(configuration);

        services.AddHostedService<EventBusBackgroundService>();


        return services;


    }
}