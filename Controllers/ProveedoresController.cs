using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PNT1_Grupo6.Context;
using PNT1_Grupo6.Models;

namespace PNT1_Grupo6.Controllers
{
    public class ProveedoresController : Controller
    {
        private readonly GestorDatabaseContext _context;

        public ProveedoresController(GestorDatabaseContext context)
        {
            _context = context;
        }

        // GET: Proveedores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Proveedores.ToListAsync());
        }

        // GET: Proveedores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedor = await _context.Proveedores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proveedor == null)
            {
                return NotFound();
            }

            return View(proveedor);
        }

        // GET: Proveedores/Create
        [AuthorizeRole(Rol.Admin)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proveedores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRole(Rol.Admin)]
        public async Task<IActionResult> Create([Bind("Id,CodigoProveedor,Nombre,Email,Telefono,Rubro")] Proveedor proveedor)
        {
            if (ModelState.IsValid && ValidateData(proveedor.CodigoProveedor, proveedor.Email))
            {
                _context.Add(proveedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else 
            {
                return RedirectToAction(nameof(Index));
            }
            //return View(proveedor);
        }

        // GET: Proveedores/Edit/5
        [AuthorizeRole(Rol.Admin)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }
            return View(proveedor);
        }

        // POST: Proveedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRole(Rol.Admin)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CodigoProveedor,Nombre,Email,Telefono,Rubro")] Proveedor proveedor)
        {
            if (id != proveedor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (!ValidEmail(proveedor.Email))
                {
                    TempData["ProveedorError"] = "El email provisto no cumple con el formato esperado.";
                    return View(proveedor);
                }
                try
                {
                    _context.Update(proveedor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProveedorExists(proveedor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(proveedor);
        }

        // GET: Proveedores/Delete/5
        [AuthorizeRole(Rol.Admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedor = await _context.Proveedores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proveedor == null)
            {
                return NotFound();
            }

            return View(proveedor);
        }

        // POST: Proveedores/Delete/5
        [AuthorizeRole(Rol.Admin)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            _context.Proveedores.Remove(proveedor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProveedorExists(int id)
        {
            return _context.Proveedores.Any(e => e.Id == id);
        }

        private bool ValidateData(string code, string email)
        {
            bool state = false;
            if (ProveedorExists(code))
            {
                TempData["ProveedorError"] = "El proveedor con código: " + code + " ya se encuentra registrado.";
            }
            else if (!ValidEmail(email))
            {
                TempData["ProveedorError"] = "El email provisto no cumple con el formato esperado.";
            }
            else 
            {
                state = true;
            }
            return state;
        }

        private bool ProveedorExists(string code)
        {
            return _context.Proveedores.Any(e => e.CodigoProveedor == code);
        }

        private bool ValidEmail(string email) 
        {
            // Expresión regular para validar direcciones de correo electrónico
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            // Comprobar si el correo electrónico coincide con el patrón
            Match match = Regex.Match(email, pattern);

            return match.Success;
        }
    }
}
