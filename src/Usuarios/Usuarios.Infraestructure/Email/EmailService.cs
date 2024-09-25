using Usuarios.Application.Abstractions.Email;

namespace Usuarios.Infraestructure.Email;

public class EmailService : IEmailService
{
    public Task SendAsync(string correo, string subject, string body)
    {
       return Task.CompletedTask;
    }
}
