namespace Usuarios.Application.Jwt;

public record AutorizacionResponse
(
    string Token,
    bool Resultado,
    string Msg
);