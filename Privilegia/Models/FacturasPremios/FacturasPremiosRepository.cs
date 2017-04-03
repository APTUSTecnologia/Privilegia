using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Privilegia.Models.FacturasPremios
{
    public class FacturasPremiosRepository : BaseRepository<FacturasPremiosModel> , IFacturasPremiosRepository
    {
        public List<FacturasPremiosModel> ObtenerFacturacionPremios()
        {
            using (Contexto context = new Contexto())
            {
                return context.FacturasPremios.OfType<FacturasPremiosModel>().ToList();
            }
        }

        public List<FacturasPremiosModel> ObtenerFacturasPremiosPorIdPartner(string idPartner)
        {
            using (Contexto context = new Contexto())
            {
                var lista = context.FacturasPremios.OfType<FacturasPremiosModel>().Where(d => d.IdPartner == idPartner).ToList();
               
                return lista;
            }
        }

        public FacturasPremiosModel ObtenerFacturaPremiosPorIdFactura(string idFactura)
        {
            using (Contexto context = new Contexto())
            {
                var id = Guid.Parse(idFactura);
                return context.Set<FacturasPremiosModel>().FirstOrDefault(x => x.Id == id);
            }
        }

        public FacturasPremiosModel ObtenerFacturasPremiosPorMutualista(string idMutualista)
        {
            throw new NotImplementedException();
        }

        public List<FacturasPremiosModel> ObtenerFacturacionPorMes(string mes)
        {
            throw new NotImplementedException();
        }
    }
}