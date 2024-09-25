using Usuarios.Application.Abstractions.Messaging;
using Usuarios.Application.Abstractions.Time;
using Usuarios.Domain.Abstractions;
using Usuarios.Domain.Roles;
using Usuarios.Domain.Usuarios;

namespace Usuarios.Application.Usuarios.CrearUsuario;

internal sealed class CrearUsuarioCommandHandler :
ICommandHandler<CrearUsuarioCommand, Guid>
{
    private readonly IRolRepository _rolRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly NombreUsuarioService _nombreUsuarioService;

    public CrearUsuarioCommandHandler(IRolRepository rolRepository, IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider, NombreUsuarioService nombreUsuarioService)
    {
        _rolRepository = rolRepository;
        _usuarioRepository = usuarioRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
        _nombreUsuarioService = nombreUsuarioService;
    }

    public async Task<Result<Guid>> Handle(CrearUsuarioCommand request, CancellationToken cancellationToken)
    {
        var rol = await _rolRepository.GetByNameAsync(request.Rol,cancellationToken);
        if (rol == null)
        {
            return Result.Failure<Guid>(RolErrores.NoEncontrado);
        }

        var usuario = Usuario.Create(
            new ApellidoPaterno(request.ApellidoPaterno),
            new ApellidoMaterno(request.ApellidoMaterno),
            new NombresPersona(request.Nombres),
            rol.Id,
            Password.Create(request.Password),
            request.FechaNacimiento.ToUniversalTime(),
            CorreoElectronico.Create(request.CorreoElectronico).Value,
            new Direccion (
                request.Pais,
                request.Departamento,
                request.Provincia,
                request.Distrito,
                request.Calle
            ),
            _dateTimeProvider.CurrentTime.ToUniversalTime(),
            _nombreUsuarioService
        );

        _usuarioRepository.Add(usuario);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return usuario.Id;
    }
}