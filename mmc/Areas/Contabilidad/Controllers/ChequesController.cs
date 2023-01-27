using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using mmc.Modelos.ContabilidadModels;
using mmc.Utilidades;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System;
using mmc.Modelos.ContabilidadModels.ViewModels;
using NPOI.SS.Formula.Functions;

namespace mmc.Areas.Contabilidad.Controllers
{
    [Area("Contabilidad")]
    [Authorize(Roles = DS.Role_Admin)]

    public class ChequesController : Controller
    {
        private readonly string _cadena;
        private readonly string _cadenaTest;
        //private IHostingEnvironment _hostingEnvironment;
        public ChequesController(IConfiguration cadena)
        {
            _cadena = cadena.GetConnectionString("OrecleString");
            _cadenaTest = cadena.GetConnectionString("OrecleStringExternaTest");
        }
        public IActionResult Index()
        {
            long tempPoliza = 2207000068;

            var test2 = new ChequeDetallesVM();
            var test = ObtenerChequePorPoliza(tempPoliza);
            test2.cabecera = test;
            //test2.Detalle = test.chequeDetalles;

            return View(test2);
        }

        #region Connection 
        //public OracleConnection conexion()
        //{
        //    try
        //    {
        //        //using (var connection = new OracleConnection(_cadenaTest))
        //        var connection = new OracleConnection(_cadenaTest);
        //        //{
        //        connection.Open();
        //        return connection;
        //        //}
        //    }
        //    catch (System.Exception)
        //    {
        //        throw;
        //    }
        //}
        #endregion

        #region Logica de Negocio
        public ChequeVM ObtenerChequePorPoliza(long poliza)
        {
            var convertir = new LetasaNumeros();
            var query = @"select a.ConMovMayFecDoc AS Fecha ,a.ConMovMayVal AS MontoNumeros, a.ConMovMayANomDe AS Nombrede, a.ConMovMayNeg AS Negociable ,a.ConMovMayConDoc AS Concepto,
                            d.CueMayNomCta AS NombreCuenta, b.CUEAUXNUMCTABAN AS NoCuenta, a.ConMovMaySolPor AS SolicitadoPOr, a.CONMOVMAYNUMDOC AS NoCheque, a.ConMovMayCor AS Poliza, b.CueAuxCtaBanMon,
                            a.CUEMAYCOD, a.TIPPOLCOD, a.CueMayCod, a.TipAuxCod, a.CueAuxCodAux
	                            from CONTA_MOVMES_MAYORIZADO_H a
		                            join CONTA_CUENTAS_MAYOR_Y_AUXILIAR b
			                            on a.paicod = b.paicod
			                            and a.empcod = b.empcod
			                            and a.CueMayCod = b.CueMayCod
			                            and a.TipAuxCod = b.TipAuxCod
			                            and a.CueAuxCodAux = b.CueAuxCodAux
		                            join CONTA_TIPOS_DE_POLIZA c
			                            on a.paicod = c.paicod
			                            and a.empcod = c.empcod
			                            and a.TIPPOLCOD = c.TIPPOLCOD
		                            join CONTA_CUENTAS_DE_MAYOR d
			                            on a.PAICOD = d.PAICOD
			                            and a.EMPCOD = d.EMPCOD
			                            and a.CUEMAYCOD = d.CUEMAYCOD
                            where a.empcod = '00001'
                            --and (c.TipPolTran = 'CHQ' or c.TipPolTran = 'PPR' or c.TipPolTran = 'PPA' or c.TipPolTran = 'TRB' or c.TipPolTran = 'PPROV')
                            and (c.TipPolTran = 'CHQ' or c.TipPolTran = 'TRB' or c.TipPolTran = 'PPR' or c.TipPolTran = 'PPA')
                            and b.CueAuxTipBan = 'S'
                            AND a.CONMOVMAYCOR =" + poliza;

            using (var connection = new OracleConnection(_cadena))
            {
                connection.Open();

                using (var cmd = new OracleCommand(query, connection))
                {
                    var reader = cmd.ExecuteReader();
                    reader.Read();
                    var oDatos = new ChequeVM();
                    {
                        oDatos.FECHA = Convert.ToDateTime(reader["Fecha"]);
                        oDatos.MONTONUMEROS = Convert.ToDouble(reader["MontoNumeros"]);
                        oDatos.MONTOLETRAS = convertir.enletras(oDatos.MONTONUMEROS.ToString());
                        oDatos.NOMBREDE = Convert.ToString(reader["Nombrede"]);
                        oDatos.NEGOCIABLE = Convert.ToString(reader["Negociable"]);
                        oDatos.CONCEPTO = Convert.ToString(reader["Concepto"]);
                        oDatos.NOMBRECUENTA = Convert.ToString(reader["NombreCuenta"]);
                        oDatos.NOCUENTA = Convert.ToString(reader["NoCuenta"]);
                        oDatos.SOLICITADOPOR = Convert.ToString(reader["SolicitadoPOr"]);
                        oDatos.NOCHEQUE = Convert.ToInt32(reader["NoCheque"]);
                        oDatos.POLIZA = Convert.ToInt64(reader["Poliza"]);
                        oDatos.chequeDetalles = ObtieneDetelleCheque(connection, poliza);
                    }
                    return oDatos;
                }
            }
        }

        public List<ChequeDetalle> ObtieneDetelleCheque(OracleConnection connection, long poliza)
        {
            List<ChequeDetalle> Detalles = new List<ChequeDetalle>();
            var Query = @"SELECT CONMOVMAYCARABO, CUEMAYCARABO, CUEAUXCODAUX, TIPAUXCARABO, CONMOVMAYVALDET
                                FROM CONTA_MOVMES_MAYORIZADO_D cmmd 
                                WHERE EMPCOD = '00001'
                                AND TIPPOLCOD = 'PCH'
                                AND CONMOVMAYCOR = "+ poliza + " ORDER BY CONMOVMAYCARABO DESC ";

            var cmd = new OracleCommand(Query, connection);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var model = new ChequeDetalle();
                {
                    model.CONMOVMAYCARABO = Convert.ToString(reader["CONMOVMAYCARABO"]);
                    model.CUEMAYCARABO = Convert.ToString(reader["CUEMAYCARABO"]);
                    model.CUEAUXCODAUX = Convert.ToString(reader["CUEAUXCODAUX"]);
                    model.TIPAUXCARABO = Convert.ToString(reader["TIPAUXCARABO"]);
                    model.CONMOVMAYVALDET = Convert.ToDouble(reader["CONMOVMAYVALDET"]);
                }
                Detalles.Add(model);
            }

            return Detalles;
        }
        #endregion
    }
}
