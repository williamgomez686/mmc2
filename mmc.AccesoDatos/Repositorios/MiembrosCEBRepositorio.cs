using mmc.AccesoDatos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mmc.Modelos;
using mmc.AccesoDatos.Repositorios.IRepositorio;
using mmc.Modelos.IglesiaModels;

namespace mmc.AccesoDatos.Repositorios
{
    public class MiembrosCEBRepositorio : Repositorio<MiembrosCEB>, IMiembrosCEBRepositorio
    {
        private readonly ApplicationDbContext _db;

        public MiembrosCEBRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(MiembrosCEB miembrosCEB)
        {
            var MiembrosDB = _db.Miembros.FirstOrDefault(p => p.Id == miembrosCEB.Id);
            if (MiembrosDB != null)
            {
                if (miembrosCEB.ImagenUrl != null)
                {
                    MiembrosDB.ImagenUrl = miembrosCEB.ImagenUrl;
                }
                //if (miembrosCEB.PadreId == 0)
                //{
                //    MiembrosDB.PadreId = null;
                //}
                //else
                //{
                //    MiembrosDB.PadreId = miembrosCEB.PadreId;
                //}
                MiembrosDB.Id = miembrosCEB.Id;
                MiembrosDB.Name = miembrosCEB.Name;
                MiembrosDB.lastName = miembrosCEB.lastName;
                MiembrosDB.Addres = miembrosCEB.Addres;
                MiembrosDB.phone = miembrosCEB.phone;
                MiembrosDB.phone2 = miembrosCEB.phone2;
                miembrosCEB.DPI = miembrosCEB.DPI;
                miembrosCEB.CargosCEBId = miembrosCEB.CargosCEBId;
                miembrosCEB.RegionId = miembrosCEB.RegionId;
            }
        }
    }
}
