using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Privilegia.Models.FacturacionPublicidad
{
    public class FacturacionPublicidadRepository : BaseRepository<FacturacionPublicidadModel> , IFacturacionPublicidadRepository
    {
        public List<FacturacionPublicidadModel> ObtenerFacturacion()
        {
            using (Contexto context = new Contexto())
            {
                return context.FacturacionPublicidad.OfType<FacturacionPublicidadModel>().ToList();
            }
        }

        public List<FacturacionPublicidadModel> ObtenerFacturacionPorIdPartner(string idPartner)
        {
            using (Contexto context = new Contexto())
            {
                return context.FacturacionPublicidad.OfType<FacturacionPublicidadModel>().Where(d => d.IdPartner == idPartner).ToList();
            }
        }

        public FacturacionPublicidadModel ObtenerFacturaPorIdFactura(string idFactura)
        {
            using (Contexto context = new Contexto())
            {
                var guid = Guid.Parse(idFactura);
                return context.Set<FacturacionPublicidadModel>().FirstOrDefault(x => x.Id == guid);
            }
        }

        public FacturacionPublicidadModel ObtenerFacturaPorIdPublicidad(string idPublicidad)
        {
            using (Contexto context = new Contexto())
            {
                return context.Set<FacturacionPublicidadModel>().FirstOrDefault(x => x.IdPublicidad == idPublicidad);
            }
        }

        public List<FacturacionPublicidadModel> ObtenerFacturacionPorMes(string mes)
        {
            throw new NotImplementedException();
        }
    }
}