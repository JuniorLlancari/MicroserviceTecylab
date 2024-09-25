namespace Usuarios.Domain.Roles;

public record NombreRol{

    public string Value { get; init; }

    public NombreRol(string _value) => Value = _value;

    public static implicit operator string(NombreRol d) => d.Value;

};

