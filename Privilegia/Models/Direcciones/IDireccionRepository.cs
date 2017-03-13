using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Privilegia.Models.Direcciones
{
    public interface IDireccionRepository : IBaseRepository<DireccionModel>
    {
        List<DireccionModel> ObtenerTodasLasDirecciones();

        List<DireccionModel> ObtenerTodaslasDireccionesPorIdPartner(string idPartner);

        List<DireccionPrincipal> ObtenerTodasLasDireccionesPrincipales();

        List<DireccionSecundaria> ObtenerTodaslasDireccionesSecundarias();

        DireccionSecundaria ObtenerDireccionSecundariaPorId(string id);

        List<DireccionSecundaria> ObtenerTodaslasDireccionesSecundariasPorIdPartner(string idPartner);

        DireccionModel ObtenerDireccionPorId(string id);

        DireccionModel ObtenerDireccionPorIdPartner(string idPartner);
    }
}