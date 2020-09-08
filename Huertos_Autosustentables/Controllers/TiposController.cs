using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Huertos_Autosustentables.Data;
using Huertos_Autosustentables.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;

namespace Huertos_Autosustentables.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class TiposController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public TiposController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Tipos
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoCultivo.ToListAsync());
        }

        // GET: Tipos/Detalles/5
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

        // GET: Tipos/Crear
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tipos/Crear

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TipoCultivo tipoCultivo)
        {
            if (ModelState.IsValid)
            {
                //guarda la imagen en wwwroot/image
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(tipoCultivo.ImageFile.FileName);
                string extension = Path.GetExtension(tipoCultivo.ImageFile.FileName);

                //guarda el nombre de la imagen (ImageName)
                tipoCultivo.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                string path = Path.Combine(wwwRootPath + "/img/tipo", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await tipoCultivo.ImageFile.CopyToAsync(fileStream);
                }
                _context.Add(tipoCultivo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoCultivo);
        }

        // GET: Tipos/Editar/5
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

        // POST: Tipos/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipoCultivo,NombreTipoCultivos,CaracteristicasTipoCultivos,ImageName")] TipoCultivo tipoCultivo)
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

        // GET: Tipos/Borrar/5
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

        // POST: Tipos/Borrar/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoCultivo = await _context.TipoCultivo.FindAsync(id);

            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "img/tipo/", tipoCultivo.ImageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);

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
