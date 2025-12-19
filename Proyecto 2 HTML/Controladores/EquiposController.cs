using Proyecto_2_HTML.Logica;
using Proyecto_2_HTML.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_2_HTML.Controladores
{
    public class EquiposController : Controller
    {
        // GET: Equipos
        public ActionResult Index()
        {
            return View(EquiposLogica.Listar());
        }

        // GET: Equipos/Crear
        public ActionResult Crear()
        {
            // CARGAR LA LISTA DE DUEÑOS (USUARIOS) PARA EL DROPDOWN
            // Value = UsuarioID (lo que se guarda), Text = Nombre (lo que se ve)
            ViewBag.ListaUsuarios = new SelectList(UsuarioLogica.Listar(), "UsuarioID", "Nombre");

            return View();
        }

        // POST: Equipos/Crear
        [HttpPost]
        public ActionResult Crear(Equipo oEquipo)
        {
            // Validar si se seleccionó un dueño
            if (oEquipo.UsuarioID == 0)
            {
                ModelState.AddModelError("UsuarioID", "Debe seleccionar un dueño");
            }

            // Ignoramos validaciones automáticas tontas
            ModelState.Remove("UsuarioID");

            bool respuesta = EquiposLogica.Registrar(oEquipo);

            if (respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                // Si falla, recargamos la lista de dueños
                ViewBag.ListaUsuarios = new SelectList(UsuarioLogica.Listar(), "UsuarioID", "Nombre");
                ViewBag.Error = "No se pudo registrar el equipo";
                return View(oEquipo);
            }
        }

        // POST: Equipos/Eliminar/5
        public ActionResult Eliminar(int id)
        {
            bool respuesta = EquiposLogica.Eliminar(id);
            return RedirectToAction("Index");
        }
    }
}