using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using mmc.Modelos.BodegaModels;
using mmc.Utilidades;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;

namespace mmc.Areas.Contabilidad.Controllers
{
    [Area("Contabilidad")]
    [Authorize(Roles = DS.Role_Admin + "," + DS.Role_Contabilidad)]
    public class ContabilidadController : Controller
    {
        private readonly string _cadena;
        public ContabilidadController(IConfiguration cadena)
        {
            _cadena = cadena.GetConnectionString("OrecleString");
        }
        public IActionResult Index()
        {
            using (var connection = new OracleConnection(_cadena))
            {
                connection.Open();
                 var  empresa = PRM_EMPRESA(connection);


                List<SelectListItem> EmpresasList = empresa.ConvertAll(x =>
                {
                    return new SelectListItem()
                    {
                        Text = x.EMPNOM.ToString(),//empcod  NOMBRE
                        Value = x.EMPCOD.ToString(),//nombre
                        Selected = false
                    };
                });
            }

            return View();
        }

        #region Metodos que contienen la logica de Negocios *****************************************************************************************************************
        protected List<PRM_EMPRESAS> PRM_EMPRESA(OracleConnection connection)
        {
            var empresa = @"SELECT EMPCOD , EMPNOM FROM PRM_EMPRESA pe ";
            var oEmpresa = new List<PRM_EMPRESAS>();
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
             return oEmpresa;
        }
        #endregion ***********************************************************************************************************************************************************

    }
}
