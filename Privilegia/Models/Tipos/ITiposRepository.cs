using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Privilegia.Models.Tipos
{
    public interface ITiposRepository : IBaseRepository<TipoModel>
    {
        List<TipoModel> ObtenerTipos();
    }
}