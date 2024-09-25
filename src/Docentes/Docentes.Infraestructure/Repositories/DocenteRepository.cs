using Docentes.Domain.Docentes;

namespace Docentes.Infrastructure.Repositories;

internal sealed class DocenteRepository : Repository<Docente>, IDocenteRepository
{
    public DocenteRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}