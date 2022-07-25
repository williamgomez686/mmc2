using mmc.AccesoDatos.Data;
using mmc.AccesoDatos.Repositorios.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.AccesoDatos.Repositorios
{
    public class UnidadTrabajo : IUnidadTrabajo
    {

        private readonly ApplicationDbContext _db;//importamos el DBcontex
        public IBodegaRepositorio Bodega { get; private set; }
        public IEstadoRepositorio Estado { get; private set; }
        public IUsuarioAplicacionRepositorio UsuarioAplicacion { get; private set; }
        public ITicketRepositorio Ticket { get; private set; }
        public ICategoriaRepositorio Categoria { get; private set; }
        public IMarcaRepositorio Marca { get; private set; }
        public IPrivilegioRepositorio PrivilegiosCEB { get; private set; }
        public ITipoCEBRepositorio TiposCEB { get; private set; }

        public UnidadTrabajo(ApplicationDbContext db)//Creamos el constructor
        {
            _db = db;
            Bodega = new BodegaRepositorio(_db);
            Estado = new EstadoRepositorio(_db); //Inisialisando
            UsuarioAplicacion = new UsuarioAplicacionRepositorio(_db);
            Ticket = new TicketRepositorio(_db);
            Marca = new MarcaRepositorio(_db);
            Categoria = new CategoriaRepositorio(_db);
            PrivilegiosCEB = new PrivilegioRepositorio(_db);
            TiposCEB = new TipoCEBRepositorio(_db);
        }

        public void Guardar()
        {
            _db.SaveChanges();
        }

        public void Dispose()//Con este metodo liberamos lo que esta en memoria es un metodo que heredamos desde IUnidadTrabajo que a su vez lo hereda de Dispose
        {
            _db.Dispose();
        }
    }
}
