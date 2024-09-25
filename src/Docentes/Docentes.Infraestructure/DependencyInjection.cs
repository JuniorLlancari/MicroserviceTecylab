using Docentes.Application.services;
using Docentes.Domain.Abstractions;
using Docentes.Domain.CursosImpartidos;
using Docentes.Domain.Docentes;
using Docentes.Infrastructure.Repositories;
using Docentes.Infrastructure.services;
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

        var usuarioApiBaseUrl = configuration["UsuarioApiBaseUrl"];    
        var cursosApiBaseUrl = configuration["CursosApiBaseUrl"]; 

        services.AddDbContext<ApplicationDbContext>(
            options =>
            {
                options.UseNpgsql(connectionStringPostgres).UseSnakeCaseNamingConvention(); // usuario, producto_detalle
            }
        );

        services.AddScoped<IDocenteRepository, DocenteRepository>();
        services.AddScoped<ICursoImpartidoRepository, CursoImpartidoRepository>();
        // services.AddScoped<ICacheService, CacheService>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddHttpClient<IUsuarioService,UsuarioService>( client =>
        {
            client.BaseAddress = new Uri(usuarioApiBaseUrl!);
        });

        services.AddHttpClient<ICursosService,CursoService>( client =>
        {
            client.BaseAddress = new Uri(cursosApiBaseUrl!);
        });

        return services;
    }
}