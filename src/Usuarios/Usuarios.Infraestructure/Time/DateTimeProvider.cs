using Usuarios.Application.Abstractions.Time;

namespace Usuarios.Infraestructure.Time;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime CurrentTime => DateTime.UtcNow;
}
