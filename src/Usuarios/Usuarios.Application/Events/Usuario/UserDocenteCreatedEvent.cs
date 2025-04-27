namespace Usuarios.Application.Events.Usuario;

public sealed record UserDocenteCreatedEvent
(
    Guid UsuarioId,
    Guid EspecialidadId
);