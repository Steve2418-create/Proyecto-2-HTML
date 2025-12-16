using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto_2_HTML.Modelos
{
    public class Tecnico
    {
        public int TecnicoID { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Especialidad { get; set; }
    }
}