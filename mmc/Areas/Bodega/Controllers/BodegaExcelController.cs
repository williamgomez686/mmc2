using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mmc.Utilidades;
using System.Data;

namespace mmc.Areas.Bodega.Controllers
{
    [Area("Bodega")]
    [Authorize(Roles = DS.Role_Admin + "," + DS.Role_Bodega)]
    public class BodegaExcelController : Controller
    {
        // GET: BodegaExcelController
        public ActionResult Index()
        {
            return View();
        }

        // GET: BodegaExcelController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BodegaExcelController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BodegaExcelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BodegaExcelController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BodegaExcelController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BodegaExcelController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BodegaExcelController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
