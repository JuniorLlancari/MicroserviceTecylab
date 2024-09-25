using Usuarios.Domain.Abstractions;

namespace Usuarios.Domain.Usuarios;

public class UsuarioErrores
{
    public static Error YaSeEncuentraActivo = new(
        "Usuario.YaSeEncuentraActivo",
        "El usuario ya se encuentra activo por tanto no se puede volver a activar"
    );

    public static Error YaSeEncuentraInactivo = new(
        "Usuario.YaSeEncuentraInactivo",
        "El usuario ya se encuentra inactivo por tanto no se puede volver a inactivar"
    );
}