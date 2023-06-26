using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PNT1_Grupo6.Context;
using PNT1_Grupo6.Models;

namespace PNT1_Grupo6.Controllers
{
    public class ProductosController : Controller
    {
        private readonly GestorDatabaseContext _context;

        public ProductosController(GestorDatabaseContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Productos.ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        [AuthorizeRole(Rol.Admin)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRole(Rol.Admin)]
        public async Task<IActionResult> Create([Bind("Id,CodigoProducto,Nombre,Stock,MaximoAlmacenable,Descripcion,PrecioVenta")] Producto producto)
        {
            if (ModelState.IsValid && ValidateData(producto))
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
            //return View(producto);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CodigoProducto,Nombre,Stock,MaximoAlmacenable,Descripcion,PrecioVenta")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (!ValidateStockAndPricing(producto))
                {
                    return View(producto);
                }
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
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
            return View(producto);
        }

        // GET: Productos/Delete/5
        [AuthorizeRole(Rol.Admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeRole(Rol.Admin)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }

        private bool ProductoExistsByCode(string code)
        {
            return _context.Productos.Any(e => e.CodigoProducto == code);
        }

        private bool ValidateStockAndPricing(Producto producto)
        {
            bool isValid = true;
            if (producto.PrecioVenta <= 0)
            {
                TempData["ProductoError"] = "Fije un precio positivo!.";
                isValid = false;
            }
            else if (producto.Stock > producto.MaximoAlmacenable || producto.Stock < 0 || producto.MaximoAlmacenable <= 0)
            {
                TempData["ProductoError"] = "El stock no puede ser nulo o mayor al máximo almacenable.";
                isValid = false;
            }
            return isValid;
        }
        private bool ValidateData(Producto producto)
        {
            bool isValid = true;
            if (ProductoExistsByCode(producto.CodigoProducto))
            {
                TempData["ProductoError"] = "El producto: " + producto.CodigoProducto + " ya se encuentra registrado.";
                isValid = false;
            }
            else if (!ValidateStockAndPricing(producto)) 
            {
                isValid = false;
            }
            
            return isValid;
        }
    }
}
