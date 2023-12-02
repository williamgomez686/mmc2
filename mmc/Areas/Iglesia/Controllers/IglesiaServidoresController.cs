using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mmc.AccesoDatos.Data;
using mmc.Modelos.IglesiaModels;
using mmc.Modelos.IglesiaModels.lafamiliadedios;
using mmc.Utilidades;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace mmc.Areas.Iglesia.Controllers
{
    [Area("Iglesia")]
    [Authorize(Roles = DS.Role_Admin + "," + DS.Role_Iglesia)]
    public class IglesiaServidoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IglesiaServidoresController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var oServidores = _context.IglesiaServidores.Include(t => t.Departamentos);
            return View(await oServidores.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Servidores(int id)
        {
            var oServidores = _context.IglesiaServidores.Include(t => t.Departamentos).Where(s=>s.DepartamentoId ==id);
            return View(await oServidores.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["DepartamentoId"] = new SelectList(_context.IglesiaDepartamentos, "Id", "Descripcion");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IglesiaServidores model)
        {
            var sevidores = 1;
            if (sevidores == 1)
            {
                model.Estado = true;
                model.Usuario = User.Identity.Name;
                model.FechaAlta = DateTime.Now;

                _context.IglesiaServidores.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartamentoId"] = new SelectList(_context.IglesiaDepartamentos, "Id", "Descripcion", model.DepartamentoId);
            return View(model);
        }

        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oServidores = await _context.IglesiaServidores.FindAsync(id);
            if (oServidores == null)
            {
                return NotFound();
            }
           // ViewData["CEBid"] = new SelectList(_context.CEB_CABs, "Id", "Id", oServidores.CEBid);
            return View(oServidores);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, IglesiaServidores Model)
        {
            if (id != Model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.IglesiaServidores.Update(Model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!ModelExists(Model.Id))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CEBid"] = new SelectList(_context.CEB_CABs, "Id", "Id", Model.CEBid);
            return View(Model);
        }
    }
}
