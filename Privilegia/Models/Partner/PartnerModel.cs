using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Privilegia.Models.Tipos;

namespace Privilegia.Models
{
    public class PartnerModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        [StringLength(20)]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Cif")]
        [StringLength(9)]
        public string Cif { get; set; }

        public DireccionPrincipal DireccionPrincipal { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [Display(Name = "Fecha de acuerdo")]
        [Required]
        public string FechaAlta { get; set; }

        [Display(Name = "Fecha de baja")]
        public string FechaBaja { get; set; }

        public string FechaCreacion { get; set; }

    }

    public class PartnerInterno : PartnerModel
    {
        [Display(Name = "Actividad profesional")]
        public string ActividadProfesional { get; set; }

        //public DireccionSecundaria DireccionSecundaria { get; set; }
        public ICollection<PersonaContactoModel> PersonasDeContacto { get; set; }

        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "G.comisiones")]
        public Boolean Comisiones { get; set; }

        [Required]
        [Display(Name = "G.premios")]
        public Boolean Premios { get; set; }

        [Display(Name = "Observaciones")]
        [StringLength(100)]
        public string Observaciones { get; set; }

        public string Logo { get; set; }

    }

    public class PartnerExterno : PartnerModel
    {
        public string Tipo { get; set; }
    }
}