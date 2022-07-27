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
    public class CasaEstudioBiblicoController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        //esta variable nos servira para cargar archivos
        private readonly IWebHostEnvironment _hostEnvironment;

        public CasaEstudioBiblicoController(IUnidadTrabajo unidadTrabajo, IWebHostEnvironment hostEnvironment)
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
            CEB_VM CasasEstudioVM = new CEB_VM()
            {
                CasaEstudioBiblico = new CasaEstudioBiblico(),
                TipoCEBLista = _unidadTrabajo.TiposCEB.ObtenerTodos().Select(c => new SelectListItem
                {
                    Text = c.Tipo,
                    Value = c.Id.ToString()
                }),
                MiembroCEBLista = _unidadTrabajo.MiembrosCEB.ObtenerTodos().Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                }),
            };

            if (id == null)
            {
                // Esto es para Crear nuevo Registro
                return View(CasasEstudioVM);
            }
            // Esto es para Actualizar
            CasasEstudioVM.CasaEstudioBiblico = _unidadTrabajo.CasaEstudio.Obtener(id.GetValueOrDefault());
            if (CasasEstudioVM.CasaEstudioBiblico == null)
            {
                return NotFound();
            }

            return View(CasasEstudioVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CEB_VM CasaEstudioVM)
        {
            if (ModelState.IsValid)
            {
                // Cargar Imagenes
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string filename = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"imagenes\Iglesia\CEB\Imagenes");
                    var extension = Path.GetExtension(files[0].FileName);
                    if (CasaEstudioVM.CasaEstudioBiblico.ImagenUrl != null)
                    {
                        //Esto es para editar, necesitamos borrar la imagen anterior
                        var imagenPath = Path.Combine(webRootPath, CasaEstudioVM.CasaEstudioBiblico.ImagenUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagenPath))
                        {
                            System.IO.File.Delete(imagenPath);
                        }
                    }

                    using (var filesStreams = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    CasaEstudioVM.CasaEstudioBiblico.ImagenUrl = @"\imagenes\Iglesia\CEB\Imagenes\" + filename + extension;
                }
                else
                {
                    // Si en el Update el usuario no cambia la imagen
                    if (CasaEstudioVM.CasaEstudioBiblico.Id != 0)
                    {
                        CasaEstudioBiblico CasaEstudioDB = _unidadTrabajo.CasaEstudio.Obtener(CasaEstudioVM.CasaEstudioBiblico.Id);
                        CasaEstudioVM.CasaEstudioBiblico.ImagenUrl = CasaEstudioDB.ImagenUrl;
                    }
                }


                if (CasaEstudioVM.CasaEstudioBiblico.Id == 0)
                {                   
                    CasaEstudioVM.CasaEstudioBiblico.Estado = true;
                    CasaEstudioVM.CasaEstudioBiblico.total = CasaEstudioVM.CasaEstudioBiblico.TotalCristianos + CasaEstudioVM.CasaEstudioBiblico.NoCristianos + CasaEstudioVM.CasaEstudioBiblico.Ninos;
                    _unidadTrabajo.CasaEstudio.Agregar(CasaEstudioVM.CasaEstudioBiblico);
                }
                else
                {
                    _unidadTrabajo.CasaEstudio.Actualizar(CasaEstudioVM.CasaEstudioBiblico);
                }
                _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                CasaEstudioVM.TipoCEBLista = _unidadTrabajo.TiposCEB.ObtenerTodos().Select(c => new SelectListItem
                {
                    Text = c.Tipo,
                    Value = c.Id.ToString()
                });
                CasaEstudioVM.MiembroCEBLista = _unidadTrabajo.MiembrosCEB.ObtenerTodos().Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                });

                if (CasaEstudioVM.CasaEstudioBiblico.Id != 0)
                {
                    CasaEstudioVM.CasaEstudioBiblico = _unidadTrabajo.CasaEstudio.Obtener(CasaEstudioVM.CasaEstudioBiblico.Id);
                }

            }
            return View(CasaEstudioVM.CasaEstudioBiblico);
        }

        #region creacion de CEB por Lider
        public IActionResult UpsertCEBporLider(int? id)
        {
            CEB_VM CasasEstudioVM = new CEB_VM()
            {
                CasaEstudioBiblico = new CasaEstudioBiblico(),
                TipoCEBLista = _unidadTrabajo.TiposCEB.ObtenerTodos().Select(c => new SelectListItem
                {
                    Text = c.Tipo,
                    Value = c.Id.ToString()
                }),
                MiembroCEBLista = _unidadTrabajo.MiembrosCEB.ObtenerTodos().Where(m => m.Id == id).Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()                    
                }),
            };

            //if (id == null)
            //{
            //    // Esto es para Crear nuevo Registro
            //    return View(CasasEstudioVM);
            //}
            // Esto es para Actualizar
            //CasasEstudioVM.CasaEstudioBiblico = _unidadTrabajo.CasaEstudio.Obtener(id.GetValueOrDefault());
            //if (CasasEstudioVM.CasaEstudioBiblico == null)
            //{
            //    return NotFound();
            //}

            return View(CasasEstudioVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpsertCEBporLider(CEB_VM CasaEstudioVM)
        {
            if (ModelState.IsValid)
            {
                // Cargar Imagenes
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string filename = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"imagenes\Iglesia\CEB\Imagenes");
                    var extension = Path.GetExtension(files[0].FileName);
                    if (CasaEstudioVM.CasaEstudioBiblico.ImagenUrl != null)
                    {
                        //Esto es para editar, necesitamos borrar la imagen anterior
                        var imagenPath = Path.Combine(webRootPath, CasaEstudioVM.CasaEstudioBiblico.ImagenUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagenPath))
                        {
                            System.IO.File.Delete(imagenPath);
                        }
                    }

                    using (var filesStreams = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    CasaEstudioVM.CasaEstudioBiblico.ImagenUrl = @"\imagenes\Iglesia\CEB\Imagenes\" + filename + extension;
                }
                //else
                //{
                //    // Si en el Update el usuario no cambia la imagen
                //    if (CasaEstudioVM.CasaEstudioBiblico.Id != 0)
                //    {
                //        CasaEstudioBiblico CasaEstudioDB = _unidadTrabajo.CasaEstudio.Obtener(CasaEstudioVM.CasaEstudioBiblico.Id);
                //        CasaEstudioVM.CasaEstudioBiblico.ImagenUrl = CasaEstudioDB.ImagenUrl;
                //    }
                //}


                //if (CasaEstudioVM.CasaEstudioBiblico.Id == 0)
                //{
                    CasaEstudioVM.CasaEstudioBiblico.Estado = true;
                    CasaEstudioVM.CasaEstudioBiblico.total = CasaEstudioVM.CasaEstudioBiblico.TotalCristianos + CasaEstudioVM.CasaEstudioBiblico.NoCristianos + CasaEstudioVM.CasaEstudioBiblico.Ninos;
                    _unidadTrabajo.CasaEstudio.Agregar(CasaEstudioVM.CasaEstudioBiblico);
                //}
                //else
                //{
                //    _unidadTrabajo.CasaEstudio.Actualizar(CasaEstudioVM.CasaEstudioBiblico);
                //}
                _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                CasaEstudioVM.TipoCEBLista = _unidadTrabajo.TiposCEB.ObtenerTodos().Select(c => new SelectListItem
                {
                    Text = c.Tipo,
                    Value = c.Id.ToString()
                });
                CasaEstudioVM.MiembroCEBLista = _unidadTrabajo.MiembrosCEB.ObtenerTodos().Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                });

                if (CasaEstudioVM.CasaEstudioBiblico.Id != 0)
                {
                    CasaEstudioVM.CasaEstudioBiblico = _unidadTrabajo.CasaEstudio.Obtener(CasaEstudioVM.CasaEstudioBiblico.Id);
                }

            }
            return View(CasaEstudioVM.CasaEstudioBiblico);
        }

        #endregion

        #region API
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            //var todos = from m in _unidadTrabajo.MiembrosCEB.ObtenerTodos()
            //              join p in _unidadTrabajo.PrivilegiosCEB.ObtenerTodos()
            //                on m.CargosCEBId equals p.Id
            //              join r in _unidadTrabajo.RegionCEB.ObtenerTodos()
            //                on m.RegionId equals r.Id
            //                orderby r.RegionName descending
            //          select new
            //          {     
            //              m.Id,
            //              m.Name,
            //              m.lastName,
            //              m.Addres,
            //              m.phone,
            //              m.phone2,
            //              m.DPI,
            //              p.Cargos,
            //              r.RegionName
            //          };

            var casas = from ceb in _unidadTrabajo.CasaEstudio.ObtenerTodos()
                        join tipo in _unidadTrabajo.TiposCEB.ObtenerTodos()
                          on ceb.TipoCebId equals tipo.Id
                        join m in _unidadTrabajo.MiembrosCEB.ObtenerTodos()
                          on ceb.MiembrosCEBid equals m.Id
                        orderby ceb.Fecha ascending
                        select new
                        {
                            ceb.Id, ceb.Fecha, ceb.TotalCristianos, ceb.NoCristianos, ceb.Ninos, ceb.total, ceb.Convertidos, ceb.Reconciliados, ceb.Ofrenda,
                            tipo.Tipo, m.Name
                        };


            return Json(new { data = casas });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var Model = _unidadTrabajo.CasaEstudio.Obtener(id);
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

            _unidadTrabajo.CasaEstudio.Remover(Model);
            _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Casa de Estudio Biblico Borrada Exitosamente" });
        }

        #endregion
    }
}
