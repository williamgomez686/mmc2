using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mmc.AccesoDatos.Repositorios.IRepositorio;
using mmc.Modelos;
using mmc.Modelos.IglesiaModels;
using mmc.Modelos.ViewModels;
using mmc.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace mmc.Areas.Admin.Controllers
{
    [Area("Iglesia")]
    [Authorize(Roles = DS.Role_Admin + "," + DS.Role_Iglesia + "," + DS.Role_RegionesIglesia)]
    //[Authorize(Roles = "Admin,SuperUser,User")]
    public class RegionesCEBController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly ILogger<RegionesCEBController> _logger = null;

        public RegionesCEBController(IUnidadTrabajo unidadTrabajo, ILogger<RegionesCEBController> logger)
        {
            _unidadTrabajo = unidadTrabajo;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            try
            {
                RegionesCEB oRegion = new RegionesCEB();
                if (id == null)
                {
                    // Esto es para Crear nuevo Registro
                    return View(oRegion);
                }
                // Esto es para Actualizar
                oRegion = _unidadTrabajo.RegionCEB.Obtener(id.GetValueOrDefault());
                if (oRegion == null)
                {
                    return NotFound();
                }
                return View(oRegion);
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error al crear una region!";

                _logger.LogError(ex, "Ocurrió un error al agregar un nueva Region. Usuario: {0}", User.FindFirstValue(ClaimTypes.Name));
                return RedirectToAction("Upsert");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(RegionesCEB oRegion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (oRegion.Id == 0)
                    {
                        var UserAp = User.Identity.Name;//obtiene el nombre del usuario de la session activa
                        oRegion.hihgUser = UserAp;
                        oRegion.Date = DateTime.Now;
                        _unidadTrabajo.RegionCEB.Agregar(oRegion);
                    }
                    else
                    {

                        _logger.LogError( "Ocurrió un error al agregar un nueva Region. Usuario: {0}", User.FindFirstValue(ClaimTypes.Name));
                        _unidadTrabajo.RegionCEB.Actualizar(oRegion);
                    }
                    _unidadTrabajo.Guardar();
                    return RedirectToAction(nameof(Index));
                }
                return View(oRegion);
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error al crear una region!";

                _logger.LogError(ex, "Ocurrió un error al agregar un nueva Region. Usuario: {0}", User.FindFirstValue(ClaimTypes.Name));
                return RedirectToAction("Upsert");
            }
        }

        // EN ESTA PARTE va la pantalla principal de llenado 
        #region RegionesyCEB
        public IActionResult Regiones(int id)
        {
            var region = _unidadTrabajo.RegionCEB.Obtener(id);
            ViewBag.region=region.RegionName;
            ViewBag.regionId = region.Id;

            var consulta = (from rc in _unidadTrabajo.RegionCEB.ObtenerTodos()
                                join m in _unidadTrabajo.MiembrosCEB.ObtenerTodos()
                                    on rc.Id equals m.RegionId
                                join p in _unidadTrabajo.PrivilegiosCEB.ObtenerTodos()
                                    on m.CargosCEBId equals p.Id
                                where rc.Id == id
                                orderby p.Cargos descending
                                select new {
                                    m.Id, rc.RegionName, m.Name, m.lastName, m.Addres, m.phone, p.Cargos
                                }     
                            ).ToList();
            var VMdata = new List<RegionFromVM>();
            foreach(var item in consulta)
            {
                RegionFromVM result = new RegionFromVM();
                result.id = item.Id;
                result.NombreRegion = item.RegionName;
                result.NombreServidor = item.Name;
                result.ApellidoServidor = item.lastName;
                result.Direccion = item.Addres;
                result.Phone = item.phone;
                result.Cargos = item.Cargos;
                VMdata.Add(result);
            }
            return View(VMdata.ToList());
        }
        #endregion

        #region API
        [HttpGet]
        public IActionResult ObtenerTodos2()
        {
            var todos = _unidadTrabajo.RegionCEB.ObtenerTodos().Where(est => est.Estado == true).OrderBy(test => test.Id);
            return Json(new { data = todos });
        }
        [HttpGet]
        public JsonResult ObtenerTodos()
        {
            if (User.IsInRole(DS.Role_RegionesIglesia))
            {
                string NombreUsuario= User.Identity.Name;
                int idregion = 0;
                switch (NombreUsuario)
                {
                    case "Region1": idregion = 8; break;
                    case "Region2": idregion = 3; break;
                    case "Region3": idregion = 4; break;
                    case "Region4": idregion = 5; break;
                    case "Region5": idregion = 6; break;
                    case "Region7": idregion = 7; break;
                    case "Region8": idregion = 9; break;
                    case "Region9":  idregion = 10; break;
                    case "Region10": idregion = 11; break;
                    case "Region11": idregion = 12; break;
                    case "Region12": idregion = 14; break;
                    case "Region13": idregion = 15; break;
                    case "Region14": idregion = 16; break;
                    case "Region16": idregion = 17; break;
                    case "Region19": idregion = 18; break;
                    case "Region21": idregion = 19; break;
                    case "Region22": idregion = 20; break;
                    case "Region23": idregion = 21; break;
                    case "Region24": idregion = 22; break;
                    case "Region25": idregion = 23; break;
                    case "Region26": idregion = 24; break;
                    case "Region27": idregion = 25; break;
                    case "Region29": idregion = 26; break;
                    case "Region30": idregion = 27; break;
                    case "Region32": idregion = 28; break;
                    case "SedeHotel": idregion = 29; break;
                }
                var todos2 = _unidadTrabajo.RegionCEB.ObtenerTodos().OrderBy(test => test.Id).Where(r => r.Id == idregion);
                return Json(new { data = todos2 });
            }
            var todos = _unidadTrabajo.RegionCEB.ObtenerTodos().OrderBy(test => test.Id);
            return Json(new { data = todos });
        }

        [HttpDelete]
        [Authorize(Roles = "Admin,Iglesia")]
        public IActionResult Delete(int id)
        {
            var RegioniDB = _unidadTrabajo.RegionCEB.Obtener(id);
            if (RegioniDB == null)
            {
                return Json(new { success = false, message = "Error al Borrar" });
            }
            _unidadTrabajo.RegionCEB.Remover(RegioniDB);
            _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Region Borrada Exitosamente" });
        }
        #endregion
    }
}
