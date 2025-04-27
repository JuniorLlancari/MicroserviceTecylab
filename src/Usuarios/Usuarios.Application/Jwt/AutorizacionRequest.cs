namespace Usuarios.Application.Jwt;

public record AutorizacionRequest
(
    string NombreUsuario,
    string Clave
);
