using Entidades.Alamcen;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Usuarios;

namespace Datos.Mapping.Usuarios
{
    public class RolesMap : IEntityTypeConfiguration<Roles>
    {
        public void Configure(EntityTypeBuilder<Roles> builder)
        {
            builder.ToTable("Roles").HasKey(r => r.IdRol);
            builder.Property(r => r.NombreRol).HasMaxLength(30);
            builder.Property(r => r.DescripcionRol).HasMaxLength(100);
            builder.Property(r => r.Estado).HasDefaultValue(false);

        }
    
    }
}
