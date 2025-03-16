using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prueba.AppWebMVC.Models;

namespace Prueba.AppWebMVC.Controllers
{
    public class AsignacionDescuentoController : Controller
    {
        private readonly PlanillaScriptContext _context;

        public AsignacionDescuentoController(PlanillaScriptContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var descuentos = _context.AsignacionDescuentos
                .Include(a => a.Descuentos)
                .Include(a => a.Empleados);
            return View(await descuentos.ToListAsync());
        }

        public IActionResult Create(int empleadoId)
        {
            var empleado = _context.Empleados.Find(empleadoId);
            if (empleado == null)
            {
                TempData["ErrorMessage"] = "El empleado no existe.";
                return RedirectToAction("Index");
            }

            ViewBag.EmpleadoId = empleado.Id;
            ViewBag.EmpleadoNombre = empleado.Nombre;
            ViewBag.EmpleadoDUI = empleado.Dui;
            ViewBag.EmpleadoPuesto = empleado.PuestoTrabajo;
            ViewBag.EmpleadoSalario = empleado.SalarioBase;
            ViewBag.Descuentos = _context.Descuentos.ToList();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int empleadoId, List<int> descuentosSeleccionados)
        {
            if (descuentosSeleccionados == null || !descuentosSeleccionados.Any())
            {
                TempData["ErrorMessage"] = "Debe seleccionar al menos un descuento.";
                return RedirectToAction("Create", new { empleadoId });
            }

            foreach (var descuentoId in descuentosSeleccionados)
            {
                var asignacion = new AsignacionDescuento
                {
                    EmpleadosId = empleadoId,
                    DescuentosId = descuentoId
                };
                _context.AsignacionDescuentos.Add(asignacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var asignacionDescuento = await _context.AsignacionDescuentos.FindAsync(id);
            if (asignacionDescuento == null) return NotFound();

            ViewData["DescuentosId"] = new SelectList(_context.Descuentos, "Id", "Nombre", asignacionDescuento.DescuentosId);
            ViewData["EmpleadosId"] = new SelectList(_context.Empleados, "Id", "NombreCompleto", asignacionDescuento.EmpleadosId);
            return View(asignacionDescuento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmpleadosId,DescuentosId")] AsignacionDescuento asignacionDescuento)
        {
            if (id != asignacionDescuento.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asignacionDescuento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsignacionDescuentoExists(asignacionDescuento.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DescuentosId"] = new SelectList(_context.Descuentos, "Id", "Nombre", asignacionDescuento.DescuentosId);
            ViewData["EmpleadosId"] = new SelectList(_context.Empleados, "Id", "NombreCompleto", asignacionDescuento.EmpleadosId);
            return View(asignacionDescuento);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var asignacionDescuento = await _context.AsignacionDescuentos
                .Include(a => a.Descuentos)
                .Include(a => a.Empleados)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (asignacionDescuento == null) return NotFound();

            return View(asignacionDescuento);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asignacionDescuento = await _context.AsignacionDescuentos.FindAsync(id);
            if (asignacionDescuento != null) _context.AsignacionDescuentos.Remove(asignacionDescuento);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsignacionDescuentoExists(int id)
        {
            return _context.AsignacionDescuentos.Any(e => e.Id == id);
        }
    }
}
