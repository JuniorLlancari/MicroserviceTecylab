using FluentValidation;

namespace Usuarios.Application.Usuarios.CrearUsuario;

public class CrearUsuarioCommandValidator : AbstractValidator<CrearUsuarioCommand>
{
    public CrearUsuarioCommandValidator()
    {
        RuleFor(u => u.CorreoElectronico).NotEmpty().WithMessage("El correo debe ser valido.");
        RuleFor(u => u.Nombres).NotEmpty();
        RuleFor(u => u.ApellidoPaterno).NotEmpty();
        RuleFor(u => u.FechaNacimiento).LessThan(DateTime.UtcNow).WithMessage("La fecha de nacimiento no puede ser futura");
    }
}