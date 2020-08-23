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
    public class ClimasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClimasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Climas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clima.ToListAsync());
        }

        // GET: Climas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clima = await _context.Clima
                .FirstOrDefaultAsync(m => m.IdClima == id);
            if (clima == null)
            {
                return NotFound();
            }

            return View(clima);
        }

        // GET: Climas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Climas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdClima,NombreClima,CaracteristicasClima")] Clima clima)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clima);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clima);
        }

        // GET: Climas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clima = await _context.Clima.FindAsync(id);
            if (clima == null)
            {
                return NotFound();
            }
            return View(clima);
        }

        // POST: Climas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdClima,NombreClima,CaracteristicasClima")] Clima clima)
        {
            if (id != clima.IdClima)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clima);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClimaExists(clima.IdClima))
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
            return View(clima);
        }

        // GET: Climas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clima = await _context.Clima
                .FirstOrDefaultAsync(m => m.IdClima == id);
            if (clima == null)
            {
                return NotFound();
            }

            return View(clima);
        }

        // POST: Climas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clima = await _context.Clima.FindAsync(id);
            _context.Clima.Remove(clima);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClimaExists(int id)
        {
            return _context.Clima.Any(e => e.IdClima == id);
        }
    }
}
