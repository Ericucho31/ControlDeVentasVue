using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Ventas
{
    public class Ingresos
    {
        public int IdIngreso { get; set; }
        public int IdPersona { get; set; }
        public int IdUsuario { get; set; }
        public string TipoComprobante { get; set; }
        public string SerieComprobante { get; set; }
        public string NumeroComprobante { get; set; }
        public DateTime FechaHoraComprobante { get; set; }
        public float Impuestos { get; set; }
        public float Total { get; set; }
        public bool Estado { get; set; }
    }
}
