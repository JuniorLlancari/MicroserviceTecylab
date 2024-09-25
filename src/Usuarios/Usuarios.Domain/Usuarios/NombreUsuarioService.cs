namespace Usuarios.Domain.Usuarios;

public class NombreUsuarioService
{
    public NombreUsuario GenerarNombreUsuario(
        ApellidoPaterno apellidoPaterno,
        NombresPersona nombrePersona
    ){
        var inicialNombre = nombrePersona.Value.Trim().Substring(0,1);
        var restoNombre = apellidoPaterno.Value.Trim().Replace(" ","");
        var nombreUsuario = inicialNombre + restoNombre;
        return NombreUsuario.Create(nombreUsuario);
    }
}