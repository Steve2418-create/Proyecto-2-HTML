using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_2_HTML.Modelos
{
    public class Equipo
    {
        public int EquipoID { get; set; }

        [Required]
        [Display(Name = "Tipo de Equipo")]
        public string TipoEquipo { get; set; } // Ejemplo: Laptop, Impresora

        public string Modelo { get; set; }

        [Required]
        [Display(Name = "ID del Dueño")]
        public int UsuarioID { get; set; }
    }
}