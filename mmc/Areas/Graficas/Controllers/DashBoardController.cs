using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mmc.Utilidades;
using System.Data;
using mmc.Modelos.ViewModels;
using Microsoft.AspNetCore.Hosting;
using mmc.AccesoDatos.Repositorios.IRepositorio;
using mmc.AccesoDatos.Data;
using System.Runtime.InteropServices;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace mmc.Areas.Graficas.Controllers
{
    [Area("Graficas")]
    [Authorize(Roles = DS.Role_Admin + "," + DS.Role_Iglesia)]
    public class DashBoardController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly ApplicationDbContext _contex;
        public DashBoardController(IUnidadTrabajo unidadTrabajo, ApplicationDbContext contex)
        {
            _unidadTrabajo = unidadTrabajo;
            _contex = contex;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult DataPastel()
        {
            return Json(GetDataPie());
        }

        public JsonResult DataBarras()
        {
            var listBarras = (from m in _contex.Miembros.ToList()
                              join cc in _contex.CEB_CABs.ToList()
                                 on m.Id equals cc.MiembrosCEBid
                              join rc in _contex.RegionesCEB.ToList()
                                on m.RegionId equals rc.Id
                                where rc.Estado= true
                                orderby rc.RegionName
                              group rc by new {rc.RegionName} into resultado                       
                              select new
                             {
                                 region = resultado.Key.RegionName,
                                 cantidad = resultado.Count(),
                             }).ToList();

            var cantArray = listBarras.Count;

            object[] data = new object[cantArray];
            int contar = 0;
            foreach (var item in listBarras)
            {               
                data[contar] = new object[] { item.region, item.cantidad };
                contar++;
            }

            return Json(data);
        }
        public List<ModelPastel> GetDataPie()
        {
            var total = (from a in _contex.CEB_CABs.ToList()
                         join b in _contex.TiposCEB.ToList()
                             on a.TipoCebId equals b.Id
                         where a.Estado == true
                         group a by new { a.TipoCebId, b.Tipo } into resultados
                         select new
                         {
                             Cantidad = resultados.Count(),
                             Tipo = resultados.Key.Tipo
                         }).ToList();
    
            List<ModelPastel> lista = new();

            foreach (var item in total)
            {

                lista.Add(new ModelPastel(item.Tipo, item.Cantidad));
            }

            return lista;
        }
    }
}
