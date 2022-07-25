using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mmc.Modelos;

namespace mmc.AccesoDatos.Repositorios.IRepositorio
{

        public interface IProductoRepositorio : IRepositorio<Producto>
        {
            void Actualizar(Producto producto);
        }
}
