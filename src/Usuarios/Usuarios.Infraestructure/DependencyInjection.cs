using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Usuarios.Application.Abstractions.Data;
using Usuarios.Application.Abstractions.Email;
using Usuarios.Application.Abstractions.Time;
using Usuarios.Domain.Abstractions;
using Usuarios.Domain.Roles;
using Usuarios.Domain.Usuarios;
using Usuarios.Infraestructure.Data;
using Usuarios.Infraestructure.Email;
using Usuarios.Infraestructure.Repositories;
using Usuarios.Infraestructure.Time;

namespace Usuarios.Infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(
        this IServiceCollection services,
        IConfiguration configuration
    ){
        services.AddTransient<IDateTimeProvider,DateTimeProvider>();
        services.AddTransient<IEmailService,EmailService>();
 
        var connectionString = configuration.GetConnectionString("Database") 
        ?? throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(
            options => {  options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IUsuarioRepository,UsuarioRepository>();
        services.AddScoped<IRolRepository,RolRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));

        return services;
        

    }
}