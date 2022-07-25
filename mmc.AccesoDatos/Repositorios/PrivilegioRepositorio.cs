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
    public class PrivilegioRepositorio : Repositorio<PrivilegioCEB>, IPrivilegioRepositorio 
    {
        private readonly ApplicationDbContext _db;

        public PrivilegioRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(PrivilegioCEB privilegio)
        {
            var PrivilegioDB = _db.privilegios.FirstOrDefault(b => b.Id == privilegio.Id);
            if (PrivilegioDB != null)
            {
                PrivilegioDB.Cargos = privilegio.Cargos;
                PrivilegioDB.Estado = privilegio.Estado;
            }
        }

    }
}
