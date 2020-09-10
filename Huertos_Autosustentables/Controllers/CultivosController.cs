using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Huertos_Autosustentables.Data;
using Huertos_Autosustentables.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Data.Common;
using Microsoft.AspNetCore.Authorization;

namespace Huertos_Autosustentables.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class CultivosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CultivosController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Cultivos
        //consulta para los cultvios
        public async Task<IActionResult> Index(string searchString)
        {
            var cultivos = from m in _context.Cultivo
                           select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                cultivos = cultivos.Where(s => s.NombreCultivos.Contains(searchString));
            }
            ViewData["IdTipoCultivo"] = new SelectList(_context.TipoCultivo, "IdTipoCultivo", "NombreTipoCultivos");
            ViewData["IdRegiones"] = new SelectList(_context.Region, "IdRegiones", "NombreRegiones");
            return View(await cultivos.ToListAsync());

        }


        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }

        // GET: Cultivos/Detalles/5
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
            ViewData["IdTipoCultivo"] = new SelectList(_context.TipoCultivo, "IdTipoCultivo", "NombreTipoCultivos");
            ViewData["IdRegiones"] = new SelectList(_context.Region, "IdRegiones", "NombreRegiones");
            return View(cultivo);
        }

        // GET: Cultivos/Crear
        public IActionResult Create()
        {
            ViewData["IdTipoCultivo"] = new SelectList(_context.TipoCultivo, "IdTipoCultivo", "NombreTipoCultivos");
            ViewData["IdRegiones"] = new SelectList(_context.Region, "IdRegiones", "NombreRegiones");

            return View();
        }

        // POST: Cultivos/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cultivo cultivo)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(cultivo.ImageFile.FileName);
                string extension = Path.GetExtension(cultivo.ImageFile.FileName);

                //guarda el nombre de la imagen (ImageName)
                cultivo.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                string path = Path.Combine(wwwRootPath + "/img/cultivo", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await cultivo.ImageFile.CopyToAsync(fileStream);
                }

                _context.Add(cultivo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cultivo);
        }

        // GET: Cultivos/Editar/5
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
            ViewData["IdTipoCultivo"] = new SelectList(_context.TipoCultivo, "IdTipoCultivo", "NombreTipoCultivos");
            ViewData["IdRegiones"] = new SelectList(_context.Region, "IdRegiones", "NombreRegiones");
            return View(cultivo);
        }

        // POST: Cultivos/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCultivos,NombreCultivos,IntroduccionCultivos,CuerpoCultivos,RecomendacionesCultivos,IdTipoCultivo,IdRegiones,ImageName")] Cultivo cultivo)
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

        // GET: Cultivos/Borrar/5
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
            ViewData["IdTipoCultivo"] = new SelectList(_context.TipoCultivo, "IdTipoCultivo", "NombreTipoCultivos");
            ViewData["IdRegiones"] = new SelectList(_context.Region, "IdRegiones", "NombreRegiones");
            return View(cultivo);
        }

        // POST: Cultivos/Borrar/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cultivo = await _context.Cultivo.FindAsync(id);

            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "img/cultivo/", cultivo.ImageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);

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
