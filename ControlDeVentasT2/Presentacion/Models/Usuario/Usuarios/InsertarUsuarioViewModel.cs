using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Presentacion.Models.Usuario.Usuarios
{
    public class InsertarUsuarioViewModel
    {
        [Required]
        public int IdRol { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "El nombre del usuario no debe de tener menos de 3 caracteres, ni más de 150")]
        public string NombreUsuario { get; set; } = string.Empty;
        public string TipoDocumento { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty; //Este dato es string porque se recibe desde ele frontend
        public bool Estado { get; set; }
    }
}
