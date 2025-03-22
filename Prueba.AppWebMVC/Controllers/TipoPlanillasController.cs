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
    public class TipoPlanillasController : Controller
    {
        private readonly PlanillaScriptContext _context;

        public TipoPlanillasController(PlanillaScriptContext context)
        {
            _context = context;
        }

        // GET: TipoPlanillas
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoPlanillas.ToListAsync());
        }

        // GET: TipoPlanillas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoPlanilla = await _context.TipoPlanillas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoPlanilla == null)
            {
                return NotFound();
            }

            return View(tipoPlanilla);
        }

        // GET: TipoPlanillas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoPlanillas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreTipo")] TipoPlanilla tipoPlanilla)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoPlanilla);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoPlanilla);
        }

        // GET: TipoPlanillas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoPlanilla = await _context.TipoPlanillas.FindAsync(id);
            if (tipoPlanilla == null)
            {
                return NotFound();
            }
            return View(tipoPlanilla);
        }

        // POST: TipoPlanillas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreTipo")] TipoPlanilla tipoPlanilla)
        {
            if (id != tipoPlanilla.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoPlanilla);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoPlanillaExists(tipoPlanilla.Id))
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
            return View(tipoPlanilla);
        }

        // GET: TipoPlanillas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoPlanilla = await _context.TipoPlanillas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoPlanilla == null)
            {
                return NotFound();
            }

            return View(tipoPlanilla);
        }

        // POST: TipoPlanillas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoPlanilla = await _context.TipoPlanillas.FindAsync(id);
            if (tipoPlanilla != null)
            {
                _context.TipoPlanillas.Remove(tipoPlanilla);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoPlanillaExists(int id)
        {
            return _context.TipoPlanillas.Any(e => e.Id == id);
        }
    }
}
