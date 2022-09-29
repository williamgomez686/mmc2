using mmc.AccesoDatos.Data;
using mmc.AccesoDatos.Repositorios.IRepositorio;
using mmc.Modelos;
using mmc.Modelos.IglesiaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.AccesoDatos.Repositorios
{
    public class CEB_CABRepositorio : Repositorio<CEB_CAB>, ICEB_CABRpositorio
    {
        private readonly ApplicationDbContext _db;

        public CEB_CABRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(CEB_CAB oCEB)
        {
            var ObjetoDb = _db.CEB_CABs.FirstOrDefault(b => b.Id == oCEB.Id);
            if (ObjetoDb != null)
            {
                ObjetoDb.Hora = oCEB.Hora;
                ObjetoDb.TipoCebId = oCEB.TipoCebId;
                ObjetoDb.MiembrosCEBid = oCEB.MiembrosCEBid;
               // ObjetoDb.Ceb_Detalle = oCEB.Ceb_Detalle;
                ObjetoDb.Usuario = oCEB.Usuario;
                ObjetoDb.UsuarioModifica = oCEB.UsuarioModifica;
                ObjetoDb.FechaAlta = oCEB.FechaAlta;
                ObjetoDb.Fechamodifica = oCEB.Fechamodifica;
                ObjetoDb.Estado = oCEB.Estado;
            }
        }

    }
}
