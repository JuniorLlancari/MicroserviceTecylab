using Microsoft.EntityFrameworkCore;
using Usuarios.Domain.Roles;

namespace Usuarios.Infraestructure.Repositories;

internal sealed class RolRepository : Repository<Rol>, IRolRepository
{
    public RolRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Rol?> GetByNameAsync(string rol, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<Rol>().FirstOrDefaultAsync(
            rol => rol.Equals(rol), cancellationToken
        );
    }
}