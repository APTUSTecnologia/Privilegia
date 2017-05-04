using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Privilegia.Models
{
    public class FacturasPremiosModel
    {
        [Required]
        public Guid Id { get; set; }

        public string FechaDeCreacion { get; set; }

        public string Estado { get; set; }

        public virtual PartnerModel Partner { get; set; }

        [Required]
        public string IdPartner { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Concepto { get; set; }

        [Required]
        public string Total { get; set; }

    }
}