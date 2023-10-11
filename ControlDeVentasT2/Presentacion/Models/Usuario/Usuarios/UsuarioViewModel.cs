using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Entidades.Usuarios;

namespace Presentacion.Models.Usuario.Usuarios
{
    public class UsuarioViewModel
    {
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }

        [ForeignKey("IdRol")]
        public string Rol { get; set; } = string.Empty;

        public string NombreUsuario { get; set; } = string.Empty;
        public string TipoDocumento { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public bool Estado { get; set; }
    }
}
