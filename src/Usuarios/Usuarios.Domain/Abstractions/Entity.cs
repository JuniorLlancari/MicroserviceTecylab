namespace Usuarios.Domain.Abstractions;

public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents=new();
    public Guid Id { get; init; }

    protected Entity()
    {
    }

    protected Entity(Guid id)
    {
        Id=id;
    }

    public void ClearDomainEvents(){
        _domainEvents.Clear();
    }

    protected void RaiseDomainEvent(IDomainEvent domainEvent){
        _domainEvents.Add(domainEvent);
    }

    public IReadOnlyList<IDomainEvent> GetDomainEvents(){
        return _domainEvents.ToList();
    }

}