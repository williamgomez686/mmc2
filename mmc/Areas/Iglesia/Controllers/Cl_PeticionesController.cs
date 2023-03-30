using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using mmc.AccesoDatos.Data;
using mmc.Modelos.IglesiaModels;
using mmc.Modelos.IglesiaModels.OracleModels;
using mmc.Utilidades;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Threading.Tasks;

namespace mmc.Areas.Iglesia.Controllers
{
    [Area("Iglesia")]
    [Authorize(Roles = DS.Role_Admin + "," + DS.Role_Iglesia)]
    public class Cl_PeticionesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly string _CadenaOracle;
        public Cl_PeticionesController(ApplicationDbContext context, IConfiguration cadena)
        {
            _context = context;
            _CadenaOracle = cadena.GetConnectionString("OrecleStringExternaTest");
        }

        #region Metodos Controller
        public IActionResult VistaError()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var mes =DateTime.Now.Month;
            var anio = DateTime.Now.Year;

            var fini = "01/" +mes +"/"+ anio;
            
            //var fini = '01/ + substring(DtoC((fecha,3,10))'
            //var ffin = Fc(fecha)


            

             //select codigo, nombre from clpeticones  where nombre = "pepe" orderbay desc

            var ordenmetodo = await _context.Peticiones
                                        .OrderByDescending(f=>f.Fecha)
                                        .ToListAsync();

            var ordenquery = from query in await  _context.Peticiones.ToListAsync()
                             //where query.Nombres == "hola pepe"
                             orderby query.Fecha descending
                             select new
                             {
                                 codigo = query.Codigo,
                                 nombre = query.Nombres,
                                 peticion = query.Peticion,
                                 fecha = query.Fecha
                             };

            return View(ordenmetodo);
        }
        [HttpGet]
        public async Task<IActionResult> Detalles(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cl_Peticiones = await _context.Peticiones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cl_Peticiones == null)
            {
                return NotFound();
            }
            return View(cl_Peticiones);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Cl_Peticiones cl_Peticiones)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    cl_Peticiones.Fecha = DateTime.Now;
                    _context.Add(cl_Peticiones);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ViewBag.error = ex.Message;
                    return RedirectToAction(nameof(VistaError));
                    throw;
                }
            }
            return View(cl_Peticiones);
        }

        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cl_Peticiones = await _context.Peticiones.FindAsync(id);
            if (cl_Peticiones == null)
            {
                return NotFound();
            }
            return View(cl_Peticiones);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Cl_Peticiones cl_Peticiones)
        {
            if (id != cl_Peticiones.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cl_Peticiones);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Cl_PeticionesExists(cl_Peticiones.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cl_Peticiones);
        }
        //METODO QUE VERIFICA SI EL ID EXISTE EN LA BASE DE DATOS
        private bool Cl_PeticionesExists(int id)
        {
            return _context.Peticiones.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cl_Peticiones = await _context.Peticiones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cl_Peticiones == null)
            {
                return NotFound();
            }

            return View(cl_Peticiones);
        }

        // POST: Cl_Peticiones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cl_Peticiones = await _context.Peticiones.FindAsync(id);
            _context.Peticiones.Remove(cl_Peticiones);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region APIS
        [HttpPost]
        public JsonResult ConsultarClientes(string nombre)
        {
            try
            {
                string BuscaUpper = nombre.ToUpper().Replace(" ", "%");
                var resultado = BuscaPersona(BuscaUpper);

                return Json(resultado);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Logica Oracle
        public OracleConnection Connection()
        {
            try
            {
                OracleConnection db = new OracleConnection(_CadenaOracle);
                db.Open();
                return db;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<cxc_clientes> BuscaPersona(string name)
        {
            List<cxc_clientes> ListPersonas = new List<cxc_clientes>();

            string query = @"select CLICOD,CLIRAZSOC,CLITEL1,CLITEL2,CLISHDIRACT
                                from CXC_CLIENTES
                                where PAICOD = '00001'
                                and EMPCOD = '00001'
                                and CLIRAZSOC like '%" + name + "%' ORDER BY 2";
            using (OracleCommand cmd = new OracleCommand(query, Connection()))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                using (OracleDataReader resultado = cmd.ExecuteReader())
                {
                    while (resultado.Read())
                    {
                        cxc_clientes result = new cxc_clientes();
                        {
                            result.CliCod = Convert.ToString(resultado["CLICOD"]);
                            result.CliRazSoc = Convert.ToString(resultado["CLIRAZSOC"]);
                            result.CliTel1 = Convert.ToString(resultado["CLITEL1"]);
                            result.CliTel2 = Convert.ToString(resultado["CLITEL2"]);
                            result.CliShDirAct = Convert.ToString(resultado["CLISHDIRACT"]);
                        }
                        ListPersonas.Add(result);
                    }
                }
                return ListPersonas;
            }
        }
        #endregion
    }
}
