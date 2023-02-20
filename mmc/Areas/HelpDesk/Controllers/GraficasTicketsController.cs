using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mmc.AccesoDatos.Data;
using mmc.AccesoDatos.Repositorios.IRepositorio;
using mmc.Modelos.ViewModels;
using mmc.Utilidades;
using System.Collections.Generic;
using System.Linq;

namespace mmc.Areas.HelpDesk.Controllers
{
    [Area("HelpDesk")]
    [Authorize(Roles = DS.Role_Admin + "," + DS.Role_Iglesia + "," + DS.Role_Bodega + "," + DS.Role_Canal27 + "," + DS.Role_Setegua + "," + DS.Role_Contabilidad)]
    public class GraficasTicketsController : Controller
    {

        private readonly ApplicationDbContext _contex;
        public GraficasTicketsController(ApplicationDbContext contex)
        {
            _contex = contex;
        }
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult DataPastelTK()
        {
            return Json(GetDataPie());
        }

        public JsonResult GetDataPieporEstadosTK()
        {
            return Json(GetDataPieporEstadosjson());
        }
        public List<ModelPastel> GetDataPie()
        {
            var total = (from a in _contex.Tickets.ToList()
                         join b in _contex.EmpresasTK.ToList()
                             on a.SedeId equals b.Id
                         where a.Estado == true
                         group a by new { a.SedeId, b.Descripcion } into resultados
                         select new
                         {
                             Cantidad = resultados.Count(),
                             Tipo = resultados.Key.Descripcion
                         }).ToList();

            List<ModelPastel> lista = new();

            foreach (var item in total)
            {

                lista.Add(new ModelPastel(item.Tipo, item.Cantidad));
            }

            return lista;
        }


        public List<ModelPastel> GetDataPieporEstadosjson()
        {
            var total = (from a in _contex.Tickets.ToList()
                         join b in _contex.EstadosTKs.ToList()
                             on a.EstadoTKId equals b.Id
                         where a.Estado == true
                         group a by new { a.EstadoTKId, b.Descripcion } into resultados
                         select new
                         {
                             Cantidad = resultados.Count(),
                             Tipo = resultados.Key.Descripcion
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
