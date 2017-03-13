using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Privilegia.Models.Archivos
{
    public class LogoRepository : BaseRepository<Logo>, ILogoRepository
    {
        public List<Logo> ObtenerTodosLosLogos()
        {
            using (Contexto context = new Contexto())
            {
                return context.Logos.OfType<Logo>().ToList();
            }
        }

        public Logo ObtenerLogoPorId(string id)
        {
            using (Contexto context = new Contexto())
            {
                var guid = Guid.Parse(id);
                return context.Set<Logo>().FirstOrDefault(x => x.Id == guid);
            }
        }

        public Logo ObtenerLogoPorIdPartner(string idPartner)
        {
            using (Contexto context = new Contexto())
            {
                return context.Set<Logo>().FirstOrDefault(x => x.IdPartner == idPartner);
            }
        }
    }
}