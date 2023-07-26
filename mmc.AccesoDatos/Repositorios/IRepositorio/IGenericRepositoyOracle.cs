using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.AccesoDatos.Repositorios.IRepositorio
{
    public interface IGenericRepositoyOracle<T> where T : class
    {
        Task<List<T>> Listar(string? id);
        Task<bool> Guardar(T modelo);
        Task<bool> Ediar(T modelo);
        Task<bool> Eliminar(string id);
        Task<T> MostrarPorId(string id);
    }
}
