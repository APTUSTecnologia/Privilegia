using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Privilegia.Models.FacturacionPublicidad;

namespace Privilegia.Models
{
    public class PublicidadModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Nombre del partner")]
        [Required]
        public string IdPartner { get; set; }

        public PartnerModel Partner { get; set; }

        [Required]
        public string Importe { get; set; }

        public string Descuento { get; set; }

        [Required]
        [Display(Name = "Plan de medios")]
        public bool PlanDeMedios { get; set; }

        [Required]
        public string Total { get; set; }

        [Required]
        [Display(Name = "Fecha de Inicio")]
        public string FechaInicio { get; set; }
       
        [Required]
        [Display(Name = "Fecha de Fin")]
        public string FechaFin { get; set; }

        [Required]
        [Display(Name = "Soporte")]
        public string NombreEspacioPublicidad { get; set; }

        [Required]
        [Display(Name = "Publicidad")]
        public string NombreParteEspacioPublicidad { get; set; }

        public string IdFactura { get; set; }

        public string FechaCreacion { get; set; }
        
        
    }
}