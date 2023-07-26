using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Office2021.DocumentTasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using mmc.Modelos.ContabilidadModels;
using mmc.Modelos.OracleModels.RRHH;
using mmc.Modelos.ViewModels.IglesiaVM;
using mmc.Utilidades;
using NPOI.SS.Formula.Functions;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace mmc.Areas.Contabilidad.Controllers
{
    [Area("Contabilidad")]
    [Authorize(Roles = DS.Role_Admin + "," + DS.Role_Contabilidad)]
    public class RelacionEmpleadoAFController : Controller
    {
        public readonly string _cadena;
        public readonly string _cadenaTest;

        public RelacionEmpleadoAFController(IConfiguration cadena)
        {
            _cadena = cadena.GetConnectionString("OrecleString");
            _cadenaTest = cadena.GetConnectionString("OrecleStringExternaTest");
        }

        public async Task<IActionResult> Index(int Pag, int Registros, string Search)
        {
            var result = await ObtieneEmpleados();

            if (!string.IsNullOrEmpty(Search))
            {
                Search = Search.ToUpperInvariant(); // Convertir a mayúsculas de forma segura
                // Filtrar la lista result con el criterio de búsqueda
                result = result.Where(e => e.EMPLEMPCOD.Contains(Search) || e.EMPLEMPNOM.Contains(Search)
                                                                            || e.EMPLEMPAPE.Contains(Search)).ToList();
            }

            string host = Request.Scheme + "://" + Request.Host.Value;

            object[] resultado = new Paginador<RHEMPLEADOSRELACIONLAB>()
                                .paginador(result.OrderBy(n => n.EMPLEMPNOM).ToList(), Pag, Registros, "Contabilidad", "RelacionEmpleadoAF", "Index", host);

            DataPaginador<RHEMPLEADOSRELACIONLAB> modelo = new DataPaginador<RHEMPLEADOSRELACIONLAB>
            {
                Lista = resultado[2] as List<RHEMPLEADOSRELACIONLAB>,
                Pagi_info = resultado[0] as string,
                Pagi_navegacion = resultado[1] as string,
            };

            return View(modelo);
        }

        public async Task<IActionResult> VerActivosFijos(string codigoEmpleado, int Pag, int Registros, string Search)
        {
           // HttpContext.Session.SetString("CodigoEmpleado", codigoEmpleado);

            ViewBag.CodigoEmpleado = codigoEmpleado;

            var activosFijos = await GetActivosFijosFromOracle(codigoEmpleado);
            // Hacer algo con la lista de activosFijos, por ejemplo, devolver una vista con los resultados.

            string host = Request.Scheme + "://" + Request.Host.Value;

            object[] resultado = new Paginador<AF_Activos_Fijos>()
                                .paginador(activosFijos.OrderBy(n => n.ACFIDSC).ToList(), Pag, 200, "Contabilidad", "RelacionEmpleadoAF", "VerActivosFijos", host);

            DataPaginador<AF_Activos_Fijos> modelo = new DataPaginador<AF_Activos_Fijos>
            {
                Lista = resultado[2] as List<AF_Activos_Fijos>,
                Pagi_info = resultado[0] as string,
                Pagi_navegacion = resultado[1] as string,
            };

            return View(modelo);
        }

        #region ****************************************** Logica de negocio ***********************************************

        private async Task<List<RHEMPLEADOSRELACIONLAB>> ObtieneEmpleados()
        {
            var result = new List<RHEMPLEADOSRELACIONLAB>();

            try
            {
                using (var conexion = new OracleConnection(_cadenaTest))
                {
                    await conexion.OpenAsync();
                    using(var cmd = conexion.CreateCommand())
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandText = "SP_ObtieneEmpleado";

                        //parametro de salida
                        cmd.Parameters.Add("P_cursor", OracleDbType.RefCursor).Direction = System.Data.ParameterDirection.Output;
                        using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var empleado = new RHEMPLEADOSRELACIONLAB();
                                {
                                    empleado.EMPLEMPCOD = reader.GetString(0);
                                    empleado.EMPLEMPNOM= reader.GetString(1);
                                    empleado.EMPLEMPAPE = reader.GetString(2);
                                    empleado.EMPLEMPSEXO = reader.GetString(3);
                                    empleado.EMPLEMPFCHANAC = reader.GetDateTime(4);
                                    empleado.EMPLEMPDIRECCION = reader.GetString(5);
                                    empleado.EMPLEMPTELRESI = reader.GetString(6);
                                    empleado.EMPLEMPTELMOVIL = reader.GetString(7);
                                    empleado.DPI = reader.GetInt64(8);
                                    empleado.EMPLEMPNIT = reader.GetString(9);
                                    empleado.EMPLEMPFOTO = reader.GetString(10);
                                }
                                result.Add(empleado);
                            }
                        }
                        return result;
                    }
                }
            }
            catch (Exception error)
            {

                throw;
            }

        }

        public async Task<List<AF_Activos_Fijos>> GetActivosFijosFromOracle( string CodigoEmpleado)
        {
            string empCod = "00001";
            var result = new List<AF_Activos_Fijos>();

            try
            {
                using (var conexion = new OracleConnection(_cadenaTest))
                {
                    await conexion.OpenAsync();

                    using (var cmd = conexion.CreateCommand())
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandText = "SP_ObtenerAF";

                        // Parámetros de entrada
                        cmd.Parameters.Add("p_empcod", OracleDbType.Varchar2).Value = empCod;
                        cmd.Parameters.Add("p_emplempcod", OracleDbType.Varchar2).Value = CodigoEmpleado;

                        // Parámetro de salida
                        cmd.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = System.Data.ParameterDirection.Output;

                        using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var activoFijo = new AF_Activos_Fijos
                                {
                                    CODIGOACTIVO = reader.GetString(0),
                                    MODELO = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                                    ACFIDSC = reader.GetString(2),
                                    ACFIMONLOC = (double)reader.GetDecimal(3),
                                    ACFIFOTO = reader.IsDBNull(4) ? string.Empty : reader.GetString(4)
                                };
                                result.Add(activoFijo);
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                // Manejar errores apropiadamente
                throw;
            }
        }
        
        #endregion
    }
}
