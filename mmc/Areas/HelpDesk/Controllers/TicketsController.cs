using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mmc.AccesoDatos.Data;
using mmc.AccesoDatos.Repositorios.IRepositorio;
using mmc.Modelos.common;
using mmc.Modelos.TicketModels;
using mmc.Utilidades;

namespace mmc.Areas.HelpDesk.Controllers
{
    [Area("HelpDesk")]
    [Authorize(Roles = DS.Role_Admin + "," + DS.Role_Iglesia + "," +DS.Role_Bodega + "," + DS.Role_Contabilidad + "," + DS.Role_Setegua + "," + DS.Role_Canal27)]
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnidadTrabajo _unidadTrabajo;

        public TicketsController(ApplicationDbContext context, IUnidadTrabajo unidadTrabajo)
        {
            _context = context;
            _unidadTrabajo = unidadTrabajo;
        }

        // GET: HelpDesk/Tickets
        public async Task<IActionResult> Index(int pageNumber = 1, string busqueda = "", string busquedaActual = "", string estado = "")
        {
            if (!String.IsNullOrEmpty(busqueda))
            {
                pageNumber = 1;
            }
            else
            {
                busqueda = busquedaActual;
            }

            if (!String.IsNullOrEmpty(estado))
            {
                pageNumber = 1;
            }
            else
            {
                estado = "Pendiente";//DS.EstadoPendiente;
            }
            ViewData["EstadoActual"] = estado;
            ViewData["BusquedaActual"] = busqueda;

            if (pageNumber < 1) { pageNumber = 1; }

            Parametros parametros = new Parametros()
            {
                PageNumber = pageNumber,
                PageSize = 10
            };

            var ListTickets = await _unidadTrabajo.Tickets.ObtenerTodosPaginadoFiltrado(parametros, 
                t=>t.Estado ==true, 
                isTracking:false,
                orderBy: o => o.OrderBy(t=>t.FechaAlta),
                incluirPropiedades: "EstadosTK,Urgencia,AreaSoporte,SedeTK", 
                select: tk=> new Ticket
                {
                    Id = tk.Id,
                    Usuario = tk.Usuario,
                    Asunto = tk.Asunto,
                    Solucion = tk.Solucion,
                    EstadosTK = tk.EstadosTK,
                    Urgencia = tk.Urgencia,
                    AreaSoporte = tk.AreaSoporte,
                    SedeTK = tk.SedeTK,
                });

            if (!string.IsNullOrEmpty(busqueda))// buscar por 
            {

                ListTickets = await _unidadTrabajo.Tickets.ObtenerTodosPaginadoFiltrado(parametros,
                    t => t.Estado == true && 
                    t.Asunto.Contains(busqueda) || 
                    t.Id.Contains(busqueda) || 
                    t.Usuario.Contains(busqueda)|| 
                    t.AreaSoporte.Descripcion.Contains(busqueda),
                    isTracking: false,
                    orderBy: o => o.OrderBy(t => t.FechaAlta),
                    incluirPropiedades: "EstadosTK,Urgencia,AreaSoporte,SedeTK",
                    select: tk => new Ticket
                    {
                        Id = tk.Id,
                        Usuario = tk.Usuario,
                        Asunto = tk.Asunto,
                        Solucion = tk.Solucion,
                        EstadosTK = tk.EstadosTK,
                        Urgencia = tk.Urgencia,
                        AreaSoporte = tk.AreaSoporte,
                        SedeTK = tk.SedeTK,
                    });

            }

            //paginacion 
            ViewData["TotalPaginas"] = ListTickets.MetaData.TotalPages;
            ViewData["TotalPaginas"] = ListTickets.MetaData.TotalPages;
            ViewData["TotalRegistros"] = ListTickets.MetaData.TotalCount;
            ViewData["PageSize"] = ListTickets.MetaData.PageSize;
            ViewData["PageNumber"] = pageNumber;
            ViewData["Previo"] = "disabled";  // clase css para desactivar el boton
            ViewData["Siguiente"] = "";

            if (pageNumber > 1) { ViewData["Previo"] = ""; }
            if (ListTickets.MetaData.TotalPages <= pageNumber) { ViewData["Siguiente"] = "disabled"; }

            return View(ListTickets);
        }

