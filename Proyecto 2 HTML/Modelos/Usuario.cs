using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_2_HTML.Modelos
{
    public class Usuario
    {
        public int UsuarioID { get; set; }

        [Required]
        [Display(Name = "Nombre Completo")]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Correo Electrónico")]
        public string CorreoElectronico { get; set; }

        public string Telefono { get; set; }

        public string Clave { get; set; } 

        public string Rol { get; set; } 
    }
}