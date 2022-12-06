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
            //var owner = context.MyContainer.Where(t => t.ID == '1');
            //owner.MyTable.Load();
            //var count = owner.MyTable.Count();

            //var casas = from cab in _contex.CEB_CABs.ToList()
            //            join tipdat in _contex.TiposCEB.ToList()
            //            on cab.TipoCebId equals tipdat.Id
            //            where cab.Estado == true
            //            select cab;
            //var counter = casas.Count();

            //var result = from ceb in _contex.CEB_CABs.ToList()
            //             where ceb.Estado == true
            //             group ceb by ceb.TipoCebId into totales
            //             select new
            //             {
            //                 cantidad = totales.Count()
            //             };
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

            ModelPastel serie = new ModelPastel();
            var result2 = serie.GetDataDummy();
            return Json(total);
        }
    }
}
