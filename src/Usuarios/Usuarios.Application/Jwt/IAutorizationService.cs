namespace Usuarios.Application.Jwt;

public interface IAutorizationService
{
    Task<AutorizacionResponse> DevolverToken(AutorizacionRequest autorizacion, CancellationToken cancellationToken);
}