using MediatR;
using Usuarios.Application.Abstractions.Email;
using Usuarios.Domain.Usuarios;
using Usuarios.Domain.Usuarios.Events;

namespace Usuarios.Application.Usuarios.CrearUsuario;

public class CrearUsuarioCommandDomainEventHandler : INotificationHandler<UserCreateDomainEvent>
{
    private readonly IUsuarioRepository _usuarioRepository;

    private readonly IEmailService _emailService;

    public CrearUsuarioCommandDomainEventHandler(IUsuarioRepository usuarioRepository, IEmailService emailService)
    {
        _usuarioRepository = usuarioRepository;
        _emailService = emailService;
    }

    public async Task Handle(UserCreateDomainEvent notification, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(notification.IdUsuario,cancellationToken);

        if(usuario is null){
            return;
        }
        
        await _emailService.SendAsync(
            usuario.CorreoElectronico!.Value,
            "Bienvenido al sistema",
            $"Su usuario es: {usuario.NombreUsuario} y fue creado {usuario.FechaUltimoCambio}."
        );
    }
}
