using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Privilegia.Models;

namespace Privilegia.ViewModels
{
    public class FacturaPremiosViewModel
    {
        public List<FacturacionPremiosModel> Lista { get; set; }

        public PartnerModel Partner { get; set; }

        public FacturasPremiosModel Factura { get; set; }

        public string Fecha { get; set; }
    }
}