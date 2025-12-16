using Proyecto_2_HTML.Logica;
using Proyecto_2_HTML.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_2_HTML.Controladores
{
    public class AccesoController : Controller
    {
        // GET: Muestra la pantalla de Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Recibe los datos del formulario
        [HttpPost]
        public ActionResult Login(string correo, string clave)
        {
            // Validamos usando Correo Y Clave
            bool existe = UsuarioLogica.Validar(correo, clave);

            if (existe)
            {
                Session["Usuario"] = correo;
                Session["Rol"] = UsuarioLogica.ObtenerRol(correo);
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                ViewBag.Error = "Usuario o contraseña incorrectos.";
                return View();
            }
        }

        // GET: Mostrar formulario de registro
        public ActionResult Registrar()
        {
            return View();
        }

        // POST: Guardar el nuevo usuario
        [HttpPost]
        public ActionResult Registrar(Usuario oUsuario)
        {
            // 1. Validar que las contraseñas coincidan (si agregas confirmación) o campos vacíos
            if (string.IsNullOrEmpty(oUsuario.Nombre) || string.IsNullOrEmpty(oUsuario.CorreoElectronico) || string.IsNullOrEmpty(oUsuario.Clave))
            {
                ViewBag.Error = "Todos los campos son obligatorios";
                return View();
            }

            // 2. Guardar en base de datos
            oUsuario.Rol = "usuario"; // Forzamos que sea usuario normal
            bool respuesta = UsuarioLogica.Registrar(oUsuario);

            if (respuesta)
            {
                // SI SE GUARDA: Vamos al Login para que entre
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.Error = "No se pudo registrar. Es posible que el correo ya exista.";
                return View();
            }
        }
    }
}