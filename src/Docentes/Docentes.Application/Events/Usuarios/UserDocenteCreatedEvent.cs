using MediatR;

namespace Docentes.Application.Events.Usuarios;
public record UserDocenteCreatedEvent 
(
    Guid EspecialidadId,
    Guid UsuarioId
): INotification;