using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mmc.AccesoDatos.Data;
using mmc.Modelos.TicketModels;

namespace mmc.Areas.HelpDesk.Controllers
{
    [Area("HelpDesk")]
    public class AreaSoporteTKController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AreaSoporteTKController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HelpDesk/AreaSoporteTK
        public async Task<IActionResult> Index()
        {
            return View(await _context.AreaSoporteTK.ToListAsync());
        }

        // GET: HelpDesk/AreaSoporteTK/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var areaSoporteTK = await _context.AreaSoporteTK
                .FirstOrDefaultAsync(m => m.Id == id);
            if (areaSoporteTK == null)
            {
                return NotFound();
            }

            return View(areaSoporteTK);
        }

        // GET: HelpDesk/AreaSoporteTK/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HelpDesk/AreaSoporteTK/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,UsuarioAlta,UsuarioModifica,FechaAlta,Fechamodifica,Estado")] AreaSoporteTK areaSoporteTK)
        {
            if (ModelState.IsValid)
            {
                _context.Add(areaSoporteTK);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(areaSoporteTK);
        }

        // GET: HelpDesk/AreaSoporteTK/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var areaSoporteTK = await _context.AreaSoporteTK.FindAsync(id);
            if (areaSoporteTK == null)
            {
                return NotFound();
            }
            return View(areaSoporteTK);
        }

        // POST: HelpDesk/AreaSoporteTK/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,UsuarioAlta,UsuarioModifica,FechaAlta,Fechamodifica,Estado")] AreaSoporteTK areaSoporteTK)
        {
            if (id != areaSoporteTK.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(areaSoporteTK);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AreaSoporteTKExists(areaSoporteTK.Id))
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
            return View(areaSoporteTK);
        }

        // GET: HelpDesk/AreaSoporteTK/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var areaSoporteTK = await _context.AreaSoporteTK
                .FirstOrDefaultAsync(m => m.Id == id);
            if (areaSoporteTK == null)
            {
                return NotFound();
            }

            return View(areaSoporteTK);
        }

        // POST: HelpDesk/AreaSoporteTK/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var areaSoporteTK = await _context.AreaSoporteTK.FindAsync(id);
            _context.AreaSoporteTK.Remove(areaSoporteTK);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AreaSoporteTKExists(int id)
        {
            return _context.AreaSoporteTK.Any(e => e.Id == id);
        }
    }
}
