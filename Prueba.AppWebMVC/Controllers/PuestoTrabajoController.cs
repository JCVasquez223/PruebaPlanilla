﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prueba.AppWebMVC.Models;

namespace Prueba.AppWebMVC.Controllers
{
    public class PuestoTrabajoController : Controller
    {
        private readonly PlanillaScriptContext _context;

        public PuestoTrabajoController(PlanillaScriptContext context)
        {
            _context = context;
        }

        // GET: PuestoTrabajo
        // Controlador actualizado para PuestoTrabajo
        public async Task<IActionResult> Index(string nombrePuesto, byte estado = 2, int top = 10)
        {
            var estados = new Dictionary<byte, string> 
            {
                  { 1, "Activo" },
                  { 0, "Inactivo" }
             };

            ViewBag.Estados = estados;

            var query = _context.PuestoTrabajos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nombrePuesto))
                query = query.Where(p => p.NombrePuesto.Contains(nombrePuesto));

            if (estado != 2) // 2 indica "Todos los estados"
                query = query.Where(p => p.Estado == estado);

            query = query.Take(top);

            return View(await query.ToListAsync());
        }


        // GET: PuestoTrabajo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puestoTrabajo = await _context.PuestoTrabajos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (puestoTrabajo == null)
            {
                return NotFound();
            }

            return View(puestoTrabajo);
        }

        // GET: PuestoTrabajo/Create
        public IActionResult Create()
        {
            var estados = new List<SelectListItem>
            {
                new  SelectListItem{ Value="1",Text="Activo" },
                new  SelectListItem{ Value="0",Text="Inactivo" }
            };

            ViewBag.Estados = estados;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombrePuesto,SalarioBase,ValorxHora,ValorExtra,Estado")] PuestoTrabajo puestoTrabajo)
        {
            if (ModelState.IsValid)
            {
                puestoTrabajo.FechaCreacion = DateTime.Now;
                _context.Add(puestoTrabajo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(puestoTrabajo);
        }

        // GET: PuestoTrabajo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var estados = new List<SelectListItem>
            {
                new  SelectListItem{ Value="1",Text="Activo" },
                new  SelectListItem{ Value="0",Text="Inactivo" }
            };

            ViewBag.Estados = estados;
            if (id == null)
            {
                return NotFound();
            }

            var puestoTrabajo = await _context.PuestoTrabajos.FindAsync(id);
            if (puestoTrabajo == null)
            {
                return NotFound();
            }
            return View(puestoTrabajo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombrePuesto,SalarioBase,ValorxHora,ValorExtra,Estado")] PuestoTrabajo puestoTrabajo)
        {
            if (id != puestoTrabajo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(puestoTrabajo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PuestoTrabajoExists(puestoTrabajo.Id))
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
            return View(puestoTrabajo);
        }

        // GET: PuestoTrabajo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puestoTrabajo = await _context.PuestoTrabajos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (puestoTrabajo == null)
            {
                return NotFound();
            }

            return View(puestoTrabajo);
        }

        // POST: PuestoTrabajo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var puestoTrabajo = await _context.PuestoTrabajos.FindAsync(id);
            if (puestoTrabajo != null)
            {
                _context.PuestoTrabajos.Remove(puestoTrabajo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PuestoTrabajoExists(int id)
        {
            return _context.PuestoTrabajos.Any(e => e.Id == id);
        }
    }
}
