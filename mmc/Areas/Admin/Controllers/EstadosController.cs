using Microsoft.AspNetCore.Mvc;
using mmc.AccesoDatos.Repositorios.IRepositorio;
using mmc.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mmc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EstadosController : Controller
    {

        private readonly IUnidadTrabajo _unidadTrabajo;

        public EstadosController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            estado Oestado = new estado();
            if (id == null)
            {
                //esto es para crear un nuevo registro
                return View(Oestado);
            }
            // Esto es para Actualizar
            Oestado = _unidadTrabajo.Estado.Obtener(id.GetValueOrDefault());
            if (Oestado == null)
            {
                return NotFound();
            }

            return View(Oestado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(estado oEstado)
        {
            // estado Oestado = new estado();
            if (ModelState.IsValid)
            {
                if (oEstado.estadoId == 0)
                {
                    _unidadTrabajo.Estado.Agregar(oEstado);
                }
                else
                {
                    //se trata de una actualizacion
                    _unidadTrabajo.Estado.Actualizar(oEstado);
                }
                _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }

            return View(oEstado);
        }



        #region Api

        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var todos = _unidadTrabajo.Estado.ObtenerTodos();

            return Json(new { data = todos});
            //return View(todos);
        }

        #endregion
    }
}
