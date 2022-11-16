using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mmc.AccesoDatos.Data;
using mmc.Modelos.IglesiaModels;
using System.Linq;
using System.Threading.Tasks;

namespace mmc.Areas.HelpDesk.Controllers
{
    [Area("HelpDesk")]
    public class Prueba2Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public Prueba2Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HelpDesk/Prueba2
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CEB_DETs.Include(c => c.Cabecera);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: HelpDesk/Prueba2/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cEB_DET = await _context.CEB_DETs
                .Include(c => c.Cabecera)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cEB_DET == null)
            {
                return NotFound();
            }

            return View(cEB_DET);
        }

        // GET: HelpDesk/Prueba2/Create
        public IActionResult Create(string id)
        {
            ViewData["CEBid"] = new SelectList(_context.CEB_CABs, "Id", "Id");
            ViewBag.id=id;
            return View();
        }

        // POST: HelpDesk/Prueba2/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cistianos,NoCistianos,Ninios,Total,Convertidos,Reconcilia,Ofrenda,Foto,CEBid,Usuario,UsuarioModifica,FechaAlta,Fechamodifica,Estado")] CEB_DET cEB_DET)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cEB_DET);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CEBid"] = new SelectList(_context.CEB_CABs, "Id", "Id", cEB_DET.CEBid);
            return View(cEB_DET);
        }

        // GET: HelpDesk/Prueba2/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cEB_DET = await _context.CEB_DETs.FindAsync(id);
            if (cEB_DET == null)
            {
                return NotFound();
            }
            ViewData["CEBid"] = new SelectList(_context.CEB_CABs, "Id", "Id", cEB_DET.CEBid);
            return View(cEB_DET);
        }

        // POST: HelpDesk/Prueba2/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Cistianos,NoCistianos,Ninios,Total,Convertidos,Reconcilia,Ofrenda,Foto,CEBid,Usuario,UsuarioModifica,FechaAlta,Fechamodifica,Estado")] CEB_DET cEB_DET)
        {
            if (id != cEB_DET.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cEB_DET);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CEB_DETExists(cEB_DET.Id))
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
            ViewData["CEBid"] = new SelectList(_context.CEB_CABs, "Id", "Id", cEB_DET.CEBid);
            return View(cEB_DET);
        }

        // GET: HelpDesk/Prueba2/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cEB_DET = await _context.CEB_DETs
                .Include(c => c.Cabecera)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cEB_DET == null)
            {
                return NotFound();
            }

            return View(cEB_DET);
        }

        // POST: HelpDesk/Prueba2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cEB_DET = await _context.CEB_DETs.FindAsync(id);
            _context.CEB_DETs.Remove(cEB_DET);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CEB_DETExists(string id)
        {
            return _context.CEB_DETs.Any(e => e.Id == id);
        }
    }
}
