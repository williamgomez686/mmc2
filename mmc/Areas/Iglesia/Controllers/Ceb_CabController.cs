using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mmc.AccesoDatos.Repositorios.IRepositorio;
using mmc.Modelos;
using mmc.Modelos.IglesiaModels;
using mmc.Modelos.ViewModels;
using mmc.Modelos.ViewModels.IglesiaVM;
using mmc.Utilidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace mmc.Areas.Iglesia.Controllers
{
    [Area("Iglesia")]
    //Roles = DS.Role_Admin + "," + DS.Role_Ticket)
    [Authorize(Roles = DS.Role_Admin + "," + DS.Role_Iglesia)]
    public class Ceb_CabController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        //esta variable nos servira para cargar archivos
        private readonly IWebHostEnvironment _hostEnvironment;

        public Ceb_CabController(IUnidadTrabajo unidadTrabajo, IWebHostEnvironment hostEnvironment)
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
            Ceb_CabVM CasasEstudioVM = new Ceb_CabVM()
            {
                CasaEstudioCab = new CEB_CAB(),
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
            CasasEstudioVM.CasaEstudioCab = _unidadTrabajo.CEB_CAB.Obtener(id.GetValueOrDefault());
            if (CasasEstudioVM.CasaEstudioCab == null)
            {
                return NotFound();
            }

            return View(CasasEstudioVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Ceb_CabVM CasaEstudioVM)
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
                    if (CasaEstudioVM.CasaEstudioCab.Foto != null)
                    {
                        //Esto es para editar, necesitamos borrar la imagen anterior
                        var imagenPath = Path.Combine(webRootPath, CasaEstudioVM.CasaEstudioCab.Foto.TrimStart('\\'));
                        if (System.IO.File.Exists(imagenPath))
                        {
                            System.IO.File.Delete(imagenPath);
                        }
                    }

                    using (var filesStreams = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    CasaEstudioVM.CasaEstudioCab.Foto = @"\imagenes\Iglesia\CEB\Imagenes\" + filename + extension;
                }
                else
                {
                    // Si en el Update el usuario no cambia la imagen
                    if (CasaEstudioVM.CasaEstudioCab.Id != 0)
                    {
                        CEB_CAB CasaEstudioDB = _unidadTrabajo.CEB_CAB.Obtener(CasaEstudioVM.CasaEstudioCab.Id);
                        CasaEstudioVM.CasaEstudioCab.Foto = CasaEstudioDB.Foto;
                    }
                }


                if (CasaEstudioVM.CasaEstudioCab.Id == 0)
                {
                    CasaEstudioVM.CasaEstudioCab.FechaAlta = DateTime.Now;
                    CasaEstudioVM.CasaEstudioCab.Usuario = User.Identity.Name;//obtiene el nombre del usuario de la session activa
                    CasaEstudioVM.CasaEstudioCab.Estado = true;
                    _unidadTrabajo.CEB_CAB.Agregar(CasaEstudioVM.CasaEstudioCab);
                }
                else
                {
                    CasaEstudioVM.CasaEstudioCab.FechaAlta = DateTime.Now;
                    CasaEstudioVM.CasaEstudioCab.Usuario = User.Identity.Name;
                    _unidadTrabajo.CEB_CAB.Actualizar(CasaEstudioVM.CasaEstudioCab);
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

                if (CasaEstudioVM.CasaEstudioCab.Id != 0)
                {
                    CasaEstudioVM.CasaEstudioCab = _unidadTrabajo.CEB_CAB.Obtener(CasaEstudioVM.CasaEstudioCab.Id);
                }

            }
            return View(CasaEstudioVM.CasaEstudioCab);
        }

        #region Creacion de Detalle**********************************************************************
        public IActionResult Upsert2(int? id)
        {
            Ceb_DetVM CasasEstudioVM = new Ceb_DetVM()
            {
                CasaEstudioDet = new CEB_DET(),
                Cabeceraid = _unidadTrabajo.CEB_CAB.ObtenerTodos().Select(c => new SelectListItem
                {
                    Text = c.MiembrosCEBid.ToString(),
                    Value = c.Id.ToString()
                }),
            };

            //ViewData["CEBid"] = new SelectList(_context.CEB_CABs, "Id", "Id");
            ViewData["CEBid"] = new SelectList(_unidadTrabajo.CEB_CAB.ObtenerTodos());


            if (id == null)
            {
                // Esto es para Crear nuevo Registro
                return View(CasasEstudioVM);
            }
            // Esto es para Actualizar
            CasasEstudioVM.CasaEstudioDet = _unidadTrabajo.CEB_DET.Obtener(id.GetValueOrDefault());
            if (CasasEstudioVM.CasaEstudioDet == null)
            {
                return NotFound();
            }

            return View(CasasEstudioVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert2(Ceb_DetVM CasaEstudioVM)
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
                    if (CasaEstudioVM.CasaEstudioDet.Foto != null)
                    {
                        //Esto es para editar, necesitamos borrar la imagen anterior
                        var imagenPath = Path.Combine(webRootPath, CasaEstudioVM.CasaEstudioDet.Foto.TrimStart('\\'));
                        if (System.IO.File.Exists(imagenPath))
                        {
                            System.IO.File.Delete(imagenPath);
                        }
                    }

                    using (var filesStreams = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    CasaEstudioVM.CasaEstudioDet.Foto = @"\imagenes\Iglesia\CEB\Imagenes\" + filename + extension;
                }
                else
                {
                    // Si en el Update el usuario no cambia la imagen
                    if (CasaEstudioVM.CasaEstudioDet.Id != 0)
                    {
                        CEB_CAB CasaEstudioDB = _unidadTrabajo.CEB_CAB.Obtener(CasaEstudioVM.CasaEstudioDet.Id);
                        CasaEstudioVM.CasaEstudioDet.Foto = CasaEstudioDB.Foto;
                    }
                }


                if (CasaEstudioVM.CasaEstudioDet.Id == 0)
                {
                    CasaEstudioVM.CasaEstudioDet.FechaAlta = DateTime.Now;
                    CasaEstudioVM.CasaEstudioDet.Usuario = User.Identity.Name;//obtiene el nombre del usuario de la session activa
                    CasaEstudioVM.CasaEstudioDet.Estado = true;
                    _unidadTrabajo.CEB_DET.Agregar(CasaEstudioVM.CasaEstudioDet);
                }
                else
                {
                    CasaEstudioVM.CasaEstudioDet.Fechamodifica = DateTime.Now;
                    CasaEstudioVM.CasaEstudioDet.Usuario = User.Identity.Name;
                    _unidadTrabajo.CEB_DET.Actualizar(CasaEstudioVM.CasaEstudioDet);
                }
                _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                CasaEstudioVM.Cabeceraid = _unidadTrabajo.CEB_CAB.ObtenerTodos().Select(c => new SelectListItem
                {
                    Text = c.MiembroLider.ToString(),
                    Value = c.MiembrosCEBid.ToString()
                });


                if (CasaEstudioVM.CasaEstudioDet.Id != 0)
                {
                    CasaEstudioVM.CasaEstudioDet = _unidadTrabajo.CEB_DET.Obtener(CasaEstudioVM.CasaEstudioDet.Id);
                }

            }
            return View(CasaEstudioVM.CasaEstudioDet);
        }
        #endregion



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
            ViewBag.idporlider = id;
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

                    CasaEstudioVM.CasaEstudioBiblico.Estado = true;
                    CasaEstudioVM.CasaEstudioBiblico.total = CasaEstudioVM.CasaEstudioBiblico.TotalCristianos + CasaEstudioVM.CasaEstudioBiblico.NoCristianos + CasaEstudioVM.CasaEstudioBiblico.Ninos;
                    _unidadTrabajo.CasaEstudio.Agregar(CasaEstudioVM.CasaEstudioBiblico);

                _unidadTrabajo.Guardar();

                return RedirectToAction( "Index", "MiembrosCEB");
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
            var Model = _unidadTrabajo.CEB_CAB.Obtener(id);
            if (Model == null)
            {
                return Json(new { success = false, message = "Error al Borrar" });
            }
            // Eliminar la Imagen relacionada al producto
            string webRootPath = _hostEnvironment.WebRootPath;
            var imagenPath = Path.Combine(webRootPath, Model.Foto.TrimStart('\\'));
            if (System.IO.File.Exists(imagenPath))
            {
                System.IO.File.Delete(imagenPath);
            }

            _unidadTrabajo.CEB_CAB.Remover(Model);
            _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Casa de Estudio Biblico Borrada Exitosamente" });
        }

        #endregion
    }
}
