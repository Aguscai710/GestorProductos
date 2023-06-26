using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PNT1_Grupo6.Models;
using PNT1_Grupo6.Context;

namespace PNT1_Grupo6.Controllers
{
    public class SessionsController : Controller
    {
        private readonly GestorDatabaseContext _context;

        public SessionsController(GestorDatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Si las variables de sesión ya están asignadas, redirige al dashboard
            if (HttpContext.Session.GetString("Rol") != null && HttpContext.Session.GetString("Nombre") != null)
            {
                return RedirectToAction("Dashboard");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Si ya hay variables de usuario porque ya nos logeamos, no debería ir al login
            if (HttpContext.Session.GetString("Rol") != null && HttpContext.Session.GetString("Nombre") != null)
            {
                return RedirectToAction("Dashboard");
            }
            // Validar el usuario y contraseña en la base de datos
            var user = _context.Usuarios.FirstOrDefault(u => u.Nombre == username && u.Password == password);

            if (user != null)
            {
                // Guardar el rol del usuario en la variable de sesión
                HttpContext.Session.SetString("Rol", user.RolUsuario.ToString());
                HttpContext.Session.SetString("Nombre", user.Nombre.ToString());

                HttpContext.Session.SetObject("Usuario", user);
                return RedirectToAction("Dashboard");
            }
            else
            {
                TempData["LoginError"] = "Credenciales inválidas";
                Usuario invalidUser = new Usuario { Nombre = username };
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Dashboard()
        {
            var nombreUsuario = HttpContext.Session.GetString("Nombre");
            var rolUsuario = HttpContext.Session.GetString("Rol");
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Nombre == nombreUsuario);

            if (usuario != null)
            {
                return View(usuario);
            }
            else
            {
                // El usuario no existe o no ha iniciado sesión correctamente
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Logout()
        {
            // Eliminar las variables de sesión
            HttpContext.Session.Remove("Rol");
            HttpContext.Session.Remove("Nombre");
            HttpContext.Session.Remove("Usuario");

            return RedirectToAction("Index", "Home"); // Redirigir a la página de inicio u otra página de tu elección
        }
    }
}
