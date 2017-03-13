using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Privilegia.Models.FacturacionPublicidad
{
    public interface IFacturacionPublicidadRepository : IBaseRepository<FacturacionPublicidadModel>
    {
        List<FacturacionPublicidadModel> ObtenerFacturacion();

        List<FacturacionPublicidadModel> ObtenerFacturacionPorIdPartner(string idPartner);

        FacturacionPublicidadModel ObtenerFacturaPorIdFactura(string idFactura);

        FacturacionPublicidadModel ObtenerFacturaPorIdPublicidad(string idPublicidad);

        List<FacturacionPublicidadModel> ObtenerFacturacionPorMes(string mes);


    }
}