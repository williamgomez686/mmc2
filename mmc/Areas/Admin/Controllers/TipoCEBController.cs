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
    [Area("Admin")]
    [Authorize(Roles = DS.Role_Admin)]
    public class TipoCEBController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public TipoCEBController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            TiposCEB oTipoCEB = new TiposCEB();
            if (id == null)
            {
                // Esto es para Crear nuevo Registro
                return View(oTipoCEB);
            }
            // Esto es para Actualizar
            oTipoCEB = _unidadTrabajo.TiposCEB.Obtener(id.GetValueOrDefault());
            if (oTipoCEB == null)
            {
                return NotFound();
            }

            return View(oTipoCEB);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(TiposCEB oTipoCEB)
        {
            if (ModelState.IsValid)
            {
                if (oTipoCEB.Id == 0)
                {
                    _unidadTrabajo.TiposCEB.Agregar(oTipoCEB);
                }
                else
                {
                    _unidadTrabajo.TiposCEB.Actualizar(oTipoCEB);
                }
                _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            return View(oTipoCEB);
        }

        #region API
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var todos = _unidadTrabajo.TiposCEB.ObtenerTodos();
            return Json(new { data = todos });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var TiposDB = _unidadTrabajo.TiposCEB.Obtener(id);
            if (TiposDB == null)
            {
                return Json(new { success = false, message = "Error al Borrar" });
            }
            _unidadTrabajo.TiposCEB.Remover(TiposDB);
            _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Tipo Borrado Exitosamente" });
        }
        #endregion
    }
}
