using mmc.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.AccesoDatos.Repositorios.IRepositorio
{
    public interface IMarcaRepositorio: IRepositorio<Marca>
    {
        void Actualizar(Marca marca);
    }
}
