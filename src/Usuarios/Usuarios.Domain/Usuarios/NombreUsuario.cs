namespace Usuarios.Domain.Usuarios;

public record NombreUsuario{

    public string Value { get; init; }
    
    public static implicit operator string(NombreUsuario nombre) => nombre.Value;

    public NombreUsuario(string _value)
    {
        Value = _value;
    }

    public static NombreUsuario Create(string _value)
    {
        if (string.IsNullOrWhiteSpace(_value))
        {
            throw new InvalidOperationException("El nombre no puede ser vacio");
        }
        return new NombreUsuario(_value);
    }


}