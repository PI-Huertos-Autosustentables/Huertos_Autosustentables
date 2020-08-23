using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Huertos_Autosustentables.Data;
using Huertos_Autosustentables.Models;

namespace Huertos_Autosustentables.Controllers
{
    public class CultivosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CultivosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cultivos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cultivo.ToListAsync());
        }

        // GET: Cultivos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cultivo = await _context.Cultivo
                .FirstOrDefaultAsync(m => m.IdCultivos == id);
            if (cultivo == null)
            {
                return NotFound();
            }

            return View(cultivo);
        }

        // GET: Cultivos/Create
        public IActionResult Create()
        {
            ViewData["IdTipoCultivo"] = new SelectList(_context.TipoCultivo, "IdTipoCultivo", "NombreTipoCultivos");
            ViewData["IdRegiones"] = new SelectList(_context.Region, "IdRegiones", "NombreRegiones");

            return View();
        }

        // POST: Cultivos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCultivos,NombreCultivos,IntroduccionCultivos,CuerpoCultivos,RecomendacionesCultivos,IdTipoCultivo,IdRegiones")] Cultivo cultivo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cultivo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cultivo);
        }

        // GET: Cultivos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cultivo = await _context.Cultivo.FindAsync(id);
            if (cultivo == null)
            {
                return NotFound();
            }
            return View(cultivo);
        }

        // POST: Cultivos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCultivos,NombreCultivos,IntroduccionCultivos,CuerpoCultivos,RecomendacionesCultivos,IdTipoCultivo,IdRegiones")] Cultivo cultivo)
        {
            if (id != cultivo.IdCultivos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cultivo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CultivoExists(cultivo.IdCultivos))
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
            return View(cultivo);
        }

        // GET: Cultivos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cultivo = await _context.Cultivo
                .FirstOrDefaultAsync(m => m.IdCultivos == id);
            if (cultivo == null)
            {
                return NotFound();
            }

            return View(cultivo);
        }

        // POST: Cultivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cultivo = await _context.Cultivo.FindAsync(id);
            _context.Cultivo.Remove(cultivo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CultivoExists(int id)
        {
            return _context.Cultivo.Any(e => e.IdCultivos == id);
        }
    }
}
