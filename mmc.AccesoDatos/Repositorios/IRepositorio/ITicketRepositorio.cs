using mmc.Modelos;
using mmc.Modelos.TicketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.AccesoDatos.Repositorios.IRepositorio
{
    public interface ITicketRepositorio : IRepositorio<Ticket>
    {
        void Actualizar(Ticket Model);
    }
}
