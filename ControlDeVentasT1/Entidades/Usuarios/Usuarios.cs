using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Usuarios
{
    public class Usuarios
    {
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        public string NombreUsuario { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public bool Estado { get; set; }


    }
}
