using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Privilegia
{
    public class BaseRepository<T> where T : class
    {
        protected DbContext Contexto = new Contexto();

        protected DbSet<T> DbSet;

        public BaseRepository()
        {
            DbSet = Contexto.Set<T>();
        }

        public void Insertar(T entidad)
        {
            using (Contexto context = new Contexto())
            {
                DbSet.Add(entidad);
                try
                {
                    Contexto.SaveChanges();
                }
                catch (Exception ex)
                {
                    
                    throw;
                }
                
            }   
        }

        public void Actualizar(T entidad)
        {
            using (Contexto context = new Contexto())
            {
                context.Entry(entidad).State = EntityState.Modified;
                context.SaveChanges();
            }
            
        }

        public void Eliminar(T entidad)
        {
            using (Contexto context = new Contexto())
            {
                context.Entry(entidad).State = EntityState.Deleted;
                context.SaveChanges();
            }
           
        }

        public List<T> Filtrar(Expression<Func<T, bool>> expresion)
        {
            using (Contexto context = new Contexto())
            {
                return (List<T>)context.Set<T>().Where(expresion).ToList();
            }
        }

        public List<T> Filtrar(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>> includes)
        {
            List<string> includelist = new List<string>();

            foreach (var item in includes)
            {
                MemberExpression body = item.Body as MemberExpression;
                if (body == null)
                    throw new ArgumentException("The body must be a member expression");

                includelist.Add(body.Member.Name);
            }

            using (Contexto context = new Contexto())
            {
                DbQuery<T> query = context.Set<T>();

                includelist.ForEach(x => query = query.Include(x));

                return (List<T>)query.Where(predicate).ToList();
            }
        }


        public T ObtenerPorId(string id)
        {
            return DbSet.Find(id);
        }

        public List<T> ObtenerTodos()
        {
            using (Contexto context = new Contexto())
            {
                return (List<T>)context.Set<T>().ToList();
            }
        }

        public List<T> ObtenerTodos(List<Expression<Func<T, object>>> expresion)
        {
            List<string> includelist = new List<string>();

            foreach (var item in expresion)
            {
                MemberExpression body = item.Body as MemberExpression;
                if (body == null)
                    throw new ArgumentException("The body must be a member expression");

                includelist.Add(body.Member.Name);
            }

            using (Contexto context = new Contexto())
            {
                DbQuery<T> query = context.Set<T>();

                includelist.ForEach(x => query = query.Include(x));

                return (List<T>)query.ToList();
            }

        }

        public T Single(Expression<Func<T, bool>> predicate)
        {
            using (Contexto context = new Contexto())
            {
                return context.Set<T>().FirstOrDefault(predicate);
            }
        }

        public T Single(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>> includes)
        {
            List<string> includelist = new List<string>();

            foreach (var item in includes)
            {
                MemberExpression body = item.Body as MemberExpression;
                if (body == null)
                    throw new ArgumentException("The body must be a member expression");

                includelist.Add(body.Member.Name);
            }

            using (Contexto context = new Contexto())
            {
                DbQuery<T> query = context.Set<T>();

                includelist.ForEach(x => query = query.Include(x));

                return query.FirstOrDefault(predicate);
            }
        }
    }
}