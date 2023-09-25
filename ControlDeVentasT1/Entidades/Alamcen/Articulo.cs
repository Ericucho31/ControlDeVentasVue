using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Alamcen
{
    public class Articulo
    {
        public int IdArticulo { get; set; }
        public int IdCategoria { get; set; }
        public string CodigoArticulo { get; set; }
        public string NombreArticulo { get; set; }
        public float PrecioVenta { get; set; }
        public int Stock { get; set; }
        public string DescripcionArticulo { get; set; }
        public bool Estado { get; set; }


    }
}
