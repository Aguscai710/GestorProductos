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
            
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Validar el usuario y contraseña en la base de datos
            var user = _context.Usuarios.FirstOrDefault(u => u.Nombre == username && u.Password == password);

            if (user != null)
            {
                // Guardar el rol del usuario en la variable de sesión
                HttpContext.Session.SetString("Rol", user.RolUsuario.ToString());
                HttpContext.Session.SetString("Nombre", user.Nombre.ToString());
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
    }
}
