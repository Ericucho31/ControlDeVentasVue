using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Alamcen
{
    public class Articulos
    {
        [Key]
        public int IdArticulo { get; set; }

        [Required]
        [ForeignKey("IdCategoria")]
        public int IdCategoria { get; set; }

        public  Categoria IdCategoriaNavigation{ get; set; }


        [StringLength(50, MinimumLength = 3, ErrorMessage = "El código no debe de tener menos de 3 caracteres, ni más de 50")]
        public string CodigoArticulo { get; set; } = string.Empty;
        [Required]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener menos de 3 caracteres, ni más de 150")]
        public string NombreArticulo { get; set; } = string.Empty;

        [Required]
        public float PrecioVenta { get; set; }
        [Required]
        public int Stock { get; set; }
        [StringLength(250, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener menos de 3 caracteres, ni más de 150")]
        public string DescripcionArticulo { get; set; } = string.Empty;
        [Required]
        public bool Estado { get; set; }


    }
}
