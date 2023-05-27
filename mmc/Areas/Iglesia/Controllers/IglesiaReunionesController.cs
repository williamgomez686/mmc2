using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mmc.AccesoDatos.Data;
using mmc.Modelos.IglesiaModels.lafamiliadedios;
using mmc.Utilidades;
using System;
using System.Threading.Tasks;

namespace mmc.Areas.Iglesia.Controllers
{
    [Area("Iglesia")]
    [Authorize(Roles = DS.Role_Admin + "," + DS.Role_Iglesia)]
    public class IglesiaReunionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IglesiaReunionesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return _context.IglesiaReuniones != null ?
                        View(await _context.IglesiaReuniones.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.IglesiaReuniones'  is null.");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IglesiaReuniones modelo)
        {
            var uno = 1;
            if (uno == 1)
            {
                modelo.Estado = true;
                modelo.FechaAlta = DateTime.Now;
                modelo.Usuario = User.Identity.Name;
                //var nueva_fecha = //modelo.ReunionFecha.ToUniversalTime();
                //modelo.ReunionFecha = nueva_fecha;
                _context.IglesiaReuniones.Add(modelo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(modelo);
        }
    }
}
