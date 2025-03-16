using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prueba.AppWebMVC.Models;

namespace Prueba.AppWebMVC.Controllers
{
    public class AsignacionBonoController : Controller
    {
        private readonly PlanillaScriptContext _context;

        public AsignacionBonoController(PlanillaScriptContext context)
        {
            _context = context;
        }

        // GET: AsignacionBono
        public async Task<IActionResult> Index()
        {
            var planillaScriptContext = _context.AsignacionBonos.Include(a => a.Bonos).Include(a => a.Empleados);
            return View(await planillaScriptContext.ToListAsync());
        }

        // GET: AsignacionBono/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignacionBono = await _context.AsignacionBonos
                .Include(a => a.Bonos)
                .Include(a => a.Empleados)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asignacionBono == null)
            {
                return NotFound();
            }

            return View(asignacionBono);
        }

        // GET: AsignacionBono/Create
        public IActionResult Create(int? empleadoId)
        {
            if (empleadoId == null)
            {
                return NotFound();
            }

            var empleado = _context.Empleados
                .Include(e => e.PuestoTrabajo).FirstOrDefault(e => e.Id == empleadoId);

            if (empleado == null)
            {
                return NotFound();
            }

            var bonos = _context.Bonos.ToList();

            ViewBag.EmpleadoId = empleadoId;
            ViewBag.EmpleadoNombre = $"{empleado.Nombre} {empleado.Apellido}";
            ViewBag.EmpleadoDUI = empleado.Dui;
            ViewBag.EmpleadoPuesto = empleado.PuestoTrabajo?.NombrePuesto;
            ViewBag.EmpleadoSalario = empleado.SalarioBase;
            ViewBag.Bonos = bonos;


            return View();
        }

        // POST: AsignacionBono/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int empleadoId, List<int> bonosSeleccionados)
        {
            if (empleadoId == 0 || bonosSeleccionados == null || !bonosSeleccionados.Any())
            {
                ModelState.AddModelError("", "Debe seleccionar al menos un bono.");
                ViewBag.Bonos = _context.Bonos.ToList();
                return View();
            }



            // Asignar los bonos seleccionados al empleado
            foreach (var bonoId in bonosSeleccionados)
            {
                var asignacionBono = new AsignacionBono
                {
                    EmpleadosId = empleadoId,
                    BonosId = bonoId,
                    Estado = 1 // Estado activo
                };
                _context.AsignacionBonos.Add(asignacionBono);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Empleado");
        }
        // GET: AsignacionBono/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignacionBono = await _context.AsignacionBonos
                .Include(a => a.Bonos)
                .Include(a => a.Empleados)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (asignacionBono == null)
            {
                return NotFound();
            }

            // Obtener todos los bonos disponibles
            var bonosDisponibles = _context.Bonos.ToList();

            // Obtener bonos ya asignados al empleado
            var bonosAsignados = _context.AsignacionBonos
                .Where(a => a.EmpleadosId == asignacionBono.EmpleadosId)
                .Select(a => a.BonosId)
                .ToList();

            ViewBag.BonosDisponibles = bonosDisponibles;
            ViewBag.BonosAsignados = bonosAsignados;
            ViewBag.EmpleadoNombre = $"{asignacionBono.Empleados.Nombre} {asignacionBono.Empleados.Apellido}";

            return View(asignacionBono);
        }

        // POST: AsignacionBono/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, List<int> bonosSeleccionados)
        {
            var asignacionBono = await _context.AsignacionBonos
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            if (asignacionBono == null)
            {
                return NotFound();
            }

            // Eliminar los bonos previamente asignados
            var bonosPrevios = _context.AsignacionBonos
                .Where(a => a.EmpleadosId == asignacionBono.EmpleadosId)
                .ToList();

            _context.AsignacionBonos.RemoveRange(bonosPrevios);

            // Agregar los bonos nuevos seleccionados
            foreach (var bonoId in bonosSeleccionados)
            {
                _context.AsignacionBonos.Add(new AsignacionBono
                {
                    EmpleadosId = asignacionBono.EmpleadosId,
                    BonosId = bonoId,
                    Estado = asignacionBono.Estado
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Empleado");
        }

        // GET: AsignacionBono/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignacionBono = await _context.AsignacionBonos
                .Include(a => a.Bonos)
                .Include(a => a.Empleados)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asignacionBono == null)
            {
                return NotFound();
            }

            return View(asignacionBono);
        }

        // POST: AsignacionBono/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asignacionBono = await _context.AsignacionBonos.FindAsync(id);
            if (asignacionBono != null)
            {
                _context.AsignacionBonos.Remove(asignacionBono);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsignacionBonoExists(int id)
        {
            return _context.AsignacionBonos.Any(e => e.Id == id);
        }
    }
}
