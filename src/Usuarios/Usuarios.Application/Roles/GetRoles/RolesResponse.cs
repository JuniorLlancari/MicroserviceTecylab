namespace Usuarios.Application.Roles.GetRoles;

public record RolesResponse
(
    Guid Id,
    string Nombre,
    string Descripcion
);