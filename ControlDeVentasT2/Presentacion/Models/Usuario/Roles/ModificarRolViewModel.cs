using System.ComponentModel.DataAnnotations;

namespace Presentacion.Models.Usuario.Roles
{
    public class ModificarRolViewModel
    {
        public int IdRol { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener menos de 3 caracteres, ni más de 30")]
        public string NombreRol { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string DescripcionRol { get; set; } = string.Empty;
        public bool Estado { get; set; }
    }
}
