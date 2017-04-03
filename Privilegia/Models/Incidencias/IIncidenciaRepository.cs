using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Privilegia.Models.Incidencias
{
    public interface IIncidenciaRepository : IBaseRepository<IncidenciaModel>
    {
        List<IncidenciaModel> ObtenerTodasLasIncidencias();


    }
}