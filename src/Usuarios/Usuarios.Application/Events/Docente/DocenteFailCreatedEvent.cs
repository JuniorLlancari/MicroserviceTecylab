using MediatR;

namespace Usuarios.Application.Events.Docente;

public sealed record DocenteFailCreatedEvent
(
    Guid IdUsuario
) : INotification;