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
    public class PlanillasController : Controller
    {
        private readonly PlanillaScriptContext _context;

        public PlanillasController(PlanillaScriptContext context)
        {
            _context = context;
        }

        // GET: Planillas
        public async Task<IActionResult> Index()
        {
            var planillaScriptContext = _context.Planillas.Include(p => p.TipoPlanilla);
            return View(await planillaScriptContext.ToListAsync());
        }

        // GET: Planillas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planilla = await _context.Planillas
                .Include(p => p.TipoPlanilla)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (planilla == null)
            {
                return NotFound();
            }

            return View(planilla);
        }

        // GET: Planillas/Create
        public IActionResult Create()
        {
            ViewData["NombreTipo"] = new SelectList(_context.TipoPlanillas, "Id", "NombreTipo");
            return View();
        }

        // POST: Planillas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombrePlanilla,TipoPlanillaId,FechaInicio,FechaFin,Autorizacion,TotalPago")] Planilla planilla)
        {
            if (ModelState.IsValid)
            {
                _context.Add(planilla);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoPlanillaId"] = new SelectList(_context.TipoPlanillas, "Id", "NombreTipo", planilla.TipoPlanillaId);
            return View(planilla);
        }

        // GET: Planillas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planilla = await _context.Planillas.FindAsync(id);
            if (planilla == null)
            {
                return NotFound();
            }
            ViewData["NombreTipo"] = new SelectList(_context.TipoPlanillas, "Id", "NombreTipo", planilla.TipoPlanillaId);
            return View(planilla);
        }

        // POST: Planillas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombrePlanilla,TipoPlanillaId,FechaInicio,FechaFin,Autorizacion,TotalPago,FechaCreacion,FechaModificacion")] Planilla planilla)
        {
            if (id != planilla.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planilla);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanillaExists(planilla.Id))
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
            ViewData["TipoPlanillaId"] = new SelectList(_context.TipoPlanillas, "Id", "NombreTipo", planilla.TipoPlanillaId);
            return View(planilla);
        }

        // GET: Planillas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planilla = await _context.Planillas
                .Include(p => p.TipoPlanilla)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (planilla == null)
            {
                return NotFound();
            }

            return View(planilla);
        }

        // POST: Planillas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var planilla = await _context.Planillas.FindAsync(id);
            if (planilla != null)
            {
                _context.Planillas.Remove(planilla);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanillaExists(int id)
        {
            return _context.Planillas.Any(e => e.Id == id);
        }
    }
}
