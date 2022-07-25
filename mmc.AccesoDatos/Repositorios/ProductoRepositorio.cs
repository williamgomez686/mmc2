using mmc.AccesoDatos.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mmc.Modelos;
using mmc.AccesoDatos.Repositorios.IRepositorio;

namespace mmc.AccesoDatos.Repositorios
{
    public class ProductoRepositorio : Repositorio<Producto>, IProductoRepositorio
    {
        private readonly ApplicationDbContext _db;

        public ProductoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Producto producto)
        {
            var productoDb = _db.Productos.FirstOrDefault(p => p.Id == producto.Id);
            if (productoDb != null)
            {
                if (producto.ImagenUrl != null)
                {
                    productoDb.ImagenUrl = producto.ImagenUrl;
                }
                if (producto.PadreId == 0)
                {
                    productoDb.PadreId = null;
                }
                else
                {
                    productoDb.PadreId = producto.PadreId;
                }
                productoDb.NumeroSerie = producto.NumeroSerie;
                productoDb.Descripcion = producto.Descripcion;
                productoDb.Precio = producto.Precio;
                productoDb.Costo = producto.Costo;
                productoDb.CategoriaId = producto.CategoriaId;
                productoDb.MarcaId = producto.MarcaId;

            }
        }
    }
}
