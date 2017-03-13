using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Privilegia.Models.Productos
{
    public class ProductosRespository : BaseRepository<ProductoModel>, IProductosRespository
    {
        public List<ProductoModel> ObtenerTodosLosProductos()
        {
            using (Contexto context = new Contexto())
            {
                return context.Productos.OfType<ProductoModel>().ToList();
            }
        }

        public List<ProductoModel> ObtenerTodosLosProductosPorIdPartner(string idPartner)
        {
            using (Contexto context = new Contexto())
            {
                return context.Productos.OfType<ProductoModel>().Where(d => d.IdPartner == idPartner).ToList();
            }
        }

        public ProductoModel ObtnerProductoPorIdProducto(string idProducto)
        {
            using (Contexto context = new Contexto())
            {
                var guid = Guid.Parse(idProducto);
                return context.Set<ProductoModel>().FirstOrDefault(x => x.Id == guid);
            }
        }
    }
}