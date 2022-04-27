using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mmc.AccesoDatos.Data;
using mmc.Modelos;

namespace mmc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AsistenciaMiembrosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AsistenciaMiembrosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AsistenciaMiembros
        public async Task<IActionResult> Index()
        {
            return View(await _context.AsistenciaMiembros.ToListAsync());
        }

        // GET: Admin/AsistenciaMiembros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asistenciaMiembros = await _context.AsistenciaMiembros
                .FirstOrDefaultAsync(m => m.id == id);
            if (asistenciaMiembros == null)
            {
                return NotFound();
            }

            return View(asistenciaMiembros);
        }

        // GET: Admin/AsistenciaMiembros/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AsistenciaMiembros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,NumeroContacto,MiembroFamilia,Cantidad")] AsistenciaMiembros asistenciaMiembros)
        {
            //if (ModelState.IsValid)
            //{
            _context.Add(asistenciaMiembros);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //}
            //return View(asistenciaMiembros);
        }

        // GET: Admin/AsistenciaMiembros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asistenciaMiembros = await _context.AsistenciaMiembros.FindAsync(id);
            if (asistenciaMiembros == null)
            {
                return NotFound();
            }
            return View(asistenciaMiembros);
        }

        // POST: Admin/AsistenciaMiembros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,NumeroContacto,MiembroFamilia,Cantidad")] AsistenciaMiembros asistenciaMiembros)
        {
            if (id != asistenciaMiembros.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asistenciaMiembros);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsistenciaMiembrosExists(asistenciaMiembros.id))
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
            return View(asistenciaMiembros);
        }

        // GET: Admin/AsistenciaMiembros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asistenciaMiembros = await _context.AsistenciaMiembros
                .FirstOrDefaultAsync(m => m.id == id);
            if (asistenciaMiembros == null)
            {
                return NotFound();
            }

            return View(asistenciaMiembros);
        }

        // POST: Admin/AsistenciaMiembros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asistenciaMiembros = await _context.AsistenciaMiembros.FindAsync(id);
            _context.AsistenciaMiembros.Remove(asistenciaMiembros);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsistenciaMiembrosExists(int id)
        {
            return _context.AsistenciaMiembros.Any(e => e.id == id);
        }
    }
}
