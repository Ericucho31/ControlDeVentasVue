using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Ventas
{
    public class DetalleVentas
    {
        public int IdDetalleVentas { get; set; }
        public int IdVentas { get; set; }
        public int IdArticulo { get; set; }
        public float Cantidad { get; set; }
        public float Descuento { get; set; }
    }
}
