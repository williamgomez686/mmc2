using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mmc.AccesoDatos.Repositorios.IRepositorio;
using mmc.Modelos;
using mmc.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mmc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DS.Role_Admin)]
    public class MarcaController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public MarcaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Marca oMarca = new Marca();
            if (id == null)
            {
                // Esto es para Crear nuevo Registro
                return View(oMarca);
            }
            // Esto es para Actualizar
            oMarca = _unidadTrabajo.Marca.Obtener(id.GetValueOrDefault());
            if (oMarca == null)
            {
                return NotFound();
            }

            return View(oMarca);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Marca oMarca)
        {
            if (ModelState.IsValid)
            {
                if (oMarca.Id == 0)
                {
                    _unidadTrabajo.Marca.Agregar(oMarca);
                }
                else
                {
                    _unidadTrabajo.Marca.Actualizar(oMarca);
                }
                _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            return View(oMarca);
        }

        #region API
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var todos = _unidadTrabajo.Marca.ObtenerTodos();
            return Json(new { data = todos });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var MarcaDB = _unidadTrabajo.Marca.Obtener(id);
            if (MarcaDB == null)
            {
                return Json(new { success = false, message = "Error al Borrar" });
            }
            _unidadTrabajo.Marca.Remover(MarcaDB);
            _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Marca Borrada Exitosamente" });
        }

        #endregion
    }
}
