using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mmc.AccesoDatos.Repositorios.IRepositorio
{
    public interface IUnidadTrabajo : IDisposable // Idisposable al eredar de el nos permite cerrar cualquier objeto que alla quedado en memeoria lo que seria un dabase.close() u Objeto.Disposable()
    {
        IEstadoRepositorio Estado { get; }

    }
}
