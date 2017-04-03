using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileHelpers;
using Privilegia.Models.Incidencias;

namespace Privilegia.Models.FacturacionPartners
{
    public interface IFacturacionPremiosRepository : IBaseRepository<FacturacionPremiosModel> 
    {
        List<FacturacionPremiosModel> ObtenerFacturacionPremios();

        List<FacturacionPremiosModel> ObtenerFacturasPremiosPorNifPartner(string nifPartner);

        FacturacionPremiosModel ObtenerFacturaPremiosPorIdFactura(string idFactura);

        FacturacionPremiosModel ObtenerFacturasPremiosPorMutualista(string idMutualista);

        List<FacturacionPremiosModel> ObtenerFacturacionPorMes(string mes);

        List<FacturacionPremiosModel> MapearStandar(List<FicheroYoRespondo> ficheroYoRespondo);

        List<FacturacionPremiosModel> MapearErrores(List<ErrorInfo> errores);

        bool ExisteRegistro(FacturacionPremiosModel ficheroGuijuelo);
    }
}