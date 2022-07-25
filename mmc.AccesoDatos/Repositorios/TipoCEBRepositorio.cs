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
    public class TipoCEBRepositorio : Repositorio<TiposCEB>, ITipoCEBRepositorio 
    {
        private readonly ApplicationDbContext _db;

        public TipoCEBRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(TiposCEB Tipo)
        {
            var TipoDB = _db.TiposCEB.FirstOrDefault(b => b.Id == Tipo.Id);
            if (TipoDB != null)
            {
                TipoDB.Tipo = Tipo.Tipo;
                TipoDB.Estado = Tipo.Estado;
            }
        }

    }
}
