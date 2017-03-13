using System;
using System.ComponentModel.DataAnnotations;

namespace Privilegia.Models.Productos
{
    public class ProductoModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Fecha de alta")]
        public string FechaAlta { get; set; }

        [Display(Name = "Fecha de baja")]
        public string FechaBaja { get; set; }

        public string Descripcion { get; set; }

        [Required]
        public string Estado { get; set; }

        [Required]
        [Display(Name = "Codigo de producto")]
        public string Codigo { get; set; }

        [Required]
        [Display(Name = "Valor del premio")]
        public float PremioValor { get; set; }

        [Required]
        [StringLength(1)]
        [Display(Name = "Tipo de premio")]
        public string TipoPremio { get; set; }

        [Display(Name = "Tipo de Comision Indirecta")]
        [StringLength(1)]
        public string TipoComisionIndirecta { get; set; }

        [Display(Name = "Valor de la comision")]
        public float ComisionValor { get; set; }

        [Display(Name = "% para la Mutualidad")]
        public float ImporteMutualidad { get; set; }

        [Display(Name = "% para el mutualista")]
        public float ImporteMutualista { get; set; }

        [Display(Name = "Tipo de comisión")]
        [StringLength(1)]
        [Required]
        public string TipoComision { get; set; }

        [Required]
        [Display(Name = "Partner")]
        public string IdPartner { get; set; }

        public virtual PartnerModel Partner { get; set; }

        public string FechaCreacion { get; set; }

    }
}