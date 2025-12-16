using Proyecto_2_HTML.Logica;
using Proyecto_2_HTML.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_2_HTML.Controladores
{
    public class TecnicosController : Controller
    {
        public ActionResult Index()
        {
            return View(TecnicosLogica.Listar());
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Tecnico oTecnico)
        {
            if (ModelState.IsValid)
            {
                bool respuesta = TecnicosLogica.Registrar(oTecnico);
                if (respuesta)
                    return RedirectToAction("Index");
            }
            return View(oTecnico);
        }

        public ActionResult Editar(int id)
        {
            var tecnico = TecnicosLogica.Listar().Find(t => t.TecnicoID == id);
            return View(tecnico);
        }

        [HttpPost]
        public ActionResult Editar(Tecnico oTecnico)
        {
            if (ModelState.IsValid)
            {
                bool respuesta = TecnicosLogica.Modificar(oTecnico);
                if (respuesta)
                    return RedirectToAction("Index");
            }
            return View(oTecnico);
        }

        public ActionResult Eliminar(int id)
        {
            var tecnico = TecnicosLogica.Listar().Find(t => t.TecnicoID == id);
            return View(tecnico);
        }

        [HttpPost]
        public ActionResult Eliminar(string id)
        {
            bool respuesta = TecnicosLogica.Eliminar(int.Parse(id));
            if (respuesta)
                return RedirectToAction("Index");
            return View();
        }
    }
}