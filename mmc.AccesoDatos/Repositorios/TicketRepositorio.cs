using mmc.AccesoDatos.Data;
using mmc.AccesoDatos.Repositorios.IRepositorio;
using mmc.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.AccesoDatos.Repositorios
{
    public class TicketRepositorio : Repositorio<Ticket>, ITicketRepositorio
    {
        private readonly ApplicationDbContext _db;  //importamos el DBcontex
        public TicketRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
