
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Usuarios.Domain.Usuarios;

namespace Usuarios.Infraestructure.Configurations;

public class UsuarioConfigurations : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuarios");
        builder.HasKey(x => x.Id);

        builder.OwnsOne(usuario => usuario.Direccion);

        
        builder.Property(usuario => usuario.NombreUsuario)
            .HasMaxLength(30)
            .HasConversion( nombre => nombre!.Value, value => new NombreUsuario(value) );

        builder.Property(usuario => usuario.NombresPersona)
            .HasMaxLength(50)
            .HasConversion( nombre => nombre!.Value, value => new NombresPersona(value) );

        builder.Property(usuario => usuario.ApellidoPaterno)
            .HasMaxLength(50)
            .HasConversion( nombre => nombre!.Value, value => new ApellidoPaterno(value) );

        builder.Property(usuario => usuario.ApellidoMaterno)
            .HasMaxLength(50)
            .HasConversion( nombre => nombre!.Value, value => new ApellidoMaterno(value) );

        builder.Property(usuario => usuario.CorreoElectronico)
            .HasMaxLength(150)
            .HasConversion( nombre => nombre!.Value, value => CorreoElectronico.Create(value).Value );

        builder.Property(usuario => usuario.Password)
            .HasMaxLength(150)
            .HasConversion( nombre => nombre!.Value, value => Password.Create(value) );

        builder.Property(usuario => usuario.FechaNacimiento);

        builder.Property(usuario => usuario.Estado)
            .HasConversion(
                estado => estado.ToString(),
                estado => (UsuarioEstado) Enum.Parse(typeof(UsuarioEstado), estado)
            );

         builder.Property(usuario => usuario.FechaUltimoCambio);

        builder.HasOne(usuario => usuario.Rol)
        .WithMany()
        .HasForeignKey(usuario => usuario.RolId)
        .IsRequired();

        builder.Property<uint>("Version").IsRowVersion();
    }
}