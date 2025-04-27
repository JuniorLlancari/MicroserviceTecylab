using MediatR;
using Usuarios.Application.Services;
using Usuarios.Application.Usuarios.CrearUsuario;
using Usuarios.Domain.Usuarios;

namespace Usuarios.Application.Events.Usuario;

public class UserDocenteCreateEventHandler : INotificationHandler<UserDocenteCreateEvent>
{
    private readonly IEventBus _eventBus;
    private readonly ISender _sender;
    private readonly IUsuarioRepository _usuarioRepository;


    public UserDocenteCreateEventHandler(
        IEventBus eventBus,
        ISender sender,
        IUsuarioRepository usuarioRepository)
    {
        _eventBus = eventBus;
        _sender = sender;
        _usuarioRepository = usuarioRepository;
    }

    public async Task Handle(UserDocenteCreateEvent notification, CancellationToken cancellationToken)
    {
        var correoResult = CorreoElectronico.Create(notification.CorreoElectronico);
        var existencia = await _usuarioRepository.ExisteCorreoAsync(correoResult.Value, cancellationToken);
        if (!existencia)
        {
            var command = new CrearUsuarioCommand
            (
                "12345678",
                notification.Rol,
                notification.Nombres,
                notification.ApellidoPaterno,
                notification.ApellidoMaterno,
                 notification.FechaNacimiento,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
               string.Empty,
                notification.CorreoElectronico
            );

            var resultado = await _sender.Send(command, cancellationToken);

            if (resultado.IsSuccess)
            {
                _eventBus.Publish(new UserDocenteCreatedEvent(resultado.Value, notification.EspecialidadId));
            }
        }

    }
}