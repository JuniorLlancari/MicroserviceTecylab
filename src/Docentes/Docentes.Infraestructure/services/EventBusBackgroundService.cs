using Docentes.Application.Events.Usuarios;
using Docentes.Application.Services;
using Microsoft.Extensions.Hosting;

namespace Docentes.Infrastructure.Services;

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
        _eventBus.Consume<UserDocenteCreatedEvent>();
        return Task.CompletedTask;
    }
}