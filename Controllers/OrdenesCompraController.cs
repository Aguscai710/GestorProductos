using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PNT1_Grupo6.Context;
using PNT1_Grupo6.Models;
using PNT1_Grupo6.ViewModels;

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
        public async Task<IActionResult> Index(string orderBy, string nombreProveedor, string nombreProducto)
        {
            var ordenesCompra = await _context.OrdenesCompra.ToListAsync();

            var ordenesCompraViewModel = ordenesCompra.Select(o => new OrdenCompraViewModel
            {
                Id = o.Id,
                ProveedorId = o.ProveedorId,
                ProductoId = o.ProductoId,
                PrecioUnitario = o.PrecioUnitario,
                Cantidad = o.Cantidad,
                PrecioTotal = o.PrecioTotal,
                Estado = o.Estado,
                NombreProveedor = _context.Proveedores.FirstOrDefault(p => p.Id == o.ProveedorId)?.Nombre,
                NombreProducto = _context.Productos.FirstOrDefault(p => p.Id == o.ProductoId)?.Nombre
            }).ToList();

            // Almacenar la lista original en ViewData Para la parte del resumen de ordenes
            ViewData["OriginalList"] = ordenesCompraViewModel.ToList();

            if (!string.IsNullOrEmpty(nombreProveedor))
            {
                ordenesCompraViewModel = ordenesCompraViewModel
                    .Where(o => o.NombreProveedor.ToLower().Contains(nombreProveedor.ToLower()))
                    .ToList();
            }

            if (!string.IsNullOrEmpty(nombreProducto))
            {
                ordenesCompraViewModel = ordenesCompraViewModel
                    .Where(o => o.NombreProducto.ToLower().Contains(nombreProducto.ToLower()))
                    .ToList();
            }

            // Aplicar ordenamiento si se especifica
            if (!string.IsNullOrEmpty(orderBy))
            {
                switch (orderBy)
                {
                    case "NumeroOrden":
                        ordenesCompraViewModel = ordenesCompraViewModel.OrderBy(o => o.Id).ToList();
                        break;
                    case "MayorPrecioTotal":
                        ordenesCompraViewModel = ordenesCompraViewModel.OrderByDescending(o => o.PrecioUnitario * o.Cantidad).ToList();
                        break;
                    case "MenorPrecioTotal":
                        ordenesCompraViewModel = ordenesCompraViewModel.OrderBy(o => o.PrecioUnitario * o.Cantidad).ToList();
                        break;
                    case "Producto":
                        ordenesCompraViewModel = ordenesCompraViewModel.OrderBy(o => o.NombreProducto).ToList();
                        break;
                    case "Proveedor":
                        ordenesCompraViewModel = ordenesCompraViewModel.OrderBy(o => o.NombreProveedor).ToList();
                        break;
                }
            }

            return View(ordenesCompraViewModel);
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
        public async Task<IActionResult> Create([Bind("Id,ProveedorId,ProductoId,PrecioUnitario,Cantidad,Estado")] OrdenCompra ordenCompra)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProveedorId,ProductoId,PrecioUnitario,Cantidad,Estado")] OrdenCompra ordenCompra)
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
        private bool ValidatePrice(decimal price)
        {
            bool isValid = true;
            string priceString = price.ToString();

            if (price < 0)
            {
                isValid = false;
            }

            return isValid;
        }
        private bool ValidateQuantity(int quantity)
        {
            return (quantity > 0);
        }

        private bool ValidateData(OrdenCompra order)
        {
            bool isValid = true;
            if (OrdenCompraExists(order.Id))
            {
                TempData["OrdenCompraError"] = "La orden con número: " + order.Id + " ya se encuentra registrada.";
                isValid = false;
            }
            else if (!ValidatePrice(order.PrecioUnitario))
            {
                TempData["OrdenCompraError"] = "Elija un precio real y entero.";
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
