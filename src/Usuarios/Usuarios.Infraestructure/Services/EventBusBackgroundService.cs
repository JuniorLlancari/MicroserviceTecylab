using Microsoft.Extensions.Hosting;
using Usuarios.Application.Events.Docente;
using Usuarios.Application.Events.Usuario;
using Usuarios.Application.Services;

namespace Usuarios.Infrastructure.services;

public class EventBusBackgroundService : BackgroundService
{
    private readonly IEventBus _eventBus;

    public EventBusBackgroundService(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        _eventBus.Consume<UserDocenteCreateEvent>();
        _eventBus.Consume<DocenteFailCreatedEvent>();

        return Task.CompletedTask;
    }
}