using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Privilegia.Models;

namespace Privilegia.Models.Partner
{
    public class PartnerExternoRepository: BaseRepository<PartnerExterno>
    {
        public PartnerExterno ObtenerPartnerExternoPorId(string id)
        {
            using (Contexto context = new Contexto())
            {
                var guid = Guid.Parse(id);
                return context.Set<PartnerExterno>().Include("Tipo").FirstOrDefault(x => x.Id == guid);
            }
        }
    }
}