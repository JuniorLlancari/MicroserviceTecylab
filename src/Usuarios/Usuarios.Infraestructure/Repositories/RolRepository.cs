using Microsoft.EntityFrameworkCore;
using Usuarios.Domain.Roles;

namespace Usuarios.Infraestructure.Repositories;

internal sealed class RolRepository : Repository<Rol>, IRolRepository
{
    public RolRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Rol?> GetByNameAsync(string rolName, CancellationToken cancellationToken = default)
    {
        //  return await dbContext.Set<Rol>().FirstOrDefaultAsync(
        //     rol => rol.NombreRol.Equals(rolName) , cancellationToken
        //  );

        return await dbContext.Set<Rol>().FirstOrDefaultAsync(
        rol => rol.NombreRol == rolName, cancellationToken
        );

    }
}