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
            if (HttpContext.Session.GetString("Rol") != null && HttpContext.Session.GetString("UserName") != null)
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
            if (HttpContext.Session.GetString("Rol") != null && HttpContext.Session.GetString("UserName") != null)
            {
                return RedirectToAction("Dashboard");
            }
            // Validar el usuario y contraseña en la base de datos
            var user = _context.Usuarios.FirstOrDefault(u => u.UserName == username && u.Password == password);

            if (user != null)
            {
                // Guardar el rol del usuario en la variable de sesión
                HttpContext.Session.SetString("Rol", user.RolUsuario.ToString());
                HttpContext.Session.SetString("UserName", user.Nombre.ToString());

                HttpContext.Session.SetObject("Usuario", user);

                ViewData["Usuario"] = user;
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
            var usuario = HttpContext.Session.GetObject<Usuario>("Usuario");

            if (usuario != null)
            {
                ViewData["Usuario"] = usuario;
                return View(usuario);
            }
            else
            {
                // El usuario no existe o no ha iniciado sesión correctamente
                TempData["LoginError"] = "No se ha iniciado sesión correctamente.";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Logout()
        {
            // Eliminar las variables de sesión
            HttpContext.Session.Remove("Rol");
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("Usuario");

            return RedirectToAction("Index", "Home"); // Redirigir a la página de inicio u otra página de tu elección
        }

        // GET: Sessions/CreateNewUser
        public IActionResult CreateNewUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            if (ModelState.IsValid && (!DniExists(usuario.Dni) && !UserNameExists(usuario.UserName)))
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();

                // Guardar el rol del usuario en la variable de sesión
                HttpContext.Session.SetString("Rol", usuario.RolUsuario.ToString());
                HttpContext.Session.SetString("UserName", usuario.UserName.ToString());
                // Guardar el objeto usuario en la sesión
                HttpContext.Session.SetObject("Usuario", usuario);

                // Redirigir al dashboard
                return RedirectToAction("Dashboard");
            }
            else
            {
                TempData["CreateNewUserError"] = "No se pudo crear el usuario debido a duplicados.";
                return RedirectToAction("CreateNewUser");
            }

        }

        private bool DniExists(int dni)
        {
            bool exists = _context.Usuarios.Any(e => e.Dni == dni);
            if (exists)
            {
                TempData["CreateNewUserError"] = "Dni repetido.";
            }
            return exists;
        }
        private bool UserNameExists(string user)
        {
            bool exists = _context.Usuarios.Any(e => e.UserName == user);
            if (exists)
            {
                TempData["CreateNewUserError"] = "Usuario repetido.";
            }
            return exists;
        }
    }
}
