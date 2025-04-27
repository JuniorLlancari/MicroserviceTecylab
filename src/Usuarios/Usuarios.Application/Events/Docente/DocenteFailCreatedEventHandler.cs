using MediatR;
using Usuarios.Domain.Abstractions;
using Usuarios.Domain.Usuarios;

namespace Usuarios.Application.Events.Docente;

public class DocenteFailCreatedEventHandler : INotificationHandler<DocenteFailCreatedEvent>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DocenteFailCreatedEventHandler(
        IUsuarioRepository usuarioRepository,
        IUnitOfWork unitOfWork)
    {
        _usuarioRepository = usuarioRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(DocenteFailCreatedEvent notification, CancellationToken cancellationToken)
    {
    
        var usuario = await _usuarioRepository.GetByIdAsync(notification.IdUsuario);
        if (usuario != null)
        {
            _usuarioRepository.Delete(usuario);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        
    }
}