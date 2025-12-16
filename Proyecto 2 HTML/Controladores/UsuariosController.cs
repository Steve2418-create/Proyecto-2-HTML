using Proyecto_2_HTML.Logica;
using Proyecto_2_HTML.Modelos;
using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_2_HTML.Controladores
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios (Muestra la lista)
        public ActionResult Index()
        {
            return View(UsuarioLogica.Listar());
        }

        // GET: Crear (Muestra el formulario vacío)
        public ActionResult Crear()
        {
            return View();
        }

        // POST: Crear (Recibe los datos y guarda)
        [HttpPost]
        public ActionResult Crear(Usuario oUsuario)
        {
            if (ModelState.IsValid)
            {
                bool respuesta = UsuarioLogica.Registrar(oUsuario);
                if (respuesta)
                    return RedirectToAction("Index");
                else
                    ViewBag.Error = "No se pudo guardar.";
            }
            return View(oUsuario);
        }

        // GET: Editar (Busca el usuario y lo muestra)
        public ActionResult Editar(int id)
        {
            // Reutilizamos el listar para buscar uno solo (no es lo más óptimo pero funciona rápido para este ejercicio)
            var usuario = UsuarioLogica.Listar().Find(u => u.UsuarioID == id);
            return View(usuario);
        }

        // POST: Editar (Guarda los cambios)
        [HttpPost]
        public ActionResult Editar(Usuario oUsuario)
        {
            if (ModelState.IsValid)
            {
                bool respuesta = UsuarioLogica.Modificar(oUsuario);
                if (respuesta)
                    return RedirectToAction("Index");
            }
            return View(oUsuario);
        }

        // GET: Eliminar
        public ActionResult Eliminar(int id)
        {
            var usuario = UsuarioLogica.Listar().Find(u => u.UsuarioID == id);
            return View(usuario);
        }

        [HttpPost]
        public ActionResult Eliminar(string id) // Recibe confirmación
        {
            // Nota: id viene como string a veces en post, convertir a int
            bool respuesta = UsuarioLogica.Eliminar(int.Parse(id));
            if (respuesta)
                return RedirectToAction("Index");
            return View();
        }
    }
}