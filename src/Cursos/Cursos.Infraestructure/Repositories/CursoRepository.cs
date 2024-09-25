using Cursos.Domain.Cursos;
using MongoDB.Driver;

namespace Cursos.Infrastructure.Repositories;

public class CursoRepository : Repository<Curso>, ICursoRepository
{
    public CursoRepository(IMongoDatabase database) : base(database, "Cursos")
    {
    }

    public async Task<List<Curso>> GetCursos(CancellationToken cancellationToken = default)
    {
        return await _collection.Find(_ => true).ToListAsync(cancellationToken);    
    }
}