using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mmc.AccesoDatos.Data;
using mmc.Modelos.IglesiaModels;

namespace mmc.Areas.HelpDesk.Controllers
{
    [Area("HelpDesk")]
    public class PreubaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PreubaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HelpDesk/Preuba
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CEB_CABs.Include(c => c.MiembroLider).Include(c => c.TiposCEB);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: HelpDesk/Preuba/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cEB_CAB = await _context.CEB_CABs
                .Include(c => c.MiembroLider)
                .Include(c => c.TiposCEB)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cEB_CAB == null)
            {
                return NotFound();
            }

            return View(cEB_CAB);
        }

        // GET: HelpDesk/Preuba/Create
        public IActionResult Create()
        {
            ViewData["MiembrosCEBid"] = new SelectList(_context.Miembros, "Id", "Name");
            ViewData["TipoCebId"] = new SelectList(_context.TiposCEB, "Id", "Tipo");
            var test= new SelectList(_context.TiposCEB, "Id", "Tipo");
            return View();
        }

        // POST: HelpDesk/Preuba/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Hora,Foto,TipoCebId,MiembrosCEBid,Usuario,UsuarioModifica,FechaAlta,Fechamodifica,Estado")] CEB_CAB cEB_CAB)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(cEB_CAB);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MiembrosCEBid"] = new SelectList(_context.Miembros, "Id", "Name", cEB_CAB.MiembrosCEBid);
            ViewData["TipoCebId"] = new SelectList(_context.TiposCEB, "Id", "Tipo", cEB_CAB.TipoCebId);
            return View(cEB_CAB);
        }

        // GET: HelpDesk/Preuba/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cEB_CAB = await _context.CEB_CABs.FindAsync(id);
            if (cEB_CAB == null)
            {
                return NotFound();
            }
            ViewData["MiembrosCEBid"] = new SelectList(_context.Miembros, "Id", "Name", cEB_CAB.MiembrosCEBid);
            ViewData["TipoCebId"] = new SelectList(_context.TiposCEB, "Id", "Tipo", cEB_CAB.TipoCebId);
            return View(cEB_CAB);
        }

        // POST: HelpDesk/Preuba/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Hora,Foto,TipoCebId,MiembrosCEBid,Usuario,UsuarioModifica,FechaAlta,Fechamodifica,Estado")] CEB_CAB cEB_CAB)
        {
            if (id != cEB_CAB.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cEB_CAB);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CEB_CABExists(cEB_CAB.Id))
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
            ViewData["MiembrosCEBid"] = new SelectList(_context.Miembros, "Id", "Name", cEB_CAB.MiembrosCEBid);
            ViewData["TipoCebId"] = new SelectList(_context.TiposCEB, "Id", "Tipo", cEB_CAB.TipoCebId);
            return View(cEB_CAB);
        }

        // GET: HelpDesk/Preuba/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cEB_CAB = await _context.CEB_CABs
                .Include(c => c.MiembroLider)
                .Include(c => c.TiposCEB)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cEB_CAB == null)
            {
                return NotFound();
            }

            return View(cEB_CAB);
        }

        // POST: HelpDesk/Preuba/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cEB_CAB = await _context.CEB_CABs.FindAsync(id);
            _context.CEB_CABs.Remove(cEB_CAB);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CEB_CABExists(string id)
        {
            return _context.CEB_CABs.Any(e => e.Id == id);
        }
    }
}
