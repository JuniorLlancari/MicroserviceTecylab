using FluentValidation;

namespace Docentes.Application.Docentes.CrearDocente;

public class CrearDocenteCommandValidator : AbstractValidator<CrearDocenteCommand>
{
    public CrearDocenteCommandValidator()
    {
        RuleFor(d => d.usuarioId)
            .Must(guid => Guid.TryParse(guid.ToString() , out _))
            .WithMessage("El ID de usuario debe ser un Guid válido");
        RuleFor(d => d.especialidadId)
            .Must(guid => Guid.TryParse(guid.ToString() , out _))
            .WithMessage("El ID de usuario debe ser un Guid válido");;
    }
}