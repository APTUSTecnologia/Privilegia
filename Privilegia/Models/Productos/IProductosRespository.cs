using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Privilegia.Models.Productos
{
    public interface IProductosRespository : IBaseRepository<ProductoModel>
    {
        List<ProductoModel> ObtenerTodosLosProductos();

        List<ProductoModel> ObtenerTodosLosProductosPorIdPartner(string idPartner);

        ProductoModel ObtnerProductoPorIdProducto(string idProducto);
    }
}