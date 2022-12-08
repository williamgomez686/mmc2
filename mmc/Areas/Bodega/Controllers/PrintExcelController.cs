using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mmc.Utilidades;
using System.Data;

namespace mmc.Areas.Bodega.Controllers
{
    public class PrintExcelController : Controller
    {
        [Area("Bodega")]
        [Authorize(Roles = DS.Role_Admin + "," + DS.Role_Bodega)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
