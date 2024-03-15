using Microsoft.EntityFrameworkCore;
using mmc.AccesoDatos.Data;
using mmc.AccesoDatos.Repositorios.IRepositorio;
using mmc.Modelos.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace mmc.AccesoDatos.Repositorios
{
    public class Repositorio<T> : IRepositorio<T> where T : class  //Le agregamos <T> por que va a ser una clase generica
    {

        private readonly ApplicationDbContext _db;//importamos el DBcontex
        internal DbSet<T> dbSet;

        public Repositorio(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public void Agregar(T entidad)
        {
            dbSet.Add(entidad); //este codigo es un insert a la tabla al ser generico aplica para cualquier tabla se la base de datos
        }

        public T Obtener(int id)
        {
            return dbSet.Find(id); //Este codigo es equivalente a hacer un Select * From 
        }

        public T ObtenerPorIdString(string id)
        {
            //Este metodo busca con id que son cadenas de texto
            return dbSet.Find(id); //Este codigo es equivalente a hacer un Select * From 
        }

        public T ObtenerPrimero(Expression<Func<T, bool>> filter = null, string incluirPropiedades = null)
        {
            IQueryable<T> Consulta = dbSet;

            if (filter != null)
            {
                Consulta = Consulta.Where(filter); //Select * from Where ...
            }

            if (incluirPropiedades != null)
            {
                foreach (var incluirprop in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    Consulta = Consulta.Include(incluirprop);
                }
            }

            return Consulta.FirstOrDefault();
        }

        public IEnumerable<T> ObtenerTodos(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string incluirPropiedades = null)
        {
            IQueryable<T> Consulta = dbSet;

            if (filter != null)
            {
                Consulta = Consulta.Where(filter); //Select * from Where ...
            }

            if(incluirPropiedades != null)
            {
                foreach(var incluirprop in incluirPropiedades.Split( new char[] {','}, StringSplitOptions.RemoveEmptyEntries))  //si se incluyen la propiedades mayormente si tienen llaves foraneas
                {
                    Consulta = Consulta.Include(incluirprop);
                }
            }

            if (orderBy != null)
            {
                return orderBy(Consulta).ToList();
            }

            return Consulta.ToList();
        }

        public PagedList<T> ObtenerTodosPaginado(Parametros parametros, Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro);   //  select /* from where ....
            }
            if (incluirPropiedades != null)
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp);    //  ejemplo "Categoria,Marca"
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (!isTracking)
            {
                query = query.AsNoTracking();
            }

            return PagedList<T>.ToPagedList(query, parametros.PageNumber, parametros.PageSize);

        }

        public void Remover(int id)
        {
            T entidad = dbSet.Find(id);
            Remover(entidad);
        }

        public void Remover(T entidad)
        {
            dbSet.Remove(entidad);    // delete from 
        }
        public void removerRango(IEnumerable<T> entidad)
        {
            dbSet.RemoveRange(entidad);
        }
    }
}
