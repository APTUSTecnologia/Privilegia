using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Privilegia.Models;

namespace Privilegia.ViewModels
{
    public class FacturaPublicidadViewModel
    {
       
        public List<PublicidadModel> ListadePublicidad { get; set; }

        public PartnerModel Partner { get; set; }


        public string Fecha { get; set; }

    }
}