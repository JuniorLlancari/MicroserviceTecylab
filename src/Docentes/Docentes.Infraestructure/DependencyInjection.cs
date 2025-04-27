using Docentes.Application.Events.Usuarios;
using Docentes.Application.Services;
using Docentes.Domain.Abstractions;
using Docentes.Domain.CursosImpartidos;
using Docentes.Domain.Docentes;
using Docentes.Infrastructure.Repositories;
using Docentes.Infrastructure.services;
using Docentes.Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Docentes.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
       this IServiceCollection services,
       IConfiguration configuration
   )
    {
        var connectionStringPostgres = configuration.GetConnectionString("Database")
        ?? throw new ArgumentNullException(nameof(configuration));

        var connectionStringRedis = configuration.GetConnectionString("redis")
        ?? throw new ArgumentNullException(nameof(configuration));

        var usuarioApiBaseUrl = configuration["UsuarioApiBaseUrl"];
        var cursosApiBaseUrl = configuration["CursosApiBaseUrl"];
        var urlRabbit = configuration["UrlRabbit"];

        services.AddDbContext<ApplicationDbContext>(
            options =>
            {
                options.UseNpgsql(connectionStringPostgres).UseSnakeCaseNamingConvention(); // usuario, producto_detalle
            }
        );

        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var configurationRedis = ConfigurationOptions.Parse(connectionStringRedis);
            return ConnectionMultiplexer.Connect(configurationRedis);
        });

        services.AddSingleton<IEventBus, RabbitMQEventBus>(_ => new RabbitMQEventBus(urlRabbit!, services.BuildServiceProvider().GetRequiredService<IPublisher>()));
        services.AddScoped<INotificationHandler<UserDocenteCreatedEvent>, UserDocenteCreatedEventHandler>();

        services.AddScoped<IDocenteRepository, DocenteRepository>();
        services.AddScoped<ICursoImpartidoRepository, CursoImpartidoRepository>();
        services.AddScoped<ICacheService, CacheService>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddHttpClient<IUsuarioService, UsuarioService>(client =>
        {
            client.BaseAddress = new Uri(usuarioApiBaseUrl!);
        });

        services.AddHttpClient<ICursosService, CursoService>(client =>
        {
            client.BaseAddress = new Uri(cursosApiBaseUrl!);
        });

        services.AddHostedService<EventBusBackgroundService>();

        return services;
    }
}