using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Privilegia.Models.Tipos
{
    public class TiposRepository : BaseRepository<TipoModel> , ITiposRepository
    {
        public List<TipoModel> ObtenerTipos()
        {
            using (Contexto context = new Contexto())
            {
                return context.Tipos.OfType<TipoModel>().ToList();
            }
        }
    }
}