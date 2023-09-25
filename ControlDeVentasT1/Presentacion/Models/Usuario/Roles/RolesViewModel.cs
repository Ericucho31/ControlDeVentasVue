using System.ComponentModel.DataAnnotations;

namespace Presentacion.Models.Usuario.Roles
{
    public class RolesViewModel
    {
        [Key]
        public int IdRol { get; set; }
        public string NombreRol { get; set; } = string.Empty;
        public string DescripcionRol { get; set; } = string.Empty;
        public bool Estado { get; set; } = false;
    }
}
