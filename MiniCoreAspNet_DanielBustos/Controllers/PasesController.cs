using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniCoreAspNet_DanielBustos.Models;

namespace MiniCoreAspNet_DanielBustos.Controllers
{
    public class PasesController : Controller
    {
        private readonly dbContext _context;
        public PasesController(dbContext context)
        {
            _context = context;
        }

        // GET: Pases
        public async Task<IActionResult> Index()
        {
            var dbContext = _context.Pases.Include(p => p.Usuario);
            return View(await dbContext.ToListAsync());
        }

        // GET: Pases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pase = await _context.Pases
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.PaseID == id);
            if (pase == null)
            {
                return NotFound();
            }

            return View(pase);
        }

        // GET: Pases/Create
        public IActionResult Create()
        {
            ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "UsuarioID", "UsuarioID");
            return View();
        }

        // POST: Pases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaseID,UsuarioID,FechaCompra,TtipoPase,FinTentativo,pasesRestantes,saldoRestante")] Pase pase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "UsuarioID", "UsuarioID", pase.UsuarioID);
            return View(pase);
        }

        // GET: Pases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pase = await _context.Pases.FindAsync(id);
            if (pase == null)
            {
                return NotFound();
            }
            ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "UsuarioID", "UsuarioID", pase.UsuarioID);
            return View(pase);
        }

        // POST: Pases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaseID,UsuarioID,FechaCompra,TtipoPase,FinTentativo,pasesRestantes,saldoRestante")] Pase pase)
        {
            if (id != pase.PaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaseExists(pase.PaseID))
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
            ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "UsuarioID", "UsuarioID", pase.UsuarioID);
            return View(pase);
        }

        // GET: Pases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pase = await _context.Pases
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.PaseID == id);
            if (pase == null)
            {
                return NotFound();
            }

            return View(pase);
        }

        // POST: Pases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pase = await _context.Pases.FindAsync(id);
            _context.Pases.Remove(pase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaseExists(int id)
        {
            return _context.Pases.Any(e => e.PaseID == id);
        }
        public ActionResult Calcular(DateTime fechainicio)
        {
            DateTime hoy = DateTime.Now;
            var pases = _context.Pases.Include(p => p.Usuario).ToList();
            List<Pase> activos = new List<Pase>();
            foreach (Pase pass in pases)
            {
                TimeSpan res = hoy - fechainicio;
                double pasesUsados = res.Days;
                if (pass.TtipoPase.Equals("Mensual"))
                {
                    double pasesRestantes = 96 + pasesUsados;
                    pass.FinTentativo = hoy.AddDays(pasesRestantes);
                    pass.pasesRestantes = Convert.ToInt32(pasesRestantes);
                    pass.saldoRestante = pasesRestantes * 0.26 ;

                }
                else if (pass.TtipoPase.Equals("Semestral"))
                {
                    double pasesRestantes = 576 + pasesUsados;
                    pass.FinTentativo = hoy.AddDays(pasesRestantes);
                    pass.pasesRestantes = Convert.ToInt32(pasesRestantes);
                    pass.saldoRestante = pasesRestantes * 0.087;
                }
                else
                {
                    double pasesRestantes = 1052 + pasesUsados;
                    pass.FinTentativo = hoy.AddDays(pasesRestantes);
                    pass.pasesRestantes = Convert.ToInt32(pasesRestantes);
                    pass.saldoRestante = pasesRestantes * 0.076;
                }

                if (pass.pasesRestantes > 0)
                {
                    activos.Add(pass);
                }
                _context.Entry(pass).State = EntityState.Modified;
                _context.SaveChanges();
            }

            return View("Index", activos);
        }

    }
}
