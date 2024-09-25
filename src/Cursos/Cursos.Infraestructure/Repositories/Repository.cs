using MongoDB.Driver;

namespace Cursos.Infrastructure.Repositories;

public abstract class Repository<T> where T : class
{
   protected readonly IMongoCollection<T> _collection;

    protected Repository(IMongoDatabase database, string collection)
    {
        _collection = database.GetCollection<T>(collection);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(entity,cancellationToken : cancellationToken);
    }

    public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
       return await _collection.Find(Builders<T>.Filter.Eq("Id",id)).FirstOrDefaultAsync(cancellationToken);
    }
}