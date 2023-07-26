using Microsoft.Extensions.Configuration;
using mmc.AccesoDatos.Repositorios.IRepositorio;
using mmc.Modelos.ContabilidadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.AccesoDatos.Repositorios
{
    public class ActivosFijosRepositoryOracle : IGenericRepositoyOracle<AF_Activos_Fijos>
    {
        private readonly string _Cadena = "";

        //public ActivosFijosRepositoryOracle()
        //{
        //}

        public ActivosFijosRepositoryOracle(IConfiguration configuration)
        {
            _Cadena = configuration.GetConnectionString("OracleString");
        }
        public Task<bool> Ediar(AF_Activos_Fijos modelo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Eliminar(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Guardar(AF_Activos_Fijos modelo)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AF_Activos_Fijos>> Listar(string id)
        {
        //    //string id= "0001545";
        //    var query = @"SELECT aaf.ACFICOD IDCONTA, aaf.ACFIMODELO MODELO,  aaf.ACFIDSC DESCRIPCION, aaf.ACFIMONLOC PRECIO
        //                        FROM AF_ACTIVOS_FIJOS aaf 
        //                        WHERE EMPCOD = '00001'
        //                        AND EMPLEMPCOD = '" + id + "'";

        //    List<AF_Activos_Fijos> af = new List<AF_Activos_Fijos>();
        //    try
        //    {
        //        using (OracleConnection contex = new OracleConnection(_Cadena))
        //        {
        //            contex.Open();
        //            using (OracleCommand cmd = new OracleCommand(query, contex))
        //            {
        //                cmd.CommandType = System.Data.CommandType.Text;
        //                using (var dr = await cmd.ExecuteReaderAsync())
        //                {
        //                    while (await dr.ReadAsync())
        //                    {
        //                        var result = new AF_Activos_Fijos();
        //                        {
        //                            //result.EmpCod = dr["paicod"].ToString();
        //                            result.CODIGOACTIVO = dr["IDCONTA"].ToString();
        //                            result.MODELO = dr["MODELO"].ToString();
        //                            result.ACFIDSC = dr["DESCRIPCION"].ToString();
        //                        }
        //                        af.Add(result);
        //                    }
        //                }
        //            }
        //            return af;
        //        }
        //    }
        //    catch (Exception error)
        //    {

        //        throw;
        //    }
        throw new NotImplementedException();
    }

        public Task<AF_Activos_Fijos> MostrarPorId(string id)
        {
            throw new NotImplementedException();
        }
    }
}
