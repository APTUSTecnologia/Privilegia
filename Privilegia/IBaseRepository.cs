using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Privilegia
{
    public interface IBaseRepository<T> where T : class
    {
        void Insertar(T entidad);

        void Actualizar(T entidad);

        void Eliminar(T entidad);

        List<T> Filtrar(Expression<Func<T, bool>> predicate);
        List<T> Filtrar(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>> includes);

        T ObtenerPorId(string id);

        List<T> ObtenerTodos();
        List<T> ObtenerTodos(List<Expression<Func<T, object>>> expresion);

        T Single(Expression<Func<T, bool>> predicate);
        T Single(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>> includes);
    }
}