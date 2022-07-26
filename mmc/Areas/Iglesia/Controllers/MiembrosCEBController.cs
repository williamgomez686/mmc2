using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using mmc.AccesoDatos.Repositorios.IRepositorio;
using mmc.Modelos;
using mmc.Modelos.IglesiaModels;
using mmc.Modelos.ViewModels;
using mmc.Utilidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace mmc.Areas.Iglesia.Controllers
{
    [Area("Iglesia")]
    [Authorize(Roles = DS.Role_Admin)]
    public class MiembrosCEBController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        //esta variable nos servira para cargar archivos
        private readonly IWebHostEnvironment _hostEnvironment;

        public MiembrosCEBController(IUnidadTrabajo unidadTrabajo, IWebHostEnvironment hostEnvironment)
        {
            _unidadTrabajo = unidadTrabajo;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            MiembroVM MiembrosVM = new MiembroVM()
            {
                Miembros = new MiembrosCEB(),
                RegionCEBLista = _unidadTrabajo.RegionCEB.ObtenerTodos().Select(c => new SelectListItem
                {
                    Text = c.RegionName,
                    Value = c.Id.ToString()
                }),
                PrivilegioCEBLista = _unidadTrabajo.PrivilegiosCEB.ObtenerTodos().Select(m => new SelectListItem
                {
                    Text = m.Cargos,
                    Value = m.Id.ToString()
                }),
                //PadreLista = _unidadTrabajo.Producto.ObtenerTodos().Select(p => new SelectListItem
                //{
                //    Text = p.Descripcion,
                //    Value = p.Id.ToString()
                //})

            };


            if (id == null)
            {
                // Esto es para Crear nuevo Registro
                return View(MiembrosVM);
            }
            // Esto es para Actualizar
            MiembrosVM.Miembros = _unidadTrabajo.MiembrosCEB.Obtener(id.GetValueOrDefault());
            if (MiembrosVM.Miembros == null)
            {
                return NotFound();
            }

            return View(MiembrosVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(MiembroVM miembroVM)
        {
            if (ModelState.IsValid)
            {

                // Cargar Imagenes
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string filename = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"imagenes\Iglesia\Miembros\Imagenes");
                    var extension = Path.GetExtension(files[0].FileName);
                    if (miembroVM.Miembros.ImagenUrl != null)
                    {
                        //Esto es para editar, necesitamos borrar la imagen anterior
                        var imagenPath = Path.Combine(webRootPath, miembroVM.Miembros.ImagenUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagenPath))
                        {
                            System.IO.File.Delete(imagenPath);
                        }
                    }

                    using (var filesStreams = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    miembroVM.Miembros.ImagenUrl = @"\imagenes\Iglesia\Miembros\Imagenes\" + filename + extension;
                }
                else
                {
                    // Si en el Update el usuario no cambia la imagen
                    if (miembroVM.Miembros.Id != 0)
                    {
                        MiembrosCEB productoDB = _unidadTrabajo.MiembrosCEB.Obtener(miembroVM.Miembros.Id);
                        miembroVM.Miembros.ImagenUrl = productoDB.ImagenUrl;
                    }
                }


                if (miembroVM.Miembros.Id == 0)
                {                   
                    miembroVM.Miembros.Estado = true;
                    _unidadTrabajo.MiembrosCEB.Agregar(miembroVM.Miembros);
                }
                else
                {
                    _unidadTrabajo.MiembrosCEB.Actualizar(miembroVM.Miembros);
                }
                _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                miembroVM.RegionCEBLista = _unidadTrabajo.RegionCEB.ObtenerTodos().Select(c => new SelectListItem
                {
                    Text = c.RegionName,
                    Value = c.Id.ToString()
                });
                miembroVM.PrivilegioCEBLista = _unidadTrabajo.PrivilegiosCEB.ObtenerTodos().Select(m => new SelectListItem
                {
                    Text = m.Cargos,
                    Value = m.Id.ToString()
                });
                //miembroVM.PadreLista = _unidadTrabajo.Producto.ObtenerTodos().Select(p => new SelectListItem
                //{
                //    Text = p.Descripcion,
                //    Value = p.Id.ToString()
                //});

                if (miembroVM.Miembros.Id != 0)
                {
                    miembroVM.Miembros = _unidadTrabajo.MiembrosCEB.Obtener(miembroVM.Miembros.Id);
                }

            }
            return View(miembroVM.Miembros);
        }

        #region API
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var todos = from m in _unidadTrabajo.MiembrosCEB.ObtenerTodos()
                          join p in _unidadTrabajo.PrivilegiosCEB.ObtenerTodos()
                            on m.CargosCEBId equals p.Id
                          join r in _unidadTrabajo.RegionCEB.ObtenerTodos()
                            on m.RegionId equals r.Id
                            orderby r.RegionName descending
                      select new
                      {     
                          m.Id,
                          m.Name,
                          m.lastName,
                          m.Addres,
                          m.phone,
                          m.phone2,
                          m.DPI,
                          p.Cargos,
                          r.RegionName
                      };

            return Json(new { data = todos });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var Model = _unidadTrabajo.MiembrosCEB.Obtener(id);
            if (Model == null)
            {
                return Json(new { success = false, message = "Error al Borrar" });
            }
            // Eliminar la Imagen relacionada al producto
            string webRootPath = _hostEnvironment.WebRootPath;
            var imagenPath = Path.Combine(webRootPath, Model.ImagenUrl.TrimStart('\\'));
            if (System.IO.File.Exists(imagenPath))
            {
                System.IO.File.Delete(imagenPath);
            }

            _unidadTrabajo.MiembrosCEB.Remover(Model);
            _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Producto Borrado Exitosamente" });
        }

        #endregion
    }
}
