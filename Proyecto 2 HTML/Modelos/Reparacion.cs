using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_2_HTML.Modelos
{
    public class Reparacion
    {
        public int ReparacionID { get; set; }
        public int EquipoID { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Estado { get; set; }

        public string ModeloEquipo { get; set; }
    }
}