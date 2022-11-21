using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mmc.AccesoDatos.Repositorios.IRepositorio;
using mmc.Modelos;
using mmc.Modelos.IglesiaModels;
using mmc.Modelos.ViewModels;
using mmc.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mmc.Areas.Admin.Controllers
{
    [Area("Iglesia")]
    [Authorize(Roles = DS.Role_Admin + "," + DS.Role_Iglesia)]
    public class RegionesCEBController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public RegionesCEBController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(RegionesCEB oRegion)
        {
            if (ModelState.IsValid)
            {
                if (oRegion.Id == 0)
                {
                     var UserAp = User.Identity.Name;//obtiene el nombre del usuario de la session activa
                    oRegion.hihgUser=UserAp;
                    oRegion.Date = DateTime.Now;
                    _unidadTrabajo.RegionCEB.Agregar(oRegion);
                }
                else
                {
                    _unidadTrabajo.RegionCEB.Actualizar(oRegion);
                }
                _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            return View(oRegion);
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
        public IActionResult ObtenerTodos()
        {
            var todos = _unidadTrabajo.RegionCEB.ObtenerTodos().Where(est => est.Estado == true);
            return Json(new { data = todos });
        }

        [HttpDelete]
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
