using Proyecto_2_HTML.Logica;
using Proyecto_2_HTML.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_2_HTML.Controladores
{
    public class ReparacionesController : Controller
    {
        // GET: Lista de Reparaciones
        public ActionResult Index()
        {
            return View(ReparacionLogica.Listar());
        }

        // GET: Formulario de Crear
        public ActionResult Crear()
        {
            // Cargar lista de equipos para el Dropdown
            ViewBag.ListaEquipos = new SelectList(EquiposLogica.Listar(), "EquipoID", "Modelo");
            return View();
        }

        // POST: Guardar Reparación
        [HttpPost]
        public ActionResult Crear(Reparacion oReparacion)
        {
            // Limpiamos errores falsos
            ModelState.Clear();

            // Valores por defecto
            if (string.IsNullOrEmpty(oReparacion.Estado)) oReparacion.Estado = "Pendiente";

            // INTENTO DE GUARDADO CON CAPTURA DE ERROR
            try
            {
                // Llamamos a la lógica
                bool respuesta = ReparacionLogica.Registrar(oReparacion);

                if (respuesta)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    // Si llega aquí, SQL devolvió 0 filas afectadas (raro)
                    ViewBag.Error = "No se guardó, pero no hubo error técnico. Verifique el procedimiento almacenado.";
                }
            }
            catch (Exception ex)
            {
                // ¡AQUÍ ESTÁ LA CLAVE! 
                // Esto mostrará el error real en la pantalla (ej: 'FK constraint', 'Procedure not found')
                ViewBag.Error = "ERROR TÉCNICO: " + ex.Message;
            }

            // Recargamos la lista de equipos para que no se rompa la vista
            ViewBag.ListaEquipos = new SelectList(EquiposLogica.Listar(), "EquipoID", "Modelo");
            return View(oReparacion);
        }
    }
}