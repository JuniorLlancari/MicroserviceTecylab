using Docentes.Application.Events.Docentes;
using Docentes.Application.Services;
using Docentes.Domain.Abstractions;
using Docentes.Domain.Docentes;
using MediatR;

namespace Docentes.Application.Events.Usuarios;

public class UserDocenteCreatedEventHandler : INotificationHandler<UserDocenteCreatedEvent>
{
  private readonly IEventBus _eventBus;
  private readonly IDocenteRepository _docenteRepository;
  private readonly IUnitOfWork _unitOfWork;

  public UserDocenteCreatedEventHandler(IEventBus eventBus, IDocenteRepository docenteRepository, IUnitOfWork unitOfWork)
  {
    _eventBus = eventBus;
    _docenteRepository = docenteRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task Handle(UserDocenteCreatedEvent notification, CancellationToken cancellationToken)
  {
    var existingDocente = await _docenteRepository.GetByIdUsuarioAsync(notification.UsuarioId);
    if (existingDocente == null)
    {
      try
      {

        if (notification.EspecialidadId == new Guid("b17c7893-a989-49c1-998b-3517e015ed29"))
        {
          throw new InvalidOperationException();
        }

        var docente = Docente.Create(
          notification.UsuarioId,
          notification.EspecialidadId
        );

        _docenteRepository.Add(docente.Value);
        await _unitOfWork.SaveChangesAsync();
      }
      catch (Exception)
      {
        _eventBus.Publish(new DocenteFailCreatedEvent(notification.UsuarioId));

      }
    }

  }
}