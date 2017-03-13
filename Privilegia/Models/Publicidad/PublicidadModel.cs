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
        
        [Required]
        [Display(Name = "Fecha de Inicio")]
        public string FechaInicio { get; set; }
       
        [Required]
        [Display(Name = "Fecha de Fin")]
        public string FechaFin { get; set; }

        [Required]
        [Display(Name = "Espacio")]
        public string NombreEspacioPublicidad { get; set; }

        [Required]
        [Display(Name = "Lugar")]
        public string NombreParteEspacioPublicidad { get; set; }

        public string IdFactura { get; set; }

        public string FechaCreacion { get; set; }
        
        
    }
}