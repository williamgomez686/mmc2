using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mmc.AccesoDatos.Data;
using mmc.AccesoDatos.Repositorios.IRepositorio;
using mmc.Modelos.IglesiaModels;
using mmc.Modelos.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace mmc.Areas.HelpDesk.Controllers
{
    [Area("HelpDesk")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, IUnidadTrabajo unidadTrabajo, ApplicationDbContext db)
        {
            _logger = logger;
            _unidadTrabajo = unidadTrabajo;
            _db = db;   
        }

        public IActionResult Index()
        {
            IEnumerable<MiembrosCEB> MiebrosList = _unidadTrabajo.MiembrosCEB.ObtenerTodos();//(incluirPropiedades: "Categoria,Marca");

            return View(MiebrosList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
