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
    public class JefeInmediatoController : Controller
    {
        private readonly PlanillaScriptContext _context;

        public JefeInmediatoController(PlanillaScriptContext context)
        {
            _context = context;
        }

        // GET: JefeInmediato
        public async Task<IActionResult> Index()
        {
            var planillaScriptContext = _context.JefeInmediatos.Include(j => j.Empleados);
            return View(await planillaScriptContext.ToListAsync());
        }

        // GET: JefeInmediato/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jefeInmediato = await _context.JefeInmediatos
                .Include(j => j.Empleados)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jefeInmediato == null)
            {
                return NotFound();
            }

            return View(jefeInmediato);
        }

        // GET: JefeInmediato/Create
        public IActionResult Create()
        {
            ViewData["EmpleadosId"] = new SelectList(_context.Empleados, "Id", "Id");
            return View();
        }

        // POST: JefeInmediato/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmpleadosId")] JefeInmediato jefeInmediato)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jefeInmediato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpleadosId"] = new SelectList(_context.Empleados, "Id", "Id", jefeInmediato.EmpleadosId);
            return View(jefeInmediato);
        }

        // GET: JefeInmediato/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jefeInmediato = await _context.JefeInmediatos.FindAsync(id);
            if (jefeInmediato == null)
            {
                return NotFound();
            }
            ViewData["EmpleadosId"] = new SelectList(_context.Empleados, "Id", "Id", jefeInmediato.EmpleadosId);
            return View(jefeInmediato);
        }

        // POST: JefeInmediato/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmpleadosId")] JefeInmediato jefeInmediato)
        {
            if (id != jefeInmediato.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jefeInmediato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JefeInmediatoExists(jefeInmediato.Id))
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
            ViewData["EmpleadosId"] = new SelectList(_context.Empleados, "Id", "Id", jefeInmediato.EmpleadosId);
            return View(jefeInmediato);
        }

        // GET: JefeInmediato/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jefeInmediato = await _context.JefeInmediatos
                .Include(j => j.Empleados)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jefeInmediato == null)
            {
                return NotFound();
            }

            return View(jefeInmediato);
        }

        // POST: JefeInmediato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jefeInmediato = await _context.JefeInmediatos.FindAsync(id);
            if (jefeInmediato != null)
            {
                _context.JefeInmediatos.Remove(jefeInmediato);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JefeInmediatoExists(int id)
        {
            return _context.JefeInmediatos.Any(e => e.Id == id);
        }
    }
}
