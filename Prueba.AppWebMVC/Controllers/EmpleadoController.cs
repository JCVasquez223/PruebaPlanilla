using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prueba.AppWebMVC.Models;

namespace Prueba.AppWebMVC.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly PlanillaScriptContext _context;
        //PRUEBA CLONACION xd
        public EmpleadoController(PlanillaScriptContext context)
        {
            _context = context;
        }

        // GET: Empleado
        public async Task<IActionResult> Index()
        {
            var planillaScriptContext = _context.Empleados.Include(e => e.JefeInmediato).Include(e => e.PuestoTrabajo).Include(e => e.TipoDeHorario);
            return View(await planillaScriptContext.ToListAsync());
        }

        // GET: Empleado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.JefeInmediato)
                .Include(e => e.PuestoTrabajo)
                .Include(e => e.TipoDeHorario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // GET: Empleado/Create
        public IActionResult Create()
        {
            ViewData["JefeInmediatoId"] = new SelectList(_context.Empleados, "Id", "Id");
            ViewData["PuestoTrabajoId"] = new SelectList(_context.PuestoTrabajos, "Id", "NombrePuesto");
            ViewData["TipoDeHorarioId"] = new SelectList(_context.TipodeHorarios, "Id", "Id");
            return View();
        }

        // POST: Empleado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JefeInmediatoId,TipoDeHorarioId,Dui,Nombre,Apellido,Telefono,Correo,Estado,FechaContraInicial,FechaContraFinal,Usuario,Contraseña,SalarioBase,PuestoTrabajoId")] Empleado empleado)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Errores de validación:");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                ViewData["JefeInmediatoId"] = new SelectList(_context.Empleados, "Id", "Id", empleado.JefeInmediatoId);
                ViewData["PuestoTrabajoId"] = new SelectList(_context.PuestoTrabajos, "Id", "NombrePuesto", empleado.PuestoTrabajoId);
                ViewData["TipoDeHorarioId"] = new SelectList(_context.TipodeHorarios, "Id", "Id", empleado.TipoDeHorarioId);
                return View(empleado);
            }

            _context.Add(empleado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: Empleado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            ViewData["JefeInmediatoId"] = new SelectList(_context.Empleados, "Id", "Id", empleado.JefeInmediatoId);
            ViewData["PuestoTrabajoId"] = new SelectList(_context.PuestoTrabajos, "Id", "NombrePuesto", empleado.PuestoTrabajoId);
            ViewData["TipoDeHorarioId"] = new SelectList(_context.TipodeHorarios, "Id", "Id", empleado.TipoDeHorarioId);
            return View(empleado);
        }

        // POST: Empleado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JefeInmediatoId,TipoDeHorarioId,Dui,Nombre,Apellido,Telefono,Correo,Estado,FechaContraInicial,FechaContraFinal,Usuario,Contraseña,SalarioBase,PuestoTrabajoId")] Empleado empleado)
        {
            if (id != empleado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.Id))
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
            ViewData["JefeInmediatoId"] = new SelectList(_context.Empleados, "Id", "Id", empleado.JefeInmediatoId);
            ViewData["PuestoTrabajoId"] = new SelectList(_context.PuestoTrabajos, "Id", "NombrePuesto", empleado.PuestoTrabajoId);
            ViewData["TipoDeHorarioId"] = new SelectList(_context.TipodeHorarios, "Id", "NombreHorario", empleado.TipoDeHorarioId);
            return View(empleado);
        }

        // GET: Empleado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.JefeInmediato)
                .Include(e => e.PuestoTrabajo)
                .Include(e => e.TipoDeHorario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado != null)
            {
                _context.Empleados.Remove(empleado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(int id)
        {
            return _context.Empleados.Any(e => e.Id == id);
        }



        //CONSULTA PARA OBTENER SALARIO SEGUN PUESTO
        [HttpGet]
        public async Task<IActionResult> GetSalarioBase(int puestoTrabajoId)
        {
            var puesto = await _context.PuestoTrabajos
                .Where(p => p.Id == puestoTrabajoId)
                .Select(p => new { SalarioBase = p.SalarioBase })
                .FirstOrDefaultAsync();

            if (puesto == null)
            {
                return NotFound();
            }

            return Json(new { salarioBase = puesto.SalarioBase });
        }

        public async Task<IActionResult> VerificarBono(int empleadoId)
        {
            // Buscar la asignación de bono existente para el empleado
            var asignacionBono = await _context.AsignacionBonos
                .FirstOrDefaultAsync(b => b.EmpleadosId == empleadoId);

            if (asignacionBono != null)
            {
                return RedirectToAction("Edit", "AsignacionBono", new { id = asignacionBono.Id });
            }

            return RedirectToAction("Create", "AsignacionBono", new { empleadoId });
        }

        public async Task<IActionResult> VerificarDescuento(int empleadoId)
        {
            var descuento = await _context.AsignacionDescuentos
                .FirstOrDefaultAsync(d => d.EmpleadosId == empleadoId);

            if (descuento != null)
            {
                return RedirectToAction("Edit", "AsignacionDescuento", new { id = descuento.Id });
            }

            return RedirectToAction("Create", "AsignacionDescuento", new { empleadoId });
        }


    }
}
