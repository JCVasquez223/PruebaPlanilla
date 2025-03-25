using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prueba.AppWebMVC.Models;

namespace Prueba.AppWebMVC.Controllers
{
    public class PlanillaController : Controller
    {
        private readonly PlanillaScriptContext _context;

        public PlanillaController(PlanillaScriptContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var planillas = await _context.Planillas.Include(p => p.TipoPlanilla).ToListAsync();
            return View(planillas);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var planilla = await _context.Planillas
                .Include(p => p.TipoPlanilla)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (planilla == null) return NotFound();

            return View(planilla);
        }

        public IActionResult Create()
        {
            ViewData["TipoPlanillaId"] = new SelectList(_context.TipoPlanillas, "Id", "NombreTipo");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Planilla planilla)
        {
            if (planilla.FechaFin <= planilla.FechaInicio)
            {
                ModelState.AddModelError("FechaFin", "La fecha de fin debe ser posterior a la fecha de inicio.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    planilla.Autorizacion = 0;
                    _context.Add(planilla);
                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "Planilla creada con éxito.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al guardar la planilla: " + ex.Message);
                }
            }

            ViewData["TipoPlanillaId"] = new SelectList(_context.TipoPlanillas, "Id", "NombreTipo", planilla.TipoPlanillaId);
            return View(planilla);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var planilla = await _context.Planillas.FindAsync(id);
            if (planilla == null) return NotFound();

            ViewData["TipoPlanillaId"] = new SelectList(_context.TipoPlanillas, "Id", "NombreTipo", planilla.TipoPlanillaId);
            return View(planilla);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Planilla planilla)
        {
            if (id != planilla.Id) return NotFound();

            if (planilla.FechaFin <= planilla.FechaInicio)
            {
                ModelState.AddModelError("FechaFin", "La fecha de fin debe ser posterior a la fecha de inicio.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planilla);
                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "Planilla actualizada con éxito.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanillaExists(planilla.Id)) return NotFound();
                    throw;
                }
            }

            ViewData["TipoPlanillaId"] = new SelectList(_context.TipoPlanillas, "Id", "NombreTipo", planilla.TipoPlanillaId);
            return View(planilla);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var planilla = await _context.Planillas
                .Include(p => p.TipoPlanilla)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (planilla == null) return NotFound();

            return View(planilla);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var planilla = await _context.Planillas.FindAsync(id);
            if (planilla == null) return NotFound();

            _context.Planillas.Remove(planilla);
            await _context.SaveChangesAsync();
            TempData["Mensaje"] = "Planilla eliminada correctamente.";
            return RedirectToAction(nameof(Index));
        }

        private bool PlanillaExists(int id)
        {
            return _context.Planillas.Any(e => e.Id == id);
        }
    }
}
