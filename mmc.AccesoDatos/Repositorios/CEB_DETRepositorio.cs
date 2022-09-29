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
    public class CAB_DETRepositorio : Repositorio<CEB_DET>, ICEB_DETRepositorio
    {
        private readonly ApplicationDbContext _db;

        public CAB_DETRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(CEB_DET oCEB_DET)
        {
            var ObjetoDb = _db.CEB_DETs.FirstOrDefault(b => b.Id == oCEB_DET.Id);
            if (ObjetoDb != null)
            {
                ObjetoDb.Cistianos = oCEB_DET.Cistianos;
                ObjetoDb.NoCistianos = oCEB_DET.NoCistianos;
                ObjetoDb.Ninios = oCEB_DET.Ninios;
                ObjetoDb.Total = oCEB_DET.Total;
                ObjetoDb.Convertidos = oCEB_DET.Convertidos;
                ObjetoDb.Reconcilia = oCEB_DET.Reconcilia;
                ObjetoDb.Ofrenda = oCEB_DET.Ofrenda;
                ObjetoDb.Foto = oCEB_DET.Foto;
            }
        }

    }
}
