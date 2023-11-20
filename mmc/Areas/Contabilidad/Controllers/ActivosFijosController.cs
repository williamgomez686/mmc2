using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using mmc.Modelos.ContabilidadModels;
using mmc.Modelos.ContabilidadModels.ViewModels;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using mmc.Utilidades;

namespace mmc.Areas.Contabilidad.Controllers
{
    [Area("Contabilidad")]
    [Authorize(Roles = DS.Role_Admin + "," + DS.Role_Contabilidad)]
    public class ActivosFijosController : Controller
    {
        private readonly string _cadena;
        private readonly string _cadenaTest;
        private IHostingEnvironment _hostingEnvironment;

        public ActivosFijosController(IConfiguration cadena, IHostingEnvironment hostingEnvironment)
        {
            _cadena = cadena.GetConnectionString("OrecleString");
            _cadenaTest = cadena.GetConnectionString("OrecleStringExternaTest");
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        // acion desde el menu buscar
        public IActionResult ActivosFIjosFoto(AF_Activos_Fijos model) 
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = new ActivoFijoUpdateVW();
                    using (var connection = new OracleConnection(_cadena))
                    {
                        connection.Open();
                        result = Get_AF_byId(connection, model.CODIGOACTIVO);
                    }

                    ViewBag.foto = result.ACFIFOTO;

                    if (result == null)
                    {
                        return View(nameof(Index));
                    }
                    else
                    {
                        return View(result);
                    }
                }
                catch (Exception)
                {
                    ViewBag.error = "Ocurrió un Error, No se encontró el Activo Fijó verifiqué que está bien escrito y vulva a intentar.";
                    return View(nameof(Index));
                    //return NotFound();
                }
            }
            return View(model);
        }

        [HttpPost]//aqui se actualilza la foto y la observacion
        public IActionResult ActivosFIjosFoto(ActivoFijoUpdateVW activoFijoDB)
        {
            if(ModelState.IsValid)
            {
                //Obtenemos la ruta absoluta de la carpeta raiz
                string webRootPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0 && files[0].Length > 0)
                {
                    string filename = activoFijoDB.IDCONTA;//Guid.NewGuid().ToString();  \\192.168.0.3\ActivosFijos
                    var uploads = Path.Combine(webRootPath, @"imagenes\Contabilidad\ActivosFijos"); //@"\\192.168.0.3\FotosActivosFijos");
                    var extension = Path.GetExtension(files[0].FileName);
                    if (activoFijoDB.ACFIFOTO != null)
                    {
                        //Esto es para editar, necesitamos borrar la imagen anterior
                        var imagenPath = Path.Combine(webRootPath, activoFijoDB.ACFIFOTO.TrimStart('\\'));
                        if (System.IO.File.Exists(imagenPath))
                        {
                            System.IO.File.Delete(imagenPath);
                        }
                    }
                    using (var filesStreams = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    activoFijoDB.ACFIFOTO = @"\imagenes\Contabilidad\ActivosFijos\" + filename + extension;
                }
                else
                {
                    // Si no se proporcionó una nueva imagen, mantener la ruta de la imagen existente
                    using (var connection = conexion())
                    {
                        connection.Open();
                        var activoFijoActual = Get_AF_byId(connection, activoFijoDB.IDCONTA);

                        if (activoFijoActual != null)
                        {
                            activoFijoDB.ACFIFOTO = activoFijoActual.ACFIFOTO;
                        }
                    }
                }
                //manda a llamar al update 
                using (var connection = conexion())
                {
                    connection.Open();

                    Actualizar(activoFijoDB, connection);
                }

                return RedirectToAction(nameof(Index));
            }

            return View();
        }
        [HttpGet]
        public IActionResult ActivosConFoto()
        {
            var result = new List<AF_Activos_Fijos>();
            var query = @"SELECT ACFICOD, ACFIDSC, ACFIMONLOC, ACFIFOTO, ACFIOBS  FROM AF_ACTIVOS_FIJOS aaf 
                            WHERE aaf.EMPCOD = '00001'
                            AND ACFIFOTO LIKE '%\imagenes\Contabilidad\ActivosFijos%'";
            using (var connection = new OracleConnection(_cadena))
            {
                connection.Open();
                using (var cmd = new OracleCommand(query, connection))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    using var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    { 
                        var oDatos = new AF_Activos_Fijos();
                        {
                            oDatos.CODIGOACTIVO = Convert.ToString(reader["ACFICOD"]);
                            oDatos.ACFIMONLOC = Convert.ToDouble(reader["ACFIMONLOC"]);
                            //oDatos.MARCA = Convert.ToString(reader["MARCA"]);
                            //oDatos.MODELO = Convert.ToString(reader["MODELO"]);
                            //oDatos.SERIE = Convert.ToString(reader["SERIE"]);
                            oDatos.ACFIDSC = Convert.ToString(reader["ACFIDSC"]);
                            oDatos.ACFIOBS = Convert.ToString(reader["ACFIOBS"]);
                            oDatos.ACFIFOTO = Convert.ToString(reader["ACFIFOTO"]);
                        }
                        result.Add(oDatos);
                    }
                }
            }
           
            return View(result);
        }

        #region Conneccion DB
        public OracleConnection conexion()
        {
            return new OracleConnection(_cadena);
        }

        #endregion

        #region Logica de negocio

        public void Actualizar(ActivoFijoUpdateVW activoFijoDB, OracleConnection connection)
        {
            // Consulta SQL parametrizada para evitar inyección de SQL
            var query = @"UPDATE AF_ACTIVOS_FIJOS 
                SET ACFIFOTO=:foto, ACFIOBS=:obs 
                WHERE PAICOD='00001' AND EMPCOD='00001' AND ACFICOD=:idConta";

            using (var cmd = new OracleCommand(query, connection))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Add(":foto", OracleDbType.Varchar2).Value = activoFijoDB.ACFIFOTO;
                cmd.Parameters.Add(":obs", OracleDbType.Varchar2).Value = activoFijoDB.ACFIOBS;
                cmd.Parameters.Add(":idConta", OracleDbType.Varchar2).Value = activoFijoDB.IDCONTA;

                cmd.ExecuteNonQuery();
            }
        }

        protected ActivoFijoUpdateVW Get_AF_byId(OracleConnection connection, string artCod)
        {
            if (artCod == null)
            {
                var oDatos = new ActivoFijoUpdateVW();
                return oDatos;
            }
            else
            {
                var query = @"SELECT aaf.ACFICOD IDCONTA, aaf.ACFIMONLOC PRECIO, am.ACFIMARDSC MARCA, aaf.ACFIMODELO MODELO, 
                            aaf.ACFISERIE SERIE, aaf.ACFIDSC DESCRIPCION, ap.ACFIPRYDSC PROYECTO, ata.ACFITIPDSC TIPOACTIVO , asa.ACFISUBTIPDSC SUBTIPOACTIVO, 
                            rer.EMPLEMPNOM EMPLEADO, rer.EMPLEMPAPE APELLIDO, cp.PRVNOM PROVEEDOR, aaf.ACFIFACNUM NOFACTURA, aaf.ACFIOBS, aaf.ACFIFOTO, aaf.ACFIESTCOD AS EstadoId, afe.ACFIESTDSC AS Estado
	                            FROM AF_ACTIVOS_FIJOS aaf 
		                            LEFT JOIN AF_PROYECTOS ap  ON ap.PAICOD = aaf.PAICOD AND ap.EMPCOD = aaf.EMPCOD AND ap.ACFIPRYCOD = aaf.ACFIPRYCOD 
		                            LEFT JOIN AF_TIPOS_ACTIVOS ata ON ata.PAICOD = aaf.PAICOD AND ata.EMPCOD = aaf.EMPCOD AND ata.ACFITIPCOD = aaf.ACFITIPCOD 
		                            LEFT JOIN RH_EMPLEADOS_RELACIONLAB rer ON rer.PAICOD = aaf.PAICOD AND rer.EMPCOD = aaf.EMPCOD AND rer.EMPLEMPCOD = aaf.EMPLEMPCOD 
		                            LEFT JOIN CXP_PROVEEDORES cp ON cp.PAICOD = aaf.PAICOD AND cp.EMPCOD = aaf.EMPCOD AND cp.PRVCOD = aaf.PRVCOD 
		                            LEFT JOIN AF_MARCAS am ON am.PAICOD = aaf.PAICOD AND am.EMPCOD = aaf.EMPCOD AND am.ACFIMARCOD = aaf.ACFIMARCOD
		                            LEFT JOIN AF_SUBTIPOS_ACTIVOS asa ON asa.PAICOD = ata.PAICOD AND asa.EMPCOD = aaf.EMPCOD AND asa.ACFITIPCOD = ata.ACFITIPCOD AND  asa.ACFISUBTIPCOD = aaf.ACFISUBTIPCOD AND asa.ACFISUBTIPCOD = aaf.ACFISUBTIPCOD  JOIN AF_ESTATUS afe ON aaf.PAICOD = afe.PAICOD AND aaf.EMPCOD = afe.EMPCOD AND aaf.ACFIESTCOD = afe.ACFIESTCOD 
                            WHERE aaf.ACFICOD ='" + artCod + "'";
                var OActivoFijo = new List<ActivoFijoUpdateVW>();
                var cmd = new OracleCommand(query, connection);
                using var reader = cmd.ExecuteReader();
                reader.Read();

                var oDatos = new ActivoFijoUpdateVW();
                {
                    oDatos.IDCONTA = Convert.ToString(reader["IDCONTA"]);
                    oDatos.PRECIO = Convert.ToDouble(reader["PRECIO"]);
                    oDatos.MARCA = Convert.ToString(reader["MARCA"]);
                    oDatos.MODELO = Convert.ToString(reader["MODELO"]);
                    oDatos.SERIE = Convert.ToString(reader["SERIE"]);
                    oDatos.DESCRIPCION = Convert.ToString(reader["DESCRIPCION"]);
                    oDatos.PROYECTO = Convert.ToString(reader["PROYECTO"]);
                    oDatos.TIPOACTIVO = Convert.ToString(reader["TIPOACTIVO"]);
                    oDatos.SUBTIPOACTIVO = Convert.ToString(reader["SUBTIPOACTIVO"]);
                    oDatos.EMPLEADO = Convert.ToString(reader["EMPLEADO"]);
                    oDatos.APELLIDO = Convert.ToString(reader["APELLIDO"]);
                    oDatos.PROVEEDOR = Convert.ToString(reader["PROVEEDOR"]);
                    oDatos.NOFACTURA = Convert.ToString(reader["NOFACTURA"]);
                    oDatos.ACFIOBS = Convert.ToString(reader["ACFIOBS"]);
                    oDatos.ACFIFOTO = Convert.ToString(reader["ACFIFOTO"]);
                    oDatos.EstadoCodigo = Convert.ToString(reader["EstadoId"]);
                    oDatos.EstadoDescripcion = Convert.ToString(reader["Estado"]);
                }
                return oDatos;
            }
        }
    }
}
#endregion