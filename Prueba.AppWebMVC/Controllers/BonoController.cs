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
    public class BonoController : Controller
    {
        private readonly PlanillaScriptContext _context;

        public BonoController(PlanillaScriptContext context)
        {
            _context = context;
        }

        // GET: Bono
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bonos.ToListAsync());
        }

        // GET: Bono/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bono = await _context.Bonos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bono == null)
            {
                return NotFound();
            }

            return View(bono);
        }

        // GET: Bono/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bono/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreBono,Valor,Estado,FechaValidacion,FechaExpiracion,Operacion,Planilla")] Bono bono)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bono);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bono);
        }

        // GET: Bono/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bono = await _context.Bonos.FindAsync(id);
            if (bono == null)
            {
                return NotFound();
            }
            return View(bono);
        }

        // POST: Bono/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreBono,Valor,Estado,FechaValidacion,FechaExpiracion,Operacion,Planilla")] Bono bono)
        {
            if (id != bono.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bono);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BonoExists(bono.Id))
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
            return View(bono);
        }

        // GET: Bono/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bono = await _context.Bonos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bono == null)
            {
                return NotFound();
            }

            return View(bono);
        }

        // POST: Bono/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bono = await _context.Bonos.FindAsync(id);
            if (bono != null)
            {
                _context.Bonos.Remove(bono);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BonoExists(int id)
        {
            return _context.Bonos.Any(e => e.Id == id);
        }
    }
}
