using MediatR;

namespace Usuarios.Application.Events.Usuario;

public sealed record UserDocenteCreateEvent(
    Guid EspecialidadId
    ,string Rol
    ,string Nombres
    ,string ApellidoPaterno
    ,string ApellidoMaterno
    ,DateTime FechaNacimiento
    ,string CorreoElectronico
) : INotification; 