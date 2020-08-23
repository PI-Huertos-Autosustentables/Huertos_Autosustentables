using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Huertos_Autosustentables.Data;
using Huertos_Autosustentables.Models;
using Microsoft.AspNetCore.Authorization;

namespace Huertos_Autosustentables.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class TiposController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TiposController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tipos
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoCultivo.ToListAsync());
        }

        // GET: Tipos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoCultivo = await _context.TipoCultivo
                .FirstOrDefaultAsync(m => m.IdTipoCultivo == id);
            if (tipoCultivo == null)
            {
                return NotFound();
            }

            return View(tipoCultivo);
        }

        // GET: Tipos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tipos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoCultivo,NombreTipoCultivos,CaracteristicasTipoCultivos")] TipoCultivo tipoCultivo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoCultivo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoCultivo);
        }

        // GET: Tipos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoCultivo = await _context.TipoCultivo.FindAsync(id);
            if (tipoCultivo == null)
            {
                return NotFound();
            }
            return View(tipoCultivo);
        }

        // POST: Tipos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipoCultivo,NombreTipoCultivos,CaracteristicasTipoCultivos")] TipoCultivo tipoCultivo)
        {
            if (id != tipoCultivo.IdTipoCultivo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoCultivo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoCultivoExists(tipoCultivo.IdTipoCultivo))
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
            return View(tipoCultivo);
        }

        // GET: Tipos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoCultivo = await _context.TipoCultivo
                .FirstOrDefaultAsync(m => m.IdTipoCultivo == id);
            if (tipoCultivo == null)
            {
                return NotFound();
            }

            return View(tipoCultivo);
        }

        // POST: Tipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoCultivo = await _context.TipoCultivo.FindAsync(id);
            _context.TipoCultivo.Remove(tipoCultivo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoCultivoExists(int id)
        {
            return _context.TipoCultivo.Any(e => e.IdTipoCultivo == id);
        }
    }
}
