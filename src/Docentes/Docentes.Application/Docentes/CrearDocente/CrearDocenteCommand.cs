using Docentes.Application.Abstractions.Messaging;

namespace Docentes.Application.Docentes.CrearDocente;

public record CrearDocenteCommand
(
     Guid EspecialidadId
    , string Nombres
    , string ApellidoPaterno
    , string ApellidoMaterno
    , DateTime FechaNacimiento
    , string CorreoElectronico
) : ICommand<string>;