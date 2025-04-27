namespace Usuarios.Application.Services;

public interface IEventBus
{
     void Publish<T>(T @event) where T : class;
     void Consume<T>() where T : class;
}