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
    [Authorize(Roles = DS.Role_Admin)]
    public class PrivilegioCEBController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public PrivilegioCEBController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            PrivilegioCEB oPrivilegio = new PrivilegioCEB();
            if (id == null)
            {
                // Esto es para Crear nuevo Registro
                return View(oPrivilegio);
            }
            // Esto es para Actualizar
            oPrivilegio = _unidadTrabajo.PrivilegiosCEB.Obtener(id.GetValueOrDefault());
            if (oPrivilegio == null)
            {
                return NotFound();
            }

            return View(oPrivilegio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(PrivilegioCEB oPrivilegio)
        {
            if (ModelState.IsValid)
            {
                if (oPrivilegio.Id == 0)
                {
                    _unidadTrabajo.PrivilegiosCEB.Agregar(oPrivilegio);
                }
                else
                {
                    _unidadTrabajo.PrivilegiosCEB.Actualizar(oPrivilegio);
                }
                _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            return View(oPrivilegio);
        }

        #region API
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var todos = _unidadTrabajo.PrivilegiosCEB.ObtenerTodos();
            return Json(new { data = todos });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var PrivilegioDB = _unidadTrabajo.PrivilegiosCEB.Obtener(id);
            if (PrivilegioDB == null)
            {
                return Json(new { success = false, message = "Error al Borrar" });
            }
            _unidadTrabajo.PrivilegiosCEB.Remover(PrivilegioDB);
            _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Privilegio Borrado Exitosamente" });
        }
        #endregion
    }
}
