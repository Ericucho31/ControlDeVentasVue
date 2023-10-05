using System.ComponentModel.DataAnnotations;

namespace Presentacion.Models.Almacen.Categoria
{
    public class CategoriaViewModel
    {
        [Key]
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;
        public bool Estado { get; set; } = false;
    }
}
