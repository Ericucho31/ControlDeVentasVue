using System.ComponentModel.DataAnnotations;

namespace Presentacion.Models.Usuario.Usuarios
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
