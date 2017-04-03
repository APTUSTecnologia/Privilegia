using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Privilegia.Models
{
    public class FacturasPremiosModel
    {
        public Guid Id { get; set; }

        public string FechaDeCreacion { get; set; }

        public string Estado { get; set; }

        public virtual PartnerModel Partner { get; set; }

        public string IdPartner { get; set; }

    }
}