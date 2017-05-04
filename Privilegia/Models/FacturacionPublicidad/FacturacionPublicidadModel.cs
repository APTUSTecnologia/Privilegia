using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Privilegia.Models
{
    public class FacturacionPublicidadModel
    {
        [Required]
        public Guid Id { get; set; }

        public string IdFactura { get; set; }

        [Required]
        public string IdPartner { get; set; }

        public PartnerModel Partner { get; set; }

        [Required]
        public string IdPublicidad { get; set; }

        public PublicidadModel Publicidad { get; set; }

        public bool PlanDeMedios { get; set;  }

        [Required]
        public string Total { get; set; }

        public string FechaCreacion { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Concepto { get; set; }

        public string Estado { get; set; }

    }

}