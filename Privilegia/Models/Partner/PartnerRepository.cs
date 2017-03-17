using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Privilegia.Models;
using Privilegia.Models.Partner;

namespace Privilegia.Models.Partner
{
    public class PartnerRepository : BaseRepository<PartnerModel>, IPartnerRepository
    {
        public List<PartnerModel> ObtenerPartners()
        {
            using (Contexto context = new Contexto())
            {

                return context.Partners.OfType<PartnerModel>().ToList();
            }
        }

        public List<PartnerInterno> ObtenerPartnersInternos()
        {
            using (Contexto context = new Contexto())
            {
                return context.Partners.Include("DireccionPrincipal").OfType<PartnerInterno>().ToList();
            }
        }

        public List<PartnerExterno> ObtenerPartnersExternos()
        {
            using (Contexto context = new Contexto())
            {
                return context.Partners.OfType<PartnerExterno>().ToList();
            }
        }

        public PartnerModel ObtenerPartnerPorId(string id)
        {
            using (Contexto context = new Contexto())
            {
                var guid = Guid.Parse(id);
                return context.Set<PartnerModel>().FirstOrDefault(x => x.Id == guid);
            }
        }

        public bool ExistePartner(string cif)
        {
            using (Contexto context = new Contexto())
            {
                var partner = context.Set<PartnerModel>().FirstOrDefault(x => x.Cif == cif);

                return partner != null;
            }
        }
    }
}