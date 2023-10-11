using Entidades.Alamcen;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Usuarios
{
    public class E_Usuarios
    {
        [Key]
        public int IdUsuario { get; set; }
        [Required]
        [ForeignKey("IdRol")]
        public int IdRol { get; set; }
        public Roles IdRolNavigation { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "El nombre del usuario no debe de tener menos de 3 caracteres, ni más de 150")]
        public string NombreUsuario { get; set; } = string.Empty;
        public string TipoDocumento { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        public bool Estado { get; set; }

        


    }
}
