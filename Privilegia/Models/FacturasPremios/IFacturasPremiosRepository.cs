using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Privilegia.Models.FacturacionPartners;

namespace Privilegia.Models.FacturasPremios
{
    public interface IFacturasPremiosRepository : IBaseRepository<FacturasPremiosModel>
    {
        List<FacturasPremiosModel> ObtenerFacturacionPremios();

        List<FacturasPremiosModel> ObtenerFacturasPremiosPorIdPartner(string idPartner);

        FacturasPremiosModel ObtenerFacturaPremiosPorIdFactura(string idFactura);

        FacturasPremiosModel ObtenerFacturasPremiosPorMutualista(string idMutualista);

        List<FacturasPremiosModel> ObtenerFacturacionPorMes(string mes);
    }
}