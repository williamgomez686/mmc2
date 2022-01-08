using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mmc.AccesoDatos.Data;
using mmc.Modelos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using mmc.Utilidades;

namespace mmc.Areas.HelpDesk.Controllers
{
    [Area("HelpDesk")]
    [Authorize(Roles = DS.Role_Admin + "," + DS.Role_Ticket)]
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HelpDesk/Tickets
        public async Task<IActionResult> Index()
        {
            if(User.IsInRole(DS.Role_Admin))
            {
                return View(await _context.Tickets.ToListAsync());
            }
            else
            {
                var nombre = User.Identity.Name;
                var Ticket = await _context.Tickets.Where(u => u.UsuarioAplicacionId == nombre).ToListAsync();
                return View(Ticket);
            }

        }

        // GET: HelpDesk/Tickets/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: HelpDesk/Tickets/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            var nombre = User.Identity.Name;//obtiene el nombre del usuario de la session activa
            var usuario = User.Identity.IsAuthenticated.ToString();
            var oTicket = new Ticket();
            if (!ModelState.IsValid)
            {
                oTicket.Id = Guid.NewGuid().ToString();
                oTicket.UsuarioAplicacionId = nombre;
                oTicket.Estado = "Activo";
                oTicket.Asunto = ticket.Asunto;
                oTicket.Descripcion = ticket.Descripcion;
                oTicket.FechaAlta = DateTime.Now;

                _context.Add(oTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // GET: HelpDesk/Tickets/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: HelpDesk/Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UsuarioAplicacionId,Estado,Asunto,Descripcion,FechaAlta,Solucion,FechaSolucion,Tecnico")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
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
            return View(ticket);
        }

        // GET: HelpDesk/Tickets/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: HelpDesk/Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(string id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
