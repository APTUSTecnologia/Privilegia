using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Privilegia.Models.FacturacionPartners
{
    public interface IFacturacionPremiosRepository : IBaseRepository<FacturacionPremiosModel>
    {
        List<FacturacionPremiosModel> ObtenerFacturacionPremios();

        List<FacturacionPremiosModel> ObtenerFacturasPremiosPorNifPartner(string nifPartner);

        FacturacionPremiosModel ObtenerFacturaPremiosPorIdFactura(string idFactura);

        FacturacionPremiosModel ObtenerFacturasPremiosPorMutualista(string idMutualista);

        List<FacturacionPremiosModel> ObtenerFacturacionPorMes(string mes);

        bool InsertarGuijuelo(List<FicheroGuijuelo> ficheroGuijuelo);

        bool ExisteRegistro(FacturacionPremiosModel ficheroGuijuelo);
    }
}