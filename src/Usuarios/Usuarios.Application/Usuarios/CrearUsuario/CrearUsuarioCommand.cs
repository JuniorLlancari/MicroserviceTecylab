using Usuarios.Application.Abstractions.Messaging;

namespace Usuarios.Application.Usuarios.CrearUsuario;

public record CrearUsuarioCommand
(
    string Password
   ,string Rol
   ,string Nombres
   ,string ApellidoPaterno
   ,string ApellidoMaterno
   ,DateTime FechaNacimiento
   ,string Pais
   ,string Departamento
   ,string Provincia
   ,string Ciudad
   ,string Distrito
   ,string Calle
   ,string CorreoElectronico
): ICommand<Guid>;