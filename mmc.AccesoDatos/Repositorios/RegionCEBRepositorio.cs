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
    public class RegionCEBRepositorio : Repositorio<RegionesCEB>, IRegionCEBRepositorio
    {
        private readonly ApplicationDbContext _db;

        public RegionCEBRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(RegionesCEB regiones)
        {
            var bodegaDb = _db.RegionesCEB.FirstOrDefault(b => b.Id == regiones.Id);
            if (bodegaDb != null)
            {
                bodegaDb.RegionName = regiones.RegionName;
                bodegaDb.RegionAddress = regiones.RegionAddress;
                bodegaDb.Date = regiones.Date;
                bodegaDb.hihgUser = regiones.hihgUser;
                bodegaDb.Estado = regiones.Estado;
            }
        }

    }
}
