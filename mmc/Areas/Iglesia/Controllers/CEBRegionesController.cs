﻿using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mmc.AccesoDatos.Data;
using mmc.AccesoDatos.Repositorios.IRepositorio;
using mmc.Modelos.IglesiaModels;
using mmc.Modelos.ViewModels.IglesiaVM;
using mmc.Utilidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace mmc.Areas.Iglesia.Controllers
{
    [Area("Iglesia")]
    [Authorize(Roles = DS.Role_Admin + "," + DS.Role_Iglesia)]
    public class CEBRegionesController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ApplicationDbContext _context;

        public CEBRegionesController(IUnidadTrabajo unidadTrabajo, IWebHostEnvironment hostEnvironment, ApplicationDbContext context)
        {
            _unidadTrabajo = unidadTrabajo;
            _hostEnvironment = hostEnvironment;
            _context = context;
        }
        //funciona
        public IActionResult Index(int id)
        {
            try
            {
                var region = _unidadTrabajo.RegionCEB.Obtener(id);
                ViewBag.region = region.RegionName;
                ViewBag.regionId = region.Id;

                var casas = (from m in _unidadTrabajo.MiembrosCEB.ObtenerTodos()
                             join cc in _unidadTrabajo.CEB_CAB.ObtenerTodos()
                               on m.Id equals cc.MiembrosCEBid
                             join tc in _unidadTrabajo.TiposCEB.ObtenerTodos()
                               on cc.TipoCebId equals tc.Id
                             join p in _unidadTrabajo.PrivilegiosCEB.ObtenerTodos()
                               on m.CargosCEBId equals p.Id
                             join rc in _unidadTrabajo.RegionCEB.ObtenerTodos()
                                on m.RegionId equals rc.Id
                             where rc.Id == id
                             orderby p.Cargos descending
                             select new CebLiderPrivilegioVM()
                             {
                                 id = m.Id,
                                 Nombre = m.Name,
                                 Apellido = m.lastName,
                                 Direccion = m.Addres,
                                 Telefono = m.phone,
                                 Hora = cc.Hora,
                                 Dia = cc.dia,
                                 Tipo = tc.Tipo,
                                 Cargo = p.Cargos,
                                 cebid = cc.Id
                             }).ToList();
                return View(casas);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        public IActionResult LiderdeCEB(string id)
        {
            //Consulta con Joins para obtener datos de la cabecera y el lider
            var result = from m in _unidadTrabajo.MiembrosCEB.ObtenerTodos()
                             join cc in _unidadTrabajo.CEB_CAB.ObtenerTodos()
                                 on m.Id equals cc.MiembrosCEBid
                             join tc in _unidadTrabajo.TiposCEB.ObtenerTodos()
                                 on cc.TipoCebId equals tc.Id
                             join p in _unidadTrabajo.PrivilegiosCEB.ObtenerTodos()
                                 on m.CargosCEBId equals p.Id
                         where cc.Id == id
                         orderby p.Cargos descending
                         select new CebLiderPrivilegioVM()
                         {
                             id = m.Id,
                             Nombre = m.Name,
                             Apellido = m.lastName,
                             Direccion = m.Addres,
                             Telefono = m.phone,
                             Hora = cc.Hora,
                             Dia = cc.dia,
                             Tipo = tc.Tipo,
                             Cargo = p.Cargos,
                             cebid = cc.Id
                         };
            //ForEach que se encarga de llenar los ViewBag para la cabecera y datos del lider 
            foreach (var lider in result)
            {
                //Datos del lider
                var nombre = _unidadTrabajo.MiembrosCEB.Obtener(lider.id);
                ViewBag.lider = nombre.Name;
                ViewBag.apaellido = nombre.lastName;
                ViewBag.imagen = nombre.ImagenUrl;
                //Datos para la cabecera
                CebLiderPrivilegioVM cabecera = new CebLiderPrivilegioVM()
                {
                    Hora = lider.Hora,
                    Dia = lider.Dia,
                    Cargo = lider.Cargo,
                    cebid = lider.cebid,
                    Tipo = lider.Tipo

                };
                //Llenado de los ViewBag con datos de la cabecera
                ViewBag.cabHora = cabecera.Hora;
                ViewBag.cabDia = cabecera.Dia;
                ViewBag.cabTipo = cabecera.Tipo;
                ViewBag.cabCargo = cabecera.Cargo;
            }
            ViewBag.id = id;
            // query para optener el detalle de ceb
            var detalle = from cabecera in _unidadTrabajo.CEB_CAB.ObtenerTodos()
                              join det in _unidadTrabajo.CEB_DET.ObtenerTodos()
                                  on cabecera.Id equals det.CEBid
                          where cabecera.Id == id
                          orderby det.FechaCEB descending
                          select det;

            ViewBag.totalDetalle = detalle.Count();
            //query tipo metodo para optener la suma toal de lo ofrendado en esta casa
            ViewBag.totalOfrenda = _unidadTrabajo.CEB_DET.ObtenerTodos().Where(det => det.CEBid == id).Sum(sum => sum.Ofrenda);

            //Datos del Lider
            return View(detalle.ToList());
        }

        public async Task<IActionResult> EditarCabCeb (string? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                var oModel = await _context.CEB_CABs.FindAsync(id);
                if(oModel==null)
                {
                    return NotFound();
                }
                @ViewBag.id = id;
                ViewData["MiembrosCEBid"] = new SelectList(_context.Miembros, "Id", "Name", oModel.MiembrosCEBid);
                ViewData["TipoCebId"] = new SelectList(_context.TiposCEB, "Id", "Tipo", oModel.TipoCebId);
                return View(oModel);
            }
            catch (Exception)
            {
                return NotFound(); 
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarCabCeb(string id, CEB_CAB Model)
        {
            if(id != Model.Id)
            {
                return NotFound();
            }

            try
            {
                if(ModelState.IsValid)
                {
                    Model.Fechamodifica = DateTime.Now;
                    Model.UsuarioModifica = User.Identity.Name;
                    _context.Update(Model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "RegionesCEB");
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
            return View(Model);
        }

        public IActionResult AddDet(string? id)
        {
            Ceb_DetVM model = new Ceb_DetVM()
            {
                CasaEstudioDet = new CEB_DET(),
                //**********************************************Con este where hacemos la busqueda por id para que nos devuelva el que se le envia
                Cabeceraid = _unidadTrabajo.CEB_CAB.ObtenerTodos().Where(cab => cab.Id == id).Select(c => new SelectListItem
                {
                    Text = c.Id.ToString(),
                    Value = c.Id.ToString()
                })
            };
            ViewBag.id = id;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddDet(Ceb_DetVM model)
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
                    if (model.CasaEstudioDet.Foto != null)
                    {
                        //Esto es para editar, necesitamos borrar la imagen anterior
                        var imagenPath = Path.Combine(webRootPath, model.CasaEstudioDet.Foto.TrimStart('\\'));
                        if (System.IO.File.Exists(imagenPath))
                        {
                            System.IO.File.Delete(imagenPath);
                        }
                    }

                    using (var filesStreams = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    model.CasaEstudioDet.Foto = @"\imagenes\Iglesia\CEB\Imagenes\" + filename + extension;
                }
                ///Proceso de Guardado 
                model.CasaEstudioDet.FechaAlta = DateTime.Now;
                model.CasaEstudioDet.Usuario = User.Identity.Name;
                model.CasaEstudioDet.Estado = true;
                model.CasaEstudioDet.Total = model.CasaEstudioDet.Cristianos + model.CasaEstudioDet.NoCistianos + model.CasaEstudioDet.Ninios;
                model.CasaEstudioDet.Id = Guid.NewGuid().ToString();
                
                _unidadTrabajo.CEB_DET.Agregar(model.CasaEstudioDet);

                _unidadTrabajo.Guardar();

                return RedirectToAction("Index", "RegionesCEB");////POSIBLE FORMA DE RETORNAR A UNA VISTA EN ESPESIFICO 
                //return RedirectToAction("LiderdeCEB", "CEBRegiones", model.CasaEstudioDet.CEBid);
                //return View(model.CasaEstudioDet);

            }
            else
            {
                model.Cabeceraid = _unidadTrabajo.CEB_CAB.ObtenerTodos().Select(c => new SelectListItem
                {
                    Text = c.Id.ToString(),
                    Value = c.Id.ToString()
                });

                if (model.CasaEstudioDet.Id != null)
                {
                    //model.CasaEstudioDet = _unidadTrabajo.CEB_DET.Obtener(model.CasaEstudioDet.Id);
                }

            }
            return View(model.CasaEstudioDet);
        }

        [HttpGet]
        public IActionResult addCab_byLider(int? id)
        {
            ViewData["MiembrosCEBid"] = new SelectList(_unidadTrabajo.MiembrosCEB.ObtenerTodos().Where(region => region.RegionId == id), "Id", "Name");
            ViewData["TipoCebId"] = new SelectList(_unidadTrabajo.TiposCEB.ObtenerTodos(), "Id", "Tipo");
            return View();
        }

        [HttpPost]
        public IActionResult addCab_byLider(CEB_CAB model)
        {
            if (ModelState.IsValid)
            {
                model.Usuario = User.Identity.Name;
                model.FechaAlta = DateTime.Now;
                model.Estado = true;
                model.Id = Guid.NewGuid().ToString();
                //model.Id = model.Id + 1;
                _unidadTrabajo.CEB_CAB.Agregar(model);
                _unidadTrabajo.Guardar();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "RegionesCEB");
            }
            //ViewData["MiembrosCEBid"] = new SelectList(_unidadTrabajo.MiembrosCEB.ObtenerTodos().Where(region => region.RegionId == model.), "Id", "Name");
            ViewData["TipoCebId"] = new SelectList(_unidadTrabajo.TiposCEB.ObtenerTodos(), "Id", "Tipo", model.TipoCebId);

            return View(model);
        }


        #region Api
        [HttpDelete]
        public IActionResult Delete(string id)
        {
            try
            {
                var CEB_DET_DB = _unidadTrabajo.CEB_DET.ObtenerPorIdString(id);
                if (CEB_DET_DB == null)
                {
                    return Json(new { success = false, message = "Error al Borrar" });
                }
                _unidadTrabajo.CEB_DET.Remover(CEB_DET_DB);
                _unidadTrabajo.Guardar();
                return Json(new { success = true, message = "Registro Borrado Exitosamente" });
            }
            catch (Exception error)
            {

                return Json(new { success = false, message = "Error al Borrar: " + error.Message.ToString() });
            }
        }
        [HttpDelete]
        public IActionResult DeleteCab(string id)
        {
            try
            {
                var CEB_DET_DB = _unidadTrabajo.CEB_CAB.ObtenerPorIdString(id);
                if (CEB_DET_DB == null)
                {
                    return Json(new { success = false, message = "Error al Borrar" });
                }
                _unidadTrabajo.CEB_CAB.Remover(CEB_DET_DB);
                _unidadTrabajo.Guardar();
                return Json(new { success = true, message = "Registro Borrado Exitosamente" });
            }
            catch (Exception error)
            {

                return Json(new { success = false, message = "Ocurrió un error es posible que contenga registros. Revise que no tenga detalles esta Casa. Pruebe eliminarlo y vuelva a intentar: " + error.Message.ToString() });
            }
        }
        #endregion
    }
}
