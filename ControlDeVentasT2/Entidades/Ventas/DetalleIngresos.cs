using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Ventas
{
    public class DetalleIngresos
    {
        public int IdDetalleIngresos { get; set; }
        public int IdIngreso { get; set; }
        public int IdArticulo { get; set; }
        public int Cantidad { get; set; }
        public float Precio { get; set; }
    }
}
