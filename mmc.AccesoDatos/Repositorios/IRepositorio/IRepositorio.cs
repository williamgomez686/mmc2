using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace mmc.AccesoDatos.Repositorios.IRepositorio
{
    public interface IRepositorio<T> where T :class//Creamos nuestra interface en donde el objeto generico sea una clase, T es por que es un objeto generico
    {
        T Obtener(int id);//Con este metodo obtendremos un valor por medio de su ID
        T ObtenerPorIdString(string id);//Con este metodo obtendremos un valor por medio de su ID
        IEnumerable<T> ObtenerTodos(// esta sera un alista de objetos 
            Expression<Func<T, bool>> filter = null,//con esta linea aremos un filtrado
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,// con esta linea le daremos un orden lo igualamos a nulo por si no es nesesario darle un orden
            string incluirPropiedades = null //en el caso que queramos incluir propiedades 
            );

        T ObtenerPrimero(
            Expression<Func<T, bool>> filter = null,
            string incluirPropiedades = null
            );

        void Agregar(T entidad);

        void Remover(int id);
        void Remover(T entidad);
        void removerRango(IEnumerable<T> entidad);
    }
}
