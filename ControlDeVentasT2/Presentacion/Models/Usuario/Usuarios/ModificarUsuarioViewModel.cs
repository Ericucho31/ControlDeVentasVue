﻿using System.ComponentModel.DataAnnotations;

namespace Presentacion.Models.Usuario.Usuarios
{
    public class ModificarUsuarioViewModel
    {
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string TipoDocumento { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty; //Este dato es string porque se recibe desde ele frontend
        public bool ActualizarPassword { get; set; } = false;
    }
}
