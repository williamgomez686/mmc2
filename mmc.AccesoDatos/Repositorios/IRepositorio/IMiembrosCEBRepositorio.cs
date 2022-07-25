using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mmc.Modelos;
using mmc.Modelos.IglesiaModels;

namespace mmc.AccesoDatos.Repositorios.IRepositorio
{

        public interface IMiembrosCEBRepositorio : IRepositorio<MiembrosCEB>
        {
            void Actualizar(MiembrosCEB miembrosCEB);
        }
}
