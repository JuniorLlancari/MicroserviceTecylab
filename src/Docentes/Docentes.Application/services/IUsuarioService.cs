namespace Docentes.Application.services;

public interface IUsuarioService
{
    Task<bool> UsuarioExistsAsync(Guid usuarioId, CancellationToken cancellationToken);
}