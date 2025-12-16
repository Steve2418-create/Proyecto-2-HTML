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
        // GET: Equipos (Listar)
        public ActionResult Index()
        {
            return View(EquiposLogica.Listar());
        }

        // GET: Crear
        public ActionResult Crear()
        {
            // Cargar lista de usuarios para el ComboBox (Dropdown)
            // "UsuarioID" es el valor que se guarda, "Nombre" es lo que se ve
            ViewBag.ListaUsuarios = new SelectList(UsuarioLogica.Listar(), "UsuarioID", "Nombre");
            return View();
        }

        // POST: Crear
        [HttpPost]
        public ActionResult Crear(Equipo oEquipo)
        {
            if (ModelState.IsValid)
            {
                bool respuesta = EquiposLogica.Registrar(oEquipo);
                if (respuesta)
                    return RedirectToAction("Index");
            }
            // Si falla, recargamos la lista para que no de error la vista
            ViewBag.ListaUsuarios = new SelectList(UsuarioLogica.Listar(), "UsuarioID", "Nombre");
            return View(oEquipo);
        }

        // GET: Editar
        public ActionResult Editar(int id)
        {
            var equipo = EquiposLogica.Listar().Find(e => e.EquipoID == id);

            // También cargamos la lista aquí para poder cambiar de dueño si es necesario
            ViewBag.ListaUsuarios = new SelectList(UsuarioLogica.Listar(), "UsuarioID", "Nombre", equipo.UsuarioID);

            return View(equipo);
        }

        // POST: Editar
        [HttpPost]
        public ActionResult Editar(Equipo oEquipo)
        {
            if (ModelState.IsValid)
            {
                bool respuesta = EquiposLogica.Modificar(oEquipo);
                if (respuesta)
                    return RedirectToAction("Index");
            }
            ViewBag.ListaUsuarios = new SelectList(UsuarioLogica.Listar(), "UsuarioID", "Nombre", oEquipo.UsuarioID);
            return View(oEquipo);
        }

        // GET: Eliminar
        public ActionResult Eliminar(int id)
        {
            var equipo = EquiposLogica.Listar().Find(e => e.EquipoID == id);
            return View(equipo);
        }

        [HttpPost]
        public ActionResult Eliminar(string id)
        {
            bool respuesta = EquiposLogica.Eliminar(int.Parse(id));
            if (respuesta)
                return RedirectToAction("Index");
            return View();
        }
    }
}