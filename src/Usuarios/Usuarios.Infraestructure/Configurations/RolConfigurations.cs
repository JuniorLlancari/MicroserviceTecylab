using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Usuarios.Domain.Roles;

namespace Usuarios.Infraestructure.Configurations;

public class RolConfigurations : IEntityTypeConfiguration<Rol>
{
    public void Configure(EntityTypeBuilder<Rol> builder)
    {
        builder.ToTable("roles");
        builder.HasKey(key => key.Id);

        builder.Property(rol => rol.NombreRol)
            .HasMaxLength(20)
            .HasConversion( nombre => nombre!.Value, value => new NombreRol(value) );

        builder.HasIndex(rol => rol.NombreRol).IsUnique();

         builder.Property(rol => rol.Descripcion)
            .HasMaxLength(50)
            .HasConversion( descripcion => descripcion!.Value, value => new Descripcion(value) );

        builder.Property<uint>("Version").IsRowVersion();
        
    }
}