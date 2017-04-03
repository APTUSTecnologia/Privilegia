using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Privilegia.Models.FacturasPremios;
using Privilegia.Models.Incidencias;

namespace Privilegia.Models
{
    public class FacturacionPremiosModel
    {
        [Key]
        [Column(Order = 1)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 2)]
        //CIF DEL PARTNER. SIEMPRE EL MISMO	
        [Required]
        [Display(Description = "Nif Proveed")]
        [MaxLength(10)]
        public string NifProveed { get; set; }

        [Key]
        [Column(Order = 3)]
        [Required]
        [MaxLength(10)]
        [Display(Description = "Codigo Pcto")]
        //LO CUMPLIMENTA LA MUTUALIDAD. CODIGO DE UN PRODUCTO PUEDE SER FICTICIO EL MISMO PARA TODOS "PRODUCTO01"								
        public string CodigoPcto { get; set; }

        [Key]
        [Column(Order = 4)]
        [Required]
        [MaxLength(20)]
        [Display(Description = "Codigo Cliente")]
        //NIF CLIENTE								
        public string CodigoCliente { get; set; }

        //NUMERO DE PEDIDO QUE SE GENERA CADA VEZ QUE EL CLIENTE COMPRA	
        [Required]
        [MaxLength(20)]
        [Display(Description = "NumeroPedido")]
        public string NumeroPedido { get; set; }

        //NOMBRE DEL MUTUALISTA. NO OBLIGATORIO, PERO RECOMENDABLE PARA MAYOR SEGURIDAD.	
        [Required]
        [MaxLength(80)]
        [Display(Description = "Descripcion")]
        public string Descripcion { get; set; }

        //VALOR TOTAL DE LA COMPRA SIN IVA		
        [Required]
        [MaxLength(8)]
        [RegularExpression(@"^[\d,]+(\.\d{1,5})?$", ErrorMessage = "formato incorrecto")]
        [Display(Description = "Valor")]
        public string Valor { get; set; }

        //FECHA COMPRA DEL PEDIDO		
        [Required]
        [MaxLength(10)]
        [Display(Description = "Fecha Contratacion")]
        public string FechaContratacion { get; set; }

        [MaxLength(8)]
        public string FechaBaja { get; set; }

        [MaxLength(8)]
        public string FechaDeAnulacion { get; set; }

        //SIEMPRE TENDRA VALOR "1"		
        [Required]
        [MaxLength(1)]
        [Display(Description = "Situacion")]
        public string Situacion { get; set; }

        [MaxLength(150)]
        public string Observaciones { get; set; }

        [Key]
        [Column(Order = 5)]
        [Required]
        [MaxLength(10)]
        [Display(Description = "Fecha Pago")]
        //FECHA COMPRA DEL PEDIDO/// FECHA GENERACION DEL FICHERO								
        public string FechaPago { get; set; }

        [Required]
        [MaxLength(8)]
        [Display(Description = "Importe Pago")]
        [RegularExpression(@"^[\d,]+(\.\d{1,5})?$", ErrorMessage = "formato incorrecto")]
        //VALOR TOTAL DE LA COMPRA SIN IVA								
        public string ImportePago { get; set; }

        [Required]
        [MaxLength(1)]
        [Display(Description = "Situacion Pago")]
        //SIEMPRE TENDRA VALOR "2"								
        public string SituacionPago { get; set; }

        [Required]
        [MaxLength(8)]
        [Display(Description = "Comision Pago")]
        [RegularExpression(@"^[\d,]+(\.\d{1,5})?$", ErrorMessage = "formato incorrecto")]
        //PREMIO PRIVILEGIA A ABONAR A LOS MUTUALISTAS								
        public string ComisionPago { get; set; }

        public string IdIncidencia { get; set; }
        
        [Required]
        [MaxLength(8)]
        [Display(Description = "Numero Mutualista")]
        //NÚMERO DEL MUTUALISTA								
        public string NumeroMutualista { get; set; }

        public int Temp { get; set;  } // 1 = temp / 0 = no temp

        public string IdFactura { get; set; }

        public string FechaDeCreacion { get; set; }
    }
}