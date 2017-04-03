using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Privilegia.ViewModels
{
    public class InformePremiosPartnerViewModel
    {
        public Guid Id { get; set; }

        public string Cif { get; set; }

        public string Importe { get; set; }

        public string Fecha { get; set; }
    }
}