namespace Docentes.Application.services;

public interface ICursosService
{
    Task<bool> CursoExistsAsync(Guid cursoId, CancellationToken cancellationToken);
}