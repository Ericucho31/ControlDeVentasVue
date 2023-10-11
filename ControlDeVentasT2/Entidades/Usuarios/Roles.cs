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
    public class Roles
    {
        public int IdRol { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener menos de 3 caracteres, ni más de 30")]
        public string NombreRol { get; set; } = string.Empty; //30
        [Required]
        [StringLength(100)]
        public string DescripcionRol { get; set; } = string.Empty;//100
        public bool Estado { get; set; }
        [ForeignKey("IdRol")]
        public virtual ICollection<E_Usuarios> Usuarios { get; set; } = new List<E_Usuarios>();
    }
}
