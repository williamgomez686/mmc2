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
    public class EstadosTKController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstadosTKController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HelpDesk/EstadosTK
        public async Task<IActionResult> Index()
        {
            return View(await _context.EstadosTKs.ToListAsync());
        }

        // GET: HelpDesk/EstadosTK/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadosTK = await _context.EstadosTKs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estadosTK == null)
            {
                return NotFound();
            }

            return View(estadosTK);
        }

        // GET: HelpDesk/EstadosTK/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HelpDesk/EstadosTK/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,UsuarioAlta,UsuarioModifica,FechaAlta,Fechamodifica,Estado")] EstadosTK estadosTK)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estadosTK);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estadosTK);
        }

        // GET: HelpDesk/EstadosTK/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadosTK = await _context.EstadosTKs.FindAsync(id);
            if (estadosTK == null)
            {
                return NotFound();
            }
            return View(estadosTK);
        }

        // POST: HelpDesk/EstadosTK/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,UsuarioAlta,UsuarioModifica,FechaAlta,Fechamodifica,Estado")] EstadosTK estadosTK)
        {
            if (id != estadosTK.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estadosTK);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadosTKExists(estadosTK.Id))
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
            return View(estadosTK);
        }

        // GET: HelpDesk/EstadosTK/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadosTK = await _context.EstadosTKs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estadosTK == null)
            {
                return NotFound();
            }

            return View(estadosTK);
        }

        // POST: HelpDesk/EstadosTK/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estadosTK = await _context.EstadosTKs.FindAsync(id);
            _context.EstadosTKs.Remove(estadosTK);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstadosTKExists(int id)
        {
            return _context.EstadosTKs.Any(e => e.Id == id);
        }
    }
}
