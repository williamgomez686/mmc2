using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mmc.AccesoDatos.Repositorios.IRepositorio;
using mmc.Modelos;
using mmc.Modelos.IglesiaModels;
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

        #region API
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var todos = _unidadTrabajo.RegionCEB.ObtenerTodos();
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
