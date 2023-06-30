using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PNT1_Grupo6.Context;
using PNT1_Grupo6.Models;

namespace PNT1_Grupo6.Controllers
{
    public class OrdenesCompraController : Controller
    {
        private readonly GestorDatabaseContext _context;

        public OrdenesCompraController(GestorDatabaseContext context)
        {
            _context = context;
        }

        // GET: OrdenesCompra
        
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrdenesCompra.ToListAsync());
        }

        // GET: OrdenesCompra/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordenCompra = await _context.OrdenesCompra
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ordenCompra == null)
            {
                return NotFound();
            }

            return View(ordenCompra);
        }

        // GET: OrdenesCompra/Create
        public IActionResult Create()
        {
            ViewBag.Productos = GetListaProductos();
            ViewBag.Proveedores = GetListaProveedores();
            return View();
        }

        // POST: OrdenesCompra/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumeroOrden,CodigoProveedor,NombreProveedor,CodigoProducto,NombreProducto,PrecioUnitario,Cantidad,Estado")] OrdenCompra ordenCompra)
        {
            if (ModelState.IsValid && ValidateData(ordenCompra))
            {
                _context.Add(ordenCompra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: OrdenesCompra/Edit/5
        [AuthorizeRole(Rol.Admin)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordenCompra = await _context.OrdenesCompra.FindAsync(id);
            if (ordenCompra == null)
            {
                return NotFound();
            }
            return View(ordenCompra);
        }

        // POST: OrdenesCompra/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRole(Rol.Admin)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumeroOrden,CodigoProveedor,NombreProveedor,CodigoProducto,NombreProducto,PrecioUnitario,Cantidad,Estado")] OrdenCompra ordenCompra)
        {
            if (id != ordenCompra.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (!ValidatePrice(ordenCompra.PrecioUnitario))
                {
                    TempData["OrdenCompraError"] = "Elija un precio real.";
                    return View(ordenCompra);
                }
                else if (!ValidateQuantity(ordenCompra.Cantidad))
                {
                    TempData["OrdenCompraError"] = "La cantidad del producto no es válida.";
                    return View(ordenCompra);
                }

                try
                {
                    _context.Update(ordenCompra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenCompraExists(ordenCompra.Id))
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
            return View(ordenCompra);
        }

        // GET: OrdenesCompra/Delete/5
        [AuthorizeRole(Rol.Admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordenCompra = await _context.OrdenesCompra
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ordenCompra == null)
            {
                return NotFound();
            }

            return View(ordenCompra);
        }

        // POST: OrdenesCompra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeRole(Rol.Admin)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ordenCompra = await _context.OrdenesCompra.FindAsync(id);
            _context.OrdenesCompra.Remove(ordenCompra);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdenCompraExists(int id)
        {
            return _context.OrdenesCompra.Any(e => e.Id == id);
        }

        private bool OrdenCompraExistsByOrder(string order)
        {
            return _context.OrdenesCompra.Any(e => e.NumeroOrden == order);
        }

        private bool ValidatePrice(double price)
        {
            return (price > 0);
        }
        private bool ValidateQuantity(int quantity)
        {
            return (quantity > 0);
        }

        private bool ValidateData(OrdenCompra order)
        {
            bool isValid = true;
            if (OrdenCompraExistsByOrder(order.NumeroOrden))
            {
                TempData["OrdenCompraError"] = "La orden con número: " + order.NumeroOrden + " ya se encuentra registrada.";
                isValid = false;
            }
            else if (!ValidatePrice(order.PrecioUnitario))
            {
                TempData["OrdenCompraError"] = "Elija un precio real.";
                isValid = false;
            }
            else if (!ValidateQuantity(order.Cantidad))
            {
                TempData["OrdenCompraError"] = "La cantidad del producto no es válida.";
                isValid = false;
            }
            return isValid;
        }
        private List<Producto> GetListaProductos()
        {
            return _context.Productos.ToList();
        }

        private List<Proveedor> GetListaProveedores()
        {
            return _context.Proveedores.ToList();
        }
    }
}
