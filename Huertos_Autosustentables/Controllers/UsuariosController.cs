using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Huertos_Autosustentables.Data;
using Huertos_Autosustentables.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Mvc.Rendering;

using Microsoft.AspNetCore.Hosting;
using System.IO;


namespace Huertos_Autosustentables.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

//Costa:
        public async Task<IActionResult> Costa(string searchString)
        {
            var cultivos = from m in _context.Cultivo
                           select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                cultivos = cultivos.Where(s => s.NombreCultivos.Contains(searchString));
                return View(await cultivos.ToListAsync());
            }

            return View(await _context.Cultivo.Where(cultivo => cultivo.IdRegiones == 8).ToListAsync());
        }

//Sierra
        public async Task<IActionResult> Sierra(string searchString)
        {
            var cultivos = from m in _context.Cultivo
                           select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                cultivos = cultivos.Where(s => s.NombreCultivos.Contains(searchString));
                return View(await cultivos.ToListAsync());
            }

            return View(await _context.Cultivo.Where(cultivo => cultivo.IdRegiones == 11).ToListAsync());
        }

//Amazonia

        public async Task<IActionResult> Amazonia(string searchString)
        {
            var cultivos = from m in _context.Cultivo
                           select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                cultivos = cultivos.Where(s => s.NombreCultivos.Contains(searchString));
                return View(await cultivos.ToListAsync());
            }

            return View(await _context.Cultivo.Where(cultivo => cultivo.IdRegiones == 10).ToListAsync());
        }

//Galapagos
        public async Task<IActionResult> Galapagos(string searchString)
        {
            var cultivos = from m in _context.Cultivo
                           select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                cultivos = cultivos.Where(s => s.NombreCultivos.Contains(searchString));
                return View(await cultivos.ToListAsync());
            }

            return View(await _context.Cultivo.Where(cultivo => cultivo.IdRegiones == 9).ToListAsync());
        }

  // Pagina Principal usuario

    public IActionResult Index()
        {
            return View();
        }
   
      /* [HttpPost]
        public string Sierra(string searchString, bool notUsed)
        {
            return "From [HttpPost]Sierra: filter on " + searchString;
        }*/

 // GET: Cultivos/Dealle/

        public async Task<IActionResult> Detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cultivo = await _context.DetalleUsersCultivo.Include(d => d.FK_)
                .FirstOrDefaultAsync(m => m.IdCultivo == id);
            var cul = await _context.Cultivo.FindAsync(cultivo.IdCultivo);
            cultivo.FK_ = cul;
            if (cultivo == null)
            {
                return NotFound();
            }

            return View(cultivo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Required] string NombreCultivos, string IntroduccionCultivos,
          string CuerpoCultivos, string RecomendacionesCultivos, int IdTipoCultivo, int IdRegiones)
        {
            if (ModelState.IsValid)
            {
                //var user = await _context.GetUserAsync(User);

                var region = new Cultivo();
                {
                    //region.UserId = await _context.GetUserIdAsync(user);
                    //region.UserId = await _context.GetUserIdAsync(user);
                    region.NombreCultivos = NombreCultivos;
                    region.IntroduccionCultivos = IntroduccionCultivos;
                    region.CuerpoCultivos = CuerpoCultivos;
                    region.RecomendacionesCultivos = RecomendacionesCultivos;
                    region.IdTipoCultivo = IdTipoCultivo;
                    region.IdRegiones = IdRegiones;
                }
                if (region != null)
                {
                    _context.Add(region);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Costa));
                }
            }
            return View();
        }
    }
}