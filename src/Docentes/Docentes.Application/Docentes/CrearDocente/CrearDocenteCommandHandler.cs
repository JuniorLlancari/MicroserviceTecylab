using Docentes.Application.Abstractions.Messaging;
using Docentes.Application.Events.Usuarios;
using Docentes.Application.Services;
using Docentes.Domain.Abstractions;
using Docentes.Domain.Docentes;

namespace Docentes.Application.Docentes.CrearDocente;

internal sealed class CrearDocenteCommandHandler :
ICommandHandler<CrearDocenteCommand, string>
{
    private readonly ICursosService _cursosService;
    private readonly ICacheService _cacheService;
    private readonly IEventBus _eventBus;

    public CrearDocenteCommandHandler(ICursosService cursosService, ICacheService cacheService, IEventBus eventBus)
    {
        _cursosService = cursosService;
        _cacheService = cacheService;
        _eventBus = eventBus;
    }

    public async Task<Result<string>> Handle(CrearDocenteCommand request, CancellationToken cancellationToken)
    {

        var cacheKey = $"curso_{request.EspecialidadId}";
        var cursoExist = await _cacheService.GetCacheValueAsync<bool>(cacheKey);

        if (!cursoExist)
        {
            cursoExist = await _cursosService.CursoExistsAsync(request.EspecialidadId, cancellationToken);
            var expirationTime = TimeSpan.FromMinutes(3);
            await _cacheService.SetCacheValueAsync(cacheKey, cursoExist, expirationTime);
        }

        if (!cursoExist)
        {
            return Result.Failure<string>(new Error("CursoNotFound", "El especialidadId no es valido."));
        }

        try
        {
            var usuarioCreateEvent = new UserDocenteCreateEvent(
                request.EspecialidadId,
                "Docente",
                request.Nombres,
                request.ApellidoPaterno,
                request.ApellidoMaterno,
                 request.FechaNacimiento,
                request.CorreoElectronico
        );
            _eventBus.Publish(usuarioCreateEvent);

            return Result.Success("Se registro el proceso para crear el docente");
        }
        catch (Exception)
        {
            return Result.Failure<string>(new Error("ProcesoRegistrarDocente.Fail", "El proceso para crear el docente fallo"));
        }


    }
}