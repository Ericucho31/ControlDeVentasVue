﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Entidades.Alamcen
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        [Required]
        [StringLength(100, MinimumLength =3, ErrorMessage = "El nombre no debe de tener menos de 3 caracteres, ni más de 100")]
        public string NombreCategoria { get; set; } = string.Empty;
        [StringLength(250)]
        public string Descripcion { get; set; } = string.Empty;
        public bool Estado { get; set; }

        [ForeignKey("IdCategoria")]
        public virtual ICollection<Articulos> Articulos { get; set; } = new List<Articulos>();
    }
}
