using Entidades.Alamcen;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Mapping.Usuarios
{
    public class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categoria").HasKey(c => c.IdCategoria);
            builder.Property(c => c.NombreCategoria).HasMaxLength(50);
            builder.Property(c => c.Descripcion).HasMaxLength(250);
            builder.Property(c => c.Estado).HasDefaultValue(false);

        }
    }

}
