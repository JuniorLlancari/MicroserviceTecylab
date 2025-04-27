using Microsoft.EntityFrameworkCore;
using Usuarios.Domain.Usuarios;

namespace Usuarios.Infraestructure.Repositories;

internal sealed class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> ExisteCorreoAsync(CorreoElectronico correoElectronico, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<Usuario>()
        .AnyAsync(usuario => ((string)usuario.CorreoElectronico!) == correoElectronico.Value, cancellationToken);
    }
}
