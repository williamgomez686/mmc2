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
            throw  new NotImplementedException();
        }

        public Task<AF_Activos_Fijos> MostrarPorId(string id)
        {
            throw new NotImplementedException();
        }
    }
}
