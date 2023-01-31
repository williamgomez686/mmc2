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
    public class EmpresasTKController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmpresasTKController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HelpDesk/EmpresasTK
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmpresasTK.ToListAsync());
        }

        // GET: HelpDesk/EmpresasTK/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresaTK = await _context.EmpresasTK
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empresaTK == null)
            {
                return NotFound();
            }

            return View(empresaTK);
        }

        // GET: HelpDesk/EmpresasTK/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HelpDesk/EmpresasTK/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,UsuarioAlta,UsuarioModifica,FechaAlta,Fechamodifica,Estado")] EmpresaTK empresaTK)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empresaTK);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empresaTK);
        }

        // GET: HelpDesk/EmpresasTK/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresaTK = await _context.EmpresasTK.FindAsync(id);
            if (empresaTK == null)
            {
                return NotFound();
            }
            return View(empresaTK);
        }

        // POST: HelpDesk/EmpresasTK/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,UsuarioAlta,UsuarioModifica,FechaAlta,Fechamodifica,Estado")] EmpresaTK empresaTK)
        {
            if (id != empresaTK.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empresaTK);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpresaTKExists(empresaTK.Id))
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
            return View(empresaTK);
        }

        // GET: HelpDesk/EmpresasTK/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresaTK = await _context.EmpresasTK
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empresaTK == null)
            {
                return NotFound();
            }

            return View(empresaTK);
        }

        // POST: HelpDesk/EmpresasTK/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empresaTK = await _context.EmpresasTK.FindAsync(id);
            _context.EmpresasTK.Remove(empresaTK);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpresaTKExists(int id)
        {
            return _context.EmpresasTK.Any(e => e.Id == id);
        }
    }
}
