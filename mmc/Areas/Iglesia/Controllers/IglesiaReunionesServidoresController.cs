using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mmc.AccesoDatos.Data;
using mmc.Modelos.IglesiaModels.lafamiliadedios;
using mmc.Modelos.ViewModels.IglesiaVM;
using mmc.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mmc.Areas.Iglesia.Controllers
{
    [Area("Iglesia")]
    [Authorize(Roles = DS.Role_Admin + "," + DS.Role_Iglesia)]
    public class IglesiaReunionesServidoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IglesiaReunionesServidoresController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var reuniones = await _context.IglesiaReuniones.ToListAsync();
            ViewBag.ListaReuniones = reuniones;

            var query = from servidorReunion in _context.IglesiaServidoresReuniones
                        join reunion in _context.IglesiaReuniones on servidorReunion.ReunionId equals reunion.Id
                        group new { reunion, servidorReunion } by new { reunion.Id, reunion.NombreReunion, servidorReunion.Asiste } into g   
                        select new
                        {
                            NombreReunion = g.Key.NombreReunion,
                            Asistencia = g.Key.Asiste,
                            Cantidad = g.Count()
                        };

            var resultado = query.OrderBy(r=>r.NombreReunion).ToList();

            var modeloVista = resultado.Select(r => new ReporteAsistenciaViewModel
            {
                NombreReunion = r.NombreReunion,
                Asistencia = r.Asistencia,
                Cantidad = r.Cantidad
            }).ToList();

            return View(modeloVista);
        }

        [HttpGet]
        public async Task<IActionResult> RegistraServidores(int Pag, int Registros, string Search)
        {
            List<VMReuniones> datos = null;
            if (Search != null)//si los parametros de busqueda tiene datos se aplica el filtro
            {
                datos = await (from tsr in _context.IglesiaServidoresReuniones
                               join ts in _context.IglesiaServidores
                                   on tsr.ServidorId equals ts.Id
                               join lol in _context.IglesiaDepartamentos
                                   on ts.DepartamentoId equals lol.Id
                               join tr in _context.IglesiaReuniones
                                   on tsr.ReunionId equals tr.Id
                               where tsr.Asiste == false && ts.Nombres.Contains(Search)
                               select new VMReuniones
                               {
                                   ServidorId = tsr.ServidorId,
                                   Nombres = ts.Nombres,
                                   Acompañantes = tsr.Acompañantes,
                                   Asiste = tsr.Asiste,
                                   Departamento = lol.Descripcion,
                                   NombreReunion = tr.NombreReunion
                               }).ToListAsync();
            }
            else //si no hay parametros de busqueda muestra todos los datos de la base de datos
            {
                datos = await (from tsr in _context.IglesiaServidoresReuniones
                               join ts in _context.IglesiaServidores
                                   on tsr.ServidorId equals ts.Id
                               join lol in _context.IglesiaDepartamentos
                                   on ts.DepartamentoId equals lol.Id
                               join tr in _context.IglesiaReuniones
                                   on tsr.ReunionId equals tr.Id
                               where tsr.Asiste == false
                               select new VMReuniones
                               {
                                   ServidorId = tsr.ServidorId,
                                   Nombres = ts.Nombres,
                                   Acompañantes = tsr.Acompañantes,
                                   Asiste = tsr.Asiste,
                                   Departamento = lol.Descripcion,
                                   NombreReunion = tr.NombreReunion
                               }).ToListAsync();
            }

            string host = Request.Scheme + "://" + Request.Host.Value;
            object[] resultado = new Paginador<VMReuniones>()
                                .paginador(datos, Pag, Registros, "Iglesia", "IglesiaReunionesServidores", "RegistraServidores", host);

            DataPaginador<VMReuniones> modelo = new DataPaginador<VMReuniones>
            {
                Lista = (List<VMReuniones>)resultado[2],
                Pagi_info = (string)resultado[0],
                Pagi_navegacion = (string)resultado[1],
            };

            return View(modelo);
        }

        public IActionResult CrearEvento(int idActividad)
        {
            int resultado =2;
            //var actividad = 3;
            var servidores = _context.IglesiaServidores.ToList();

            foreach (var servidor in servidores)
            {
                var idServidor = servidor.Id;

                resultado = guardarreunion(idServidor, idActividad);
            }

            return Json(new { resultado });
        }

        private int guardarreunion(int idServidor, int idReunion)
        {
            try
            {
                IglesiaServidoresReunion oReunionServidores = new IglesiaServidoresReunion();
                oReunionServidores.ReunionId = idReunion;
                oReunionServidores.ServidorId = idServidor;
                oReunionServidores.Estado = false;
                oReunionServidores.Usuario = User.Identity.Name;
                oReunionServidores.FechaAlta = DateTime.Now;
                _context.IglesiaServidoresReuniones.Add(oReunionServidores);
                _context.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return 2;
            }
        }


        [HttpPost]
        public IActionResult ActualizarAsistencia(int empleadoId, bool asistencia, int carnet)
        {

            //var hola = carnet;
            var evento = _context.IglesiaServidoresReuniones.FirstOrDefault(evento => evento.ServidorId == empleadoId);
            evento.Asiste = asistencia;
            evento.Acompañantes = carnet;
            _context.IglesiaServidoresReuniones.Update(evento);
            _context.SaveChanges();
            return Ok();
        }

        public IActionResult Create()
        {
            ViewData["ReunionId"] = new SelectList(_context.IglesiaReuniones, "Id", "Id");
            ViewData["ServidorId"] = new SelectList(_context.IglesiaServidores, "Id", "Id");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IglesiaServidoresReunion model)
        {
            var res = 1;
            if (res == 1)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReunionId"] = new SelectList(_context.IglesiaReuniones, "Id", "Id", model.ReunionId);
            ViewData["ServidorId"] = new SelectList(_context.IglesiaServidores, "Id", "Id", model.ServidorId);
            return View(model);
        }


        #region APIS
        public async Task<IActionResult> ListaAsistieron()
        {
            List<VMReuniones> datos = null;

            datos = await (from tsr in _context.IglesiaServidoresReuniones
                           join ts in _context.IglesiaServidores
                               on tsr.ServidorId equals ts.Id
                           join lol in _context.IglesiaDepartamentos
                               on ts.DepartamentoId equals lol.Id
                           join tr in _context.IglesiaReuniones
                               on tsr.ReunionId equals tr.Id
                           where tsr.Asiste == true
                           orderby ts.Departamentos
                           select new VMReuniones
                           {
                               ServidorId = tsr.ServidorId,
                               Nombres = ts.Nombres,
                               Acompañantes = tsr.Acompañantes,
                               Asiste = tsr.Asiste,
                               Departamento = lol.Descripcion,
                               NombreReunion = tr.NombreReunion
                           }).ToListAsync();

            return StatusCode(StatusCodes.Status200OK, datos);
        }
        #endregion

    }
}
