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
    public class IglesiaDepartamentosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IglesiaDepartamentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            return _context.IglesiaDepartamentos != null ?
                        View(await _context.IglesiaDepartamentos.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.IglesiaDepartamentos'  is null.");
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IglesiaDepartamentos model)
        {
            if (ModelState.IsValid)
            {
                model.Estado = true;
                model.FechaAlta = DateTime.Now;
                model.Usuario = User.Identity.Name;
                _context.IglesiaDepartamentos.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

    }
}
