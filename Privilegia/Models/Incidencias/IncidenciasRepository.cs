using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Privilegia.Models.Incidencias
{
    public class IncidenciasRepository : BaseRepository<IncidenciaModel> , IIncidenciaRepository
    {
        public List<IncidenciaModel> ObtenerTodasLasIncidencias()
        {
            using (Contexto context = new Contexto())
            {
                return context.Incidencias.OfType<IncidenciaModel>().ToList();
            }
        }
    }
}