using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using mmc.Modelos.BodegaModels;
using mmc.Modelos.ContabilidadModels;
using mmc.Utilidades;
using NPOI.SS.UserModel;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
//using NPOI.XSSF.UserModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace mmc.Areas.Contabilidad.Controllers
{
    [Area("Contabilidad")]
    [Authorize(Roles = DS.Role_Admin + "," + DS.Role_Contabilidad)]
    public class ContabilidadController : Controller
    {
        private readonly string _cadena;
        private readonly string _cadenaTest;
        private IHostingEnvironment _hostingEnvironment;

        [Obsolete]
        public ContabilidadController(IConfiguration cadena, IHostingEnvironment hostingEnvironment)
        {
            _cadena = cadena.GetConnectionString("OrecleString");
            _cadenaTest = cadena.GetConnectionString("OrecleStringExternaTest");
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                using (var connection = new OracleConnection(_cadena))
                {
                    await connection.OpenAsync();
                    var DiarioXMotivo = DiarioPorMotivo(connection);

                    // Espera la tarea y obtén la lista resultante
                    List<PRM_EMPRESAS> empresaList = await PRM_EMPRESA(connection);

                    // Usa el método ConvertAll en la lista
                    List<SelectListItem> EmpresasList = empresaList.ConvertAll(x =>
                    {
                        return new SelectListItem()
                        {
                            Text = x.EMPNOM.ToString(),
                            Value = x.EMPCOD.ToString(),
                            Selected = false
                        };
                    });

                    //list que muestra una lista de PRM_CONCEPTOS_MOTIVOS
                    List<PRM_MOTIVOS_CONCEPTOS> motList = await PRM_MOTIVOS(connection);

                    List<SelectListItem> MotList = motList.ConvertAll(x =>
                    {
                        return new SelectListItem()
                        {
                            Text = x.MOTCOD.ToString() + " - " + x.MOTDES.ToString(),//empcod  NOMBRE

                            Value = x.MOTCOD.ToString(),//nombre
                            Selected = false
                        };
                    });
                    ViewBag.Motivos = MotList;
                    ViewBag.Empresas = EmpresasList;
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DporMotivo(string MOTIVO, DateTime FIni, DateTime Ffin)
        {
            var fechaInicio = FIni.Day + "/" + FIni.Month+ "/" + FIni.Year;       
            var fechaFin = Ffin.Day +"/"+ Ffin.Month + "/" + Ffin.Year;

            var oDiarioxMotivo = new List<DiarioXMotivoVM>();

            try
            {
                using (var connection = new OracleConnection(_cadena))
                {
                    connection.OpenAsync();

                    var Motivos = await PRM_MOTIVOS(connection);
                    var res = Motivos.Where(cod => cod.MOTCOD == Convert.ToInt32(MOTIVO));
                    foreach (var item in res)
                    {
                        ViewBag.Titulo = item.MOTDES;
                    }

                    if (MOTIVO == null)
                    {
                        OracleDataReader reader =  await ObetenerDporTodosMotivo(FIni, Ffin, oDiarioxMotivo, connection);
                    }
                    else
                    {
                        //Obtienes el diario por motivo por uno motivo en espesifico 
                        OracleDataReader reader = ObetenerDporUnMotivo(MOTIVO, fechaInicio, fechaFin, oDiarioxMotivo, connection);
                    }
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
            var orden = oDiarioxMotivo.OrderByDescending(o => o.MOTIVO_DESC);
            return View(orden);        
        }

        #region Metodos que contienen la logica de Negocios *****************************************************************************************************************

        public async Task<IActionResult> DescargaExcel(string MOTIVO, DateTime FIni, DateTime Ffin)
        {
            var oDiarioxMotivo = new List<DiarioXMotivoVM>();

            using (var connection = new OracleConnection(_cadena))
            {
                await connection.OpenAsync();
                if (MOTIVO == null)
                {
                    OracleDataReader reader = await ObetenerDporTodosMotivo(FIni, Ffin, oDiarioxMotivo, connection);
                }
                else
                {
                    //Obtienes el diario por motivo por uno motivo en espesifico 
                   // OracleDataReader reader = ObetenerDporUnMotivo(MOTIVO, fechaInicio, fechaFin, oDiarioxMotivo, connection);
                }

                var nombre = "Diario Por Motivos2";
                string sWebRootFolder = _hostingEnvironment.WebRootPath;
                string NombreArchivo = nombre + ".xlsx";
                string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, NombreArchivo);
                FileInfo Archivo = new FileInfo(Path.Combine(sWebRootFolder, NombreArchivo));

                var memory = new MemoryStream();
                //using (var memory = new MemoryStream())
                //{
                    using (var fs = new FileStream(Path.Combine(sWebRootFolder, NombreArchivo), FileMode.Create, FileAccess.Write))
                    {
                    // Crear el libro de trabajo
                    IWorkbook workbook = new NPOI.XSSF.UserModel.XSSFWorkbook();
                    // Crear una hoja en el libro de trabajo
                    ISheet excelSheet = workbook.CreateSheet("Libro1");

                    ISheet sheet1 = workbook.CreateSheet("sheet1");
                    sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(10, 10, 0, 10));

                    //IRow row = workbook.CreateRow(4); // se crea la fila de los títulos
                    IRow row = sheet1.CreateRow(4); // se crea la fila de los títulos
                    row.CreateCell(1).SetCellValue("DOCUMENTO");
                    row.CreateCell(2).SetCellValue("TIPO");
                    row.CreateCell(3).SetCellValue("FECHA");
                    row.CreateCell(4).SetCellValue("TOTAL");
                    row.CreateCell(5).SetCellValue("LIQUIDACION");
                    row.CreateCell(6).SetCellValue("NOMBRE");
                    row.CreateCell(7).SetCellValue("CODIGO");
                    row.CreateCell(8).SetCellValue("PROMESA");
                    row.CreateCell(9).SetCellValue("MOTIVOS");
                    //sheet1.AutoSizeColumn(1);
                    sheet1.GetType();
                    sheet1.Autobreaks = true;
                    //row.CreateCell(9).SetCellValue("MOTCOD");

                    //Titulo
                    row = excelSheet.CreateRow(1);
                    row.CreateCell(5).SetCellValue("DIARIO POR MOTIVO ");
                    sheet1.RowSumsBelow = true;
                    //row.RowStyle.Alignment;

                    row = excelSheet.CreateRow(2);
                    row.CreateCell(3).SetCellValue("Fecha del: ");
                    row.CreateCell(4).SetCellValue(FIni);
                    row.CreateCell(5).SetCellValue("Al: ");
                    row.CreateCell(6).SetCellValue(Ffin);

                    var contador = 5;

                    var orden = oDiarioxMotivo.OrderByDescending(o => o.MOTIVO_DESC);

                    foreach (var item in orden)
                    {
                        var fch = item.FECHA.Day + "/" + item.FECHA.Month + "/" + item.FECHA.Year;
                        row = excelSheet.CreateRow(contador); // se crean las filas
                        row.CreateCell(1).SetCellValue(item.DOCUMENTO);
                        //sheet1.AutoSizeColumn(0);
                        if (item.TIPO == "CA" || item.TIPO == "DE")
                        {
                            row.CreateCell(2).SetCellValue("CAJA");
                        }
                        else if (item.TIPO == "RU")
                        {
                            row.CreateCell(2).SetCellValue("RUTA");
                        }
                    row.CreateCell(3).SetCellValue(fch);
                    row.CreateCell(4).SetCellValue(item.TOTAL);
                    row.CreateCell(5).SetCellValue(item.LIQUIDACION);
                    row.CreateCell(6).SetCellValue(item.NOMBRE);
                    excelSheet.AutoSizeColumn(1);
                    row.CreateCell(7).SetCellValue(item.CLIENTE);
                    row.CreateCell(8).SetCellValue(item.PROMESA);
                    row.CreateCell(9).SetCellValue(item.MOTIVO_DESC);
                    contador++;
                    //row.CreateCell(9).SetCellValue(item.mo);
                    }
                        workbook.Write(fs);
                    }

                    using (var stream = new FileStream(Path.Combine(sWebRootFolder, NombreArchivo), FileMode.Open))
                    {
                        await stream.CopyToAsync(memory);
                    }
                    memory.Position = 0;
                    return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", NombreArchivo);
               // }

            }
        }

        static OracleDataReader ObetenerDporTodosMotivo2(string fechaInicio, string fechaFin, List<DiarioXMotivoVM> oDiarioxMotivo, OracleConnection connection)
        {
            string Query = @"select b.liqcajrecnum Documento, d.CAPTIPPAG Tipo,  a.liqcajfch fecha, (LiqCajRutaMonAbo * LiqCajDetTasCam) Total, a.LIQCAJNUM liquidacion, j.CLIRAZSOC nombre, b.clicod cliente, d.CAPNUM Promesa, pmc.MOTDES Descripcioin
                                    from REC_LIQUIDACIONES a
                                    join REC_LIQUIDACIONESRECIBOS b
                                        on a.PAICOD = b.PAICOD
                                        and a.EMPCOD = b.EMPCOD
                                        and a.LIQCAJCOD = b.LIQCAJCOD
                                        and a.LIQCAJNUM = b.LIQCAJNUM 
                                    JOIN PRM_MOTIVOS_CONCEPTOS pmc ON b.LIQCAJMOTCOD = pmc.MOTCOD AND b.PAICOD = pmc.PAICOD AND b.EMPCOD = pmc.EMPCOD 
                                    join CXC_CAPTACIONES d
                                        on b.PAICOD = d.PAICOD
                                        and b.EMPCOD = d.EMPCOD
                                        and b.LIQCAJRUTANUMDOC = d.CAPNUM
                                    join CXC_CLIENTES j
                                        on b.PAICOD = j.PAICOD
                                        and b.EMPCOD = j.EMPCOD
                                        and b.CLICOD = j.CLICOD
                                    where a.PAICOD = '00001'
                                    and a.EMPCOD = '00001'
                                    and a.LiqCajSta <> 'AN' 
                                    and b.LiqCajRecNum <> 0
                                    and b.LiqCajRutaMonAbo > 0 
                                    and a.LiqCajTipRutCod in ('00002', ' ','DDI','00004')
                                    and a.LIQCAJFCH between '" + fechaInicio + "' and '" + fechaFin + "' union ALL " +
                                                        " select  a.FACRECNUMDOC Documento, d.CAPTIPPAG Tipo, a.FacRecFecAlt fecha, b.FACRECDETMON Total, 0 liquidacion, j.CLIRAZSOC nombre, a.clicod cliente, d.CAPNUM Promesa, pmc.MOTDES Descripcioin " +
                                                   " from CAJA_FACTURAS_RECIBOS a" +
                                                    " right join CAJA_FACTURAS_RECIBOS_DETALLE b  on a.PAICOD = b.PAICOD and a.EMPCOD = b.EMPCOD and a.TIECOD = b.TIECOD and a.CAJCOD = b.CAJCOD and a.CLICOD = b.CLICOD and a.TTRCOD = b.TTRCOD and a.FACRECNOTX = b.FACRECNOTX and a.FACRECSERDOC = b.FACRECSERDOC and a.FACRECNUMDOC = b.FACRECNUMDOC" +
                                                   " JOIN PRM_MOTIVOS_CONCEPTOS pmc ON a.PAICOD = pmc.PAICOD AND a.EMPCOD = pmc.EMPCOD AND a.MOTCOD = pmc.MOTCOD" +
                                                   " Join PRM_TIPOS_TRANSACCIONES c  on a.PAICOD = c.PAICOD and a.EMPCOD = c.EMPCOD and a.TTRCOD = c.TTRCOD " +
                                                    " join CXC_CAPTACIONES d on b.PAICOD = d.PAICOD and b.EMPCOD = d.EMPCOD and b.FACRECDETCAPNUM = d.CAPNUM" +
                                                    " join CXC_CLIENTES j on b.PAICOD = j.PAICOD and b.EMPCOD = j.EMPCOD and b.CLICOD = j.CLICOD " +
                                                    " where a.EMPCOD = '00001'" +
                                                    "and a.FacRecEst <> 'A' " +
                                                   " and a.TtrCod <> 'REPAG' and a.TtrCod <> 'RECCO' " +
                                                   " AND SubStr(TtrGru,1,1)='R' " +
                                                    " and a.FacRecFecAlt between '" + fechaInicio + "' and '" + fechaFin + "'";
            var cmd = new OracleCommand(Query, connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var Model = new DiarioXMotivoVM();
                {
                    Model.DOCUMENTO = Convert.ToInt32(reader["Documento"]);
                    Model.TIPO = Convert.ToString(reader["Tipo"]);
                    Model.FECHA = Convert.ToDateTime(reader["fecha"]);
                    Model.TOTAL = Convert.ToDouble(reader["Total"]);
                    Model.LIQUIDACION = Convert.ToInt64(reader["liquidacion"]);
                    Model.NOMBRE = Convert.ToString(reader["nombre"]);
                    Model.CLIENTE = Convert.ToString(reader["cliente"]);
                    Model.PROMESA = Convert.ToInt64(reader["Promesa"]);
                    Model.MOTIVO_DESC = Convert.ToString(reader["Descripcioin"]);
                }
                oDiarioxMotivo.Add(Model);
            }

            return reader;
        }
        //MEDOTO ASYNCRONO Y CON SP
        static async Task<OracleDataReader> ObetenerDporTodosMotivo(DateTime fechaInicio, DateTime fechaFin, List<DiarioXMotivoVM> oDiarioxMotivo, OracleConnection connection)
        {
            try
            {
                using (OracleCommand command = new OracleCommand("SP_OBTENER_DIARIO_POR_MOTIVO", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agrega los parámetros de entrada
                    command.Parameters.Add(new OracleParameter("fechaInicio", OracleDbType.Date)).Value = fechaInicio;
                    command.Parameters.Add(new OracleParameter("fechaFin", OracleDbType.Date)).Value = fechaFin;

                    // Parámetro de salida (cursor)
                    command.Parameters.Add(new OracleParameter
                    {
                        ParameterName = "oDiarioxMotivo",
                        OracleDbType = OracleDbType.RefCursor,
                        Direction = ParameterDirection.Output
                    });

                    // Ejecutar el procedimiento almacenado
                    using (OracleDataReader reader = (OracleDataReader)await command.ExecuteReaderAsync())
                    {
                        // Carga los datos en el DataTable
                        while (reader.Read())
                        {
                            var Model = new DiarioXMotivoVM();
                            {
                                Model.DOCUMENTO = Convert.ToInt32(reader["Documento"]);
                                Model.TIPO = Convert.ToString(reader["Tipo"]);
                                Model.FECHA = Convert.ToDateTime(reader["fecha"]);
                                Model.TOTAL = Convert.ToDouble(reader["Total"]);
                                Model.LIQUIDACION = Convert.ToInt64(reader["liquidacion"]);
                                Model.NOMBRE = Convert.ToString(reader["nombre"]);
                                Model.CLIENTE = Convert.ToString(reader["cliente"]);
                                Model.PROMESA = Convert.ToInt64(reader["Promesa"]);
                                Model.MOTIVO_DESC = Convert.ToString(reader["Descripcioin"]);
                            }
                            oDiarioxMotivo.Add(Model);
                        }
                        return reader;
                    }
                }

            }
            catch (Exception ex)
            {
                var lol = ex.Message;
                throw;
            }
        }

        static OracleDataReader ObetenerDporUnMotivo(string MOTIVO, string fechaInicio, string fechaFin, List<DiarioXMotivoVM> oDiarioxMotivo, OracleConnection connection)
        {
            string Query = @"select b.liqcajrecnum Documento, d.CAPTIPPAG Tipo,  a.liqcajfch fecha, (LiqCajRutaMonAbo * LiqCajDetTasCam) Total, a.LIQCAJNUM liquidacion, j.CLIRAZSOC nombre, b.clicod cliente, d.CAPNUM Promesa, pmc.MOTDES Descripcioin 
                                    from REC_LIQUIDACIONES a
                                    join REC_LIQUIDACIONESRECIBOS b
                                        on a.PAICOD = b.PAICOD
                                        and a.EMPCOD = b.EMPCOD
                                        and a.LIQCAJCOD = b.LIQCAJCOD
                                        and a.LIQCAJNUM = b.LIQCAJNUM 
                                    JOIN PRM_MOTIVOS_CONCEPTOS pmc ON b.LIQCAJMOTCOD = pmc.MOTCOD AND b.PAICOD = pmc.PAICOD AND b.EMPCOD = pmc.EMPCOD
                                    join CXC_CAPTACIONES d
                                        on b.PAICOD = d.PAICOD
                                        and b.EMPCOD = d.EMPCOD
                                        and b.LIQCAJRUTANUMDOC = d.CAPNUM
                                    join CXC_CLIENTES j
                                        on b.PAICOD = j.PAICOD
                                        and b.EMPCOD = j.EMPCOD
                                        and b.CLICOD = j.CLICOD
                                    where a.PAICOD = '00001'
                                    and a.EMPCOD = '00001'
                                    and a.LiqCajSta <> 'AN' 
                                    and b.LiqCajRecNum <> 0
                                    and b.LiqCajRutaMonAbo > 0 
                                    and a.LiqCajTipRutCod in ('00002', ' ','DDI','00004')
                                    and a.LIQCAJFCH between '" + fechaInicio + "' and '" + fechaFin + "' and b.LiqCajMotCod = " + MOTIVO + " union ALL " +
                                                        " select  a.FACRECNUMDOC Documento, d.CAPTIPPAG Tipo, a.FacRecFecAlt fecha, b.FACRECDETMON Total, 0 liquidacion, j.CLIRAZSOC nombre, a.clicod cliente, d.CAPNUM Promesa, pmc.MOTDES Descripcioin " +
                                                   " from CAJA_FACTURAS_RECIBOS a" +
                                                    " right join CAJA_FACTURAS_RECIBOS_DETALLE b  on a.PAICOD = b.PAICOD and a.EMPCOD = b.EMPCOD and a.TIECOD = b.TIECOD and a.CAJCOD = b.CAJCOD and a.CLICOD = b.CLICOD and a.TTRCOD = b.TTRCOD and a.FACRECNOTX = b.FACRECNOTX and a.FACRECSERDOC = b.FACRECSERDOC and a.FACRECNUMDOC = b.FACRECNUMDOC" +
                                                   " JOIN PRM_MOTIVOS_CONCEPTOS pmc ON a.PAICOD = pmc.PAICOD AND a.EMPCOD = pmc.EMPCOD AND a.MOTCOD = pmc.MOTCOD " +
                                                   "Join PRM_TIPOS_TRANSACCIONES c  on a.PAICOD = c.PAICOD and a.EMPCOD = c.EMPCOD and a.TTRCOD = c.TTRCOD " +
                                                    " join CXC_CAPTACIONES d on b.PAICOD = d.PAICOD and b.EMPCOD = d.EMPCOD and b.FACRECDETCAPNUM = d.CAPNUM" +
                                                    " join CXC_CLIENTES j on b.PAICOD = j.PAICOD and b.EMPCOD = j.EMPCOD and b.CLICOD = j.CLICOD " +
                                                    " where a.EMPCOD = '00001'" +
                                                    "and a.FacRecEst <> 'A' " +
                                                   " and a.TtrCod <> 'REPAG' and a.TtrCod <> 'RECCO' " +
                                                   " AND SubStr(TtrGru,1,1)='R' " +
                                                    " and a.FacRecFecAlt between '" + fechaInicio + "' and '" + fechaFin + "' and a.MOTCOD =" + MOTIVO;

            var cmd = new OracleCommand(Query, connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var Model = new DiarioXMotivoVM();
                {
                    Model.DOCUMENTO = Convert.ToInt32(reader["Documento"]);
                    Model.TIPO = Convert.ToString(reader["Tipo"]);
                    Model.FECHA = Convert.ToDateTime(reader["fecha"]);
                    Model.TOTAL = Convert.ToDouble(reader["Total"]);
                    Model.LIQUIDACION = Convert.ToInt64(reader["liquidacion"]);
                    Model.NOMBRE = Convert.ToString(reader["nombre"]);
                    Model.CLIENTE = Convert.ToString(reader["cliente"]);
                    Model.PROMESA = Convert.ToInt64(reader["Promesa"]);
                    Model.MOTIVO_DESC = Convert.ToString(reader["Descripcioin"]);
                }
                oDiarioxMotivo.Add(Model);
            }

            return reader;
        }
 
        protected async Task<List<PRM_EMPRESAS>> PRM_EMPRESA(OracleConnection connection)
        {
            var empresa = "SELECT EMPCOD, EMPNOM FROM PRM_EMPRESA pe";
            var oEmpresa = new List<PRM_EMPRESAS>();

            using (var cmd = new OracleCommand(empresa, connection))
            {
               // await connection.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
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
            }

            return oEmpresa;
        }

        protected async Task<List<PRM_MOTIVOS_CONCEPTOS>> PRM_MOTIVOS(OracleConnection connection)
        {
            var empresa = @"SELECT EMPCOD , MOTCOD , CUEMAYCOD ,MOTDES 
                    FROM PRM_MOTIVOS_CONCEPTOS pmc 
                    WHERE EMPCOD = '00001'
                    AND MOTEST = 'N'";
            var oMotivos = new List<PRM_MOTIVOS_CONCEPTOS>();

            using (var cmd = new OracleCommand(empresa, connection))
            {
                // Ejecutar la consulta de forma asíncrona
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var model = new PRM_MOTIVOS_CONCEPTOS();
                        {
                            model.EMPCOD = Convert.ToString(reader["EMPCOD"]);
                            model.MOTCOD = Convert.ToInt32(reader["MOTCOD"]);
                            model.CUEMAYCOD = Convert.ToString(reader["CUEMAYCOD"]);
                            model.MOTDES = Convert.ToString(reader["MOTDES"]);
                        }
                        oMotivos.Add(model);
                    }
                }
            }

            return oMotivos;
        }
        protected List<DiarioXMotivoVM> DiarioPorMotivo(OracleConnection connection)
        {
            string Query = @"select 
	                                a.FACRECNUMDOC Documento, d.CAPTIPPAG Tipo, a.FacRecFecAlt fecha, b.FACRECDETMON Total, 0 liquidacion, j.CLIRAZSOC nombre, a.clicod cliente, d.CAPNUM Promesa
                                from CAJA_FACTURAS_RECIBOS a
                                right join CAJA_FACTURAS_RECIBOS_DETALLE b
                                    on a.PAICOD = b.PAICOD
                                    and a.EMPCOD = b.EMPCOD
                                    and a.TIECOD = b.TIECOD
                                    and a.CAJCOD = b.CAJCOD
                                    and a.CLICOD = b.CLICOD
                                    and a.TTRCOD = b.TTRCOD
                                    and a.FACRECNOTX = b.FACRECNOTX
                                    and a.FACRECSERDOC = b.FACRECSERDOC
                                    and a.FACRECNUMDOC = b.FACRECNUMDOC
                                Join PRM_TIPOS_TRANSACCIONES c
                                    on a.PAICOD = c.PAICOD
                                    and a.EMPCOD = c.EMPCOD
                                    and a.TTRCOD = c.TTRCOD
                                join CXC_CAPTACIONES d
                                    on b.PAICOD = d.PAICOD
                                    and b.EMPCOD = d.EMPCOD
                                    and b.FACRECDETCAPNUM = d.CAPNUM
                                join CXC_CLIENTES j
                                    on b.PAICOD = j.PAICOD
                                    and b.EMPCOD = j.EMPCOD
                                    and b.CLICOD = j.CLICOD
                                where a.EMPCOD = '00001'
                                and a.FacRecEst <> 'A'
                                and a.FacRecFecAlt between '27/10/2022' and '27/10/2022'
                                and a.TtrCod <> 'REPAG' and a.TtrCod <> 'RECCO'
                                AND SubStr(TtrGru,1,1)='R'
                                and a.MOTCOD = 3";
            var oDiarioxMotivo = new List<DiarioXMotivoVM>();

            var cmd = new OracleCommand(Query, connection);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var Model = new DiarioXMotivoVM();
                {
                    Model.DOCUMENTO = Convert.ToInt32(reader["Documento"]);
                    Model.TIPO = Convert.ToString(reader["Tipo"]);
                    Model.FECHA = Convert.ToDateTime(reader["fecha"]);
                    Model.TOTAL = Convert.ToDouble(reader["Total"]);
                    Model.LIQUIDACION = Convert.ToInt32(reader["liquidacion"]);
                    Model.NOMBRE = Convert.ToString(reader["nombre"]);
                    Model.CLIENTE = Convert.ToString(reader["cliente"]);
                    Model.PROMESA = Convert.ToInt64(reader["Promesa"]);

                }
                oDiarioxMotivo.Add(Model);
            }
            return oDiarioxMotivo;
        }

        #endregion ***********************************************************************************************************************************************************

    }
}
