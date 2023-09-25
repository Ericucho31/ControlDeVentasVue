using Datos.Mapping.Almacen;
using Datos.Mapping.Usuarios;
using Entidades;
using Entidades.Alamcen;
using Entidades.Usuarios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DBContextSistema : DbContext
    {
        public DbSet<Entidades.Alamcen.Categoria> Categorias { get; set; } = null!;
        public DbSet<Entidades.Usuarios.Roles> Roles { get; set; } = null!;
        public DBContextSistema() { }
        public DBContextSistema(DbContextOptions options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("Conexion");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoriaMap());
            modelBuilder.ApplyConfiguration(new RolesMap());
        }
    }
}
