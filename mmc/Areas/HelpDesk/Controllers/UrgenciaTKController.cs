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
    public class UrgenciaTKController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UrgenciaTKController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HelpDesk/UrgenciaTK
        public async Task<IActionResult> Index()
        {
            return View(await _context.UrgenciasTK.ToListAsync());
        }

        // GET: HelpDesk/UrgenciaTK/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urgenciaTK = await _context.UrgenciasTK
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urgenciaTK == null)
            {
                return NotFound();
            }

            return View(urgenciaTK);
        }

        // GET: HelpDesk/UrgenciaTK/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HelpDesk/UrgenciaTK/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,UsuarioAlta,UsuarioModifica,FechaAlta,Fechamodifica,Estado")] UrgenciaTK urgenciaTK)
        {
            if (ModelState.IsValid)
            {
                _context.Add(urgenciaTK);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(urgenciaTK);
        }

        // GET: HelpDesk/UrgenciaTK/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urgenciaTK = await _context.UrgenciasTK.FindAsync(id);
            if (urgenciaTK == null)
            {
                return NotFound();
            }
            return View(urgenciaTK);
        }

        // POST: HelpDesk/UrgenciaTK/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,UsuarioAlta,UsuarioModifica,FechaAlta,Fechamodifica,Estado")] UrgenciaTK urgenciaTK)
        {
            if (id != urgenciaTK.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(urgenciaTK);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UrgenciaTKExists(urgenciaTK.Id))
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
            return View(urgenciaTK);
        }

        // GET: HelpDesk/UrgenciaTK/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urgenciaTK = await _context.UrgenciasTK
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urgenciaTK == null)
            {
                return NotFound();
            }

            return View(urgenciaTK);
        }

        // POST: HelpDesk/UrgenciaTK/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var urgenciaTK = await _context.UrgenciasTK.FindAsync(id);
            _context.UrgenciasTK.Remove(urgenciaTK);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UrgenciaTKExists(int id)
        {
            return _context.UrgenciasTK.Any(e => e.Id == id);
        }
    }
}
