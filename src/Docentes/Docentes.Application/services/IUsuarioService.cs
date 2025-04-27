namespace Docentes.Application.Services;

public interface IUsuarioService
{
    Task<bool> UsuarioExistsAsync(Guid usuarioId, CancellationToken cancellationToken);
}