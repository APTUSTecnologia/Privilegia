using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Privilegia.Models.Partner
{
    public interface IPartnerRepository : IBaseRepository<PartnerModel>
    {
        List<PartnerModel> ObtenerPartners();

        List<PartnerInterno> ObtenerPartnersInternos();

        List<PartnerExterno> ObtenerPartnersExternos();

        PartnerModel ObtenerPartnerPorId(string id);

        bool ExistePartner(string cif);

    }

    

   
}