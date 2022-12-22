using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting.Internal;
using mmc.Modelos.BodegaModels;
using mmc.Utilidades;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System;
using mmc.Modelos.ContabilidadModels;
//using DocumentFormat.OpenXml.Drawing;
//using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using mmc.Modelos.ViewModels;
using mmc.Modelos;

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
        public IActionResult ActivosFIjosFoto(string ArtCod) 
        {
            try
            {
                var result = new AF_Activos_Fijos();
                using (var connection = new OracleConnection(_cadenaTest))
                {
                    connection.Open();
                    result = Get_AF_byId(connection, ArtCod);
                }
                return View(result);
            }
            catch (Exception)
            {

                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult ActivosFIjosFoto(AF_Activos_Fijos activoFijoDB)
        {
            if(ModelState.IsValid)
            {
                //Obtenemos la ruta absoluta de la carpeta raiz
                string webRootPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string filename = activoFijoDB.CODIGOACTIVO;//Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"\\192.168.0.3\FotosActivosFijos"); // @"imagenes\Contabilidad\ActivosFijos");
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
                    //activoFijoDB.ACFIFOTO = @"\imagenes\Contabilidad\ActivosFijos\" + filename + extension;
                    activoFijoDB.ACFIFOTO = @"\\192.168.0.3\FotosActivosFijos\" + filename + extension;
                }

                Actualizar(activoFijoDB, conexion());

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        #region Conneccion DB

        public OracleConnection conexion()
        {
            try
            {
                //using (var connection = new OracleConnection(_cadenaTest))
                var connection = new OracleConnection(_cadenaTest);
                //{
                connection.Open();
                return connection;
                //}
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        #endregion

        #region Logina de negocio

        public void Actualizar(AF_Activos_Fijos Model, OracleConnection connection)
        {
            var foto = Model.ACFIFOTO;
            var query = @"UPDATE AF_ACTIVOS_FIJOS
	                        SET ACFIFOTO='" + foto +"'  WHERE PAICOD='00001' AND EMPCOD='00001' AND ACFICOD='" +Model.CODIGOACTIVO + "'";

            using (var cmd = new OracleCommand(query, connection))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                //cmd.Parameters.Add(":ArtCod", Model.CODIGOACTIVO);
                //cmd.Parameters.Add(":Foto", Model.ACFIFOTO);
                cmd.ExecuteNonQuery();
            }

        }

        protected AF_Activos_Fijos Get_AF_byId(OracleConnection connection, string artCod)
        {
            var query = @"SELECT ACFICOD CodigoActivo,ACFIDSC, ACFIMARCOD Marca, ACFIMODELO Modelo, ACFISERIE Serie, ACFIFOTO , ACFITIPCOD, ACFISUBTIPCOD, ACFIFCHCMP, ACFIMONLOC, ACFINUMCHQ, CONDEPCOD, EMPLEMPCOD codigoEmpleado, ACFIUBICOD,
		                        ACFIOBS, ACFIFCHBAJ, ACFIFACNUM noFactura, PRVCOD nitProveedor, ACFIDOCREF
                            FROM AF_ACTIVOS_FIJOS aaf WHERE ACFICOD ='" + artCod + "'";
            var OActivoFijo = new List<AF_Activos_Fijos>();
            var cmd = new OracleCommand(query, connection);
            using var reader = cmd.ExecuteReader();
                reader.Read();
            
                var oDatos = new AF_Activos_Fijos();
                {
                    oDatos.CODIGOACTIVO = Convert.ToString(reader["CodigoActivo"]);
                    oDatos.ACFIDSC = Convert.ToString(reader["ACFIDSC"]);
                    oDatos.MARCA = Convert.ToString(reader["Marca"]);
                    oDatos.MODELO = Convert.ToString(reader["Modelo"]);
                    oDatos.SERIE = Convert.ToString(reader["Serie"]);
                    oDatos.ACFIOBS = Convert.ToString(reader["ACFIOBS"]);
                    oDatos.ACFIMONLOC = Convert.ToDouble(reader["ACFIMONLOC"]);
                oDatos.ACFIFOTO = Convert.ToString(reader["ACFIFOTO"]);
                }
            return oDatos;
        }
        #endregion

    }
}
