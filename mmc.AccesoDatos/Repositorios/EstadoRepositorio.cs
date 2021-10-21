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
    public class EstadoRepositorio : Repositorio<estado>, IEstadoRepositorio
    {
        private readonly ApplicationDbContext _db;  //importamos el DBcontex
        public EstadoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        //Agregamos el metodo Actualizar ya que es el unico que falta ya que por la implementacion de la Interface IRepositorio Heredamos los demas metodos 
        public void Actualizar(estado Oestado)
        {
            var estadoDB = _db.estado.FirstOrDefault(e => e.estadoId == Oestado.estadoId);
            if (estadoDB != null)
            {
                estadoDB.est_descripcion = Oestado.est_descripcion;
                estadoDB.est_est = Oestado.est_est;
                estadoDB.est_fchalt = Oestado.est_fchalt;
                estadoDB.est_usu_alt = Oestado.est_usu_alt;

                _db.SaveChanges();
            }
        }
    }
}
