using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using mmc.Modelos.BodegaModels;
using mmc.Utilidades;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System;
using System.Data;
using System.Linq;
using System.IO;
using ClosedXML.Excel;

namespace mmc.Areas.Bodega.Controllers
{
    [Area("Bodega")]
    [Authorize(Roles = DS.Role_Admin + "," + DS.Role_Bodega)]
    public class PrintExcelController : Controller
    {
        private readonly string _cadena;
        public PrintExcelController(IConfiguration cadena)
        {
            _cadena = cadena.GetConnectionString("OrecleString");
        }
        public IActionResult Index()
        {
            var consulta = @"SELECT ptb.EMPCOD AS CODIGO, ptb.TIECOD AS TIENDA, ptb.TIENOM  AS NOMBRE
	                            FROM PRM_TIENDA_BODEGA ptb WHERE EMPCOD in('00001','00002','00004') 
                                    AND TIEEST = 'N' 
	                                ORDER BY TIECOD ";

            var empresa = @"SELECT EMPCOD , EMPNOM FROM PRM_EMPRESA pe ";

            var oEmpresa = new List<PRM_EMPRESAS>();

            using (var connection = new OracleConnection(_cadena))
            {
                connection.Open();

                var cmd = new OracleCommand(empresa, connection);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var oDatos = new PRM_EMPRESAS();
                    {
                        oDatos.EMPCOD = Convert.ToString(reader["EMPCOD"]);
                        oDatos.EMPNOM = Convert.ToString(reader["EMPNOM"]);
                    }
                    oEmpresa.Add(oDatos);
                }
            }

            ///////////////////PROCESO PARA OPTENER EL LISTADO DE LAS BODEGAS
            var oTiendas = new List<PRM_TIENDAS>();
            using (var conexion = new OracleConnection(_cadena))
            {
                conexion.Open();
                var cmd = new OracleCommand(consulta, conexion);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
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
            // codigo en Linq para optener una lista ordenada por medio de un objeto obtenido de la base de datos 
            List<PRM_TIENDAS> list = (from d in oTiendas
                                      select new PRM_TIENDAS
                                      {
                                          TIENDA = d.TIENDA,
                                          NOMBRE = d.NOMBRE
                                      }).ToList();

            //PROCESO PARA GENERAR UN OBJETO DE TIPO COMBOBOX ESTE ES PARA DESPLEGAR LAS BODEGAS
            List<SelectListItem> TiendasList = list.ConvertAll(x =>
            {
                return new SelectListItem()
                {
                    Text = x.TIENDA.ToString() + " - " + x.NOMBRE.ToString(),
                    Value = x.TIENDA.ToString(),
                    Selected = false
                };
            });
            //PROCESO PARA GENERAR UN OBJETO DE TIPO COMBOBOX ESTE ES PARA DESPLEGAR LAS EMPRESAS
            List<SelectListItem> EmpresasList = oEmpresa.ConvertAll(x =>
            {
                return new SelectListItem()
                {
                    Text = x.EMPNOM.ToString(),//empcod  NOMBRE
                    Value = x.EMPCOD.ToString(),//nombre
                    Selected = false
                };
            });

            ViewBag.bodega = TiendasList;
            ViewBag.empresa = EmpresasList;
            return View();
        }

        [HttpPost]
        public IActionResult printExcel(string TIECOD, string EMPCOD)
        {
            var consulta = @"SELECT  ii.ARTCOD Codigo, ia.ARTUNIMEDCOD AS UM, ia.ARTDES AS Descripcion, INVEXI AS Existencia, INVRES AS Reserva, (ii.INVEXI - ii.INVRES) AS total
	                                FROM INV_INVENTARIO ii 
	                                JOIN INV_ARTICULO ia 
		                                ON ii.PAICOD = ia.PAICOD 
		                                AND ii.EMPCOD = ia.EMPCOD 
		                                AND ii.ARTCOD  = ia.ARTCOD 
                                WHERE ii.EMPCOD = '" + EMPCOD + "' " +
                                "AND ii.TIECOD = '" + TIECOD + "' " + "" +
                                "AND ii.INVEXI > 0 " +
                                "ORDER BY ii.PaiCod , ii.EmpCod , ii.TieCod  , ia.CatCod , ia.SubCatCod , ia.SsubCatCod , ia.ArtDes ";

            DataTable tabla = new DataTable();

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
                    var nombreExcel = string.Concat("Listado_Existencias_", DateTime.Now.ToString(), ".xlsx");

                    return File(memoria.ToArray(), "appclication/vnd.openxmlformat-officedocument.spreadsheetml.sheet", nombreExcel);
                }
            }
        }
    }
}