        // GET: HelpDesk/Tickets/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.AreaSoporte)
                .Include(t => t.EstadosTK)
                .Include(t => t.SedeTK)
                .Include(t => t.Urgencia)
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
            ViewData["AreaSoporteId"] = new SelectList(_context.AreaSoporteTK, "Id", "Descripcion");
            ViewData["EstadoTKId"] = new SelectList(_context.EstadosTKs, "Id", "Descripcion");
            ViewData["SedeId"] = new SelectList(_context.EmpresasTK, "Id", "Descripcion");
            ViewData["UrgenciaId"] = new SelectList(_context.UrgenciasTK, "Id", "Descripcion");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ticket ticket)
        {

            if (!User.IsInRole(DS.Role_Admin))
            {
                ticket.Usuario = User.Identity.Name;
                ticket.SedeId = 1;
                ticket.AreaSoporteId = 1;
            }
            var cantidadTIkcets = _context.Tickets.Count();
            ticket.Id = "TK-" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + cantidadTIkcets;
            ticket.FechaAlta = DateTime.Now;
            ticket.Estado = true;
            ticket.UsuarioAlta = User.Identity.Name;
            //ticket.Tecnico = User.Identity.Name;
            ticket.EstadoTKId = 1;

            if (ticket.Asunto != null && ticket.Descripcion != "")
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AreaSoporteId"] = new SelectList(_context.AreaSoporteTK, "Id", "Descripcion", ticket.AreaSoporteId);
            ViewData["EstadoTKId"] = new SelectList(_context.EstadosTKs, "Id", "Descripcion", ticket.EstadoTKId);
            ViewData["SedeId"] = new SelectList(_context.EmpresasTK, "Id", "Descripcion", ticket.SedeId);
            ViewData["UrgenciaId"] = new SelectList(_context.UrgenciasTK, "Id", "Descripcion", ticket.UrgenciaId);
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
            ViewData["AreaSoporteId"] = new SelectList(_context.AreaSoporteTK, "Id", "Descripcion", ticket.AreaSoporteId);
            ViewData["EstadoTKId"] = new SelectList(_context.EstadosTKs, "Id", "Descripcion", ticket.EstadoTKId);
            ViewData["SedeId"] = new SelectList(_context.EmpresasTK, "Id", "Descripcion", ticket.SedeId);
            ViewData["UrgenciaId"] = new SelectList(_context.UrgenciasTK, "Id", "Descripcion", ticket.UrgenciaId);
            return View(ticket);
        }

        // POST: HelpDesk/Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Usuario,Asunto,Descripcion,Solucion,FechaSolucion,Tecnico,ImagenUrl,EstadoTKId,UrgenciaId,AreaSoporteId,SedeId,UsuarioAlta,UsuarioModifica,FechaAlta,Fechamodifica,Estado")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ticket.UsuarioModifica = User.Identity.Name;
                    ticket.Tecnico= User.Identity.Name;
                    ticket.FechaSolucion = DateTime.Now;
                    ticket.Fechamodifica = DateTime.Now;
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
            ViewData["AreaSoporteId"] = new SelectList(_context.AreaSoporteTK, "Id", "Descripcion", ticket.AreaSoporteId);
            ViewData["EstadoTKId"] = new SelectList(_context.EstadosTKs, "Id", "Descripcion", ticket.EstadoTKId);
            ViewData["SedeId"] = new SelectList(_context.EmpresasTK, "Id", "Descripcion", ticket.SedeId);
            ViewData["UrgenciaId"] = new SelectList(_context.UrgenciasTK, "Id", "Descripcion", ticket.UrgenciaId);
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
                .Include(t => t.AreaSoporte)
                .Include(t => t.EstadosTK)
                .Include(t => t.SedeTK)
                .Include(t => t.Urgencia)
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
