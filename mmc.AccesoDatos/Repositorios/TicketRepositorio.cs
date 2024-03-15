using mmc.AccesoDatos.Data;
using mmc.AccesoDatos.Repositorios.IRepositorio;
using mmc.Modelos;
using mmc.Modelos.TicketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public void Actualizar(Ticket Model)
        {
            var oModel = _db.Tickets.FirstOrDefault(m => m.Id == Model.Id);
            if (oModel != null)
            {
                oModel.Asunto = Model.Asunto;
                oModel.Descripcion = Model.Descripcion;
                oModel.Solucion = Model.Descripcion;
                oModel.EstadoTKId = Model.EstadoTKId;
                oModel.Fechamodifica = Model.Fechamodifica;
                oModel.Estado = Model.Estado;
            }
        }
    }
}
