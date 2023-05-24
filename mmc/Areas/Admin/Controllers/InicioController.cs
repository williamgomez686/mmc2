using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mmc.Utilidades;

namespace mmc.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = DS.Role_Admin)]
    public class InicioController : Controller
    {
        // GET: InicioController
        public ActionResult Index()
        {
            return View();
        }      
    }
}
