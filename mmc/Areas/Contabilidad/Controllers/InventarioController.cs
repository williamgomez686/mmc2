using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting.Internal;
using mmc.Modelos.BodegaModels;
using mmc.Utilidades;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace mmc.Areas.Contabilidad.Controllers
{
    [Area("Contabilidad")]
    [Authorize(Roles = DS.Role_Admin + "," + DS.Role_Contabilidad)]
    public class InventarioController : Controller
    {
        private readonly string _cadena;
        private readonly string _cadenaTest;
        private IHostingEnvironment _hostingEnvironment;

        public InventarioController(IConfiguration cadena, IHostingEnvironment hostingEnvironment)
        {
            _cadena = cadena.GetConnectionString("OrecleString");
            _cadenaTest = cadena.GetConnectionString("OrecleStringExternaTest");
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult InventarioFisico(string TIECOD, string EMPCOD, string INVFISFCH, string HORA)
        {
            //DateTime fecha = DateTime.ParseExact(INVFISFCH, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime fecha = DateTime.ParseExact(INVFISFCH, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var slash = "/";
            var dia = fecha.Day;
            var mes = fecha.Month;
            var anio = fecha.Year;
            var invFecha = dia + slash + mes + slash + anio;

            //TimeSpan hora = TimeSpan.Parse(HORA);

            var consulta = @"SELECT a.ARTCOD codigo, b.ARTDES descripcion,a.INVFISEXISIS Existensia_Sitema,a.INVFISEXI Existencia_fisico
                                FROM INV_INVENTARIO_FISICO_DET a
                                JOIN INV_ARTICULO b ON a.PAICOD = b.PAICOD AND a.EMPCOD = b.EMPCOD AND a.ARTCOD = b.ARTCOD
                                WHERE a.EMPCOD = '" + EMPCOD + "'"+
                                "AND a.TIECOD = '" + TIECOD + "'" +
                                "AND a.INVFISFCH = '" + invFecha + "'" +
                                "AND a.INVFISHOR = '" + HORA + "'" +
                                "AND EXISTS (SELECT 1 FROM INV_MOVIMIENTO_INVENTARIO_DET id JOIN INV_MOVIMIENTO_INVENTARIO_ENC ie ON id.PAICOD = ie.PAICOD  AND id.EMPCOD = ie.EMPCOD AND id.TIPMOVINVCOD = ie.TIPMOVINVCOD AND id.MOVINVSERDOC = ie.MOVINVSERDOC AND id.MOVINVNUMDOC = ie.MOVINVNUMDOC AND id.TIECOD = ie.TIECOD WHERE ie.PAICOD = '00001' AND ie.EMPCOD = '" + EMPCOD + "' AND ie.TieCod = a.TIECOD AND id.ArtCod = a.ARTCOD AND id.CodSubPro1 = '000' AND id.CodSubPro2 = '000' AND ie.MovInvSta <> 'A')";

            DataTable tabla = new();

            using (var conexion = new OracleConnection(_cadena))
            {
                conexion.Open();
                using (var adapter = new OracleDataAdapter())
                {
                    adapter.SelectCommand = new OracleCommand(consulta, conexion);
                    adapter.SelectCommand.ExecuteNonQuery();
                    adapter.Fill(tabla);
                }
            }

            using (var libro = new XLWorkbook())
            {
                tabla.TableName = "test";
                var hoja = libro.Worksheets.Add(tabla);
                hoja.ColumnsUsed().AdjustToContents();

                using (var memoria = new MemoryStream())
                {
                    libro.SaveAs(memoria);
                    var nombreExcel = string.Concat("Inventario_Fisico_", DateTime.Now.ToString(), ".xlsx");

                    return File(memoria.ToArray(), "appclication/vnd.openxmlformat-officedocument.spreadsheetml.sheet", nombreExcel);
                }
            }
        }


        #region region de Apis
        [HttpGet]
        public async Task<IActionResult> ListaEmpresa()
        {
            var empresa = @"SELECT EMPCOD , EMPNOM FROM PRM_EMPRESA pe WHERE EMPMANDIALAB = 'F' ";

            var oEmpresa = new List<PRM_EMPRESAS>();

            using (var connection = new OracleConnection(_cadena))
            {
                await connection.OpenAsync();

                var cmd = new OracleCommand(empresa, connection);
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var oDatos = new PRM_EMPRESAS();
                    {
                        oDatos.EMPCOD = Convert.ToString(reader["EMPCOD"]);
                        oDatos.EMPNOM = Convert.ToString(reader["EMPNOM"]);
                    }
                    oEmpresa.Add(oDatos);
                }
            }
            return StatusCode(StatusCodes.Status200OK, oEmpresa);
        }

        [HttpGet]
        public async Task<IActionResult> ListaBodegabyId(string empcod)
        {
            var consulta = @"SELECT ptb.EMPCOD AS CODIGO, ptb.TIECOD AS TIENDA, ptb.TIENOM  AS NOMBRE
	                            FROM PRM_TIENDA_BODEGA ptb WHERE EMPCOD in('00001','00002','00004') 
                                    AND TIEEST = 'N' 
	                                ORDER BY TIECOD ";

            var oTiendas = new List<PRM_TIENDAS>();
            using (var conexion = new OracleConnection(_cadena))
            {
                await conexion.OpenAsync();
                var cmd = new OracleCommand(consulta, conexion);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var oDatos = new PRM_TIENDAS();
                        {
                            oDatos.EMPCOD = Convert.ToString(reader["CODIGO"]);
                            oDatos.TIENDA = Convert.ToString(reader["TIENDA"]);
                            oDatos.NOMBRE = Convert.ToString(reader["NOMBRE"]);
                        }
                        oTiendas.Add(oDatos);
                    }
                }
            }
            var filtrado = oTiendas.Where(e => e.EMPCOD == empcod).ToList();
            return StatusCode(StatusCodes.Status200OK, filtrado);
        }
        #endregion
    }
}
