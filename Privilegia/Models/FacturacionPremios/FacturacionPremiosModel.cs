using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Privilegia.Models.FacturacionPartners
{
    public class FacturacionPremiosModel
    {
        [Key]
        [Column(Order = 1)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 2)]
        public string NifProveed { get; set; }

        [Key]
        [Column(Order = 3)]
        public string CodigoPcto { get; set; }

        [Key]
        [Column(Order = 4)]
        public string CodigoCliente { get; set; }

        public string NumeroPedido { get; set; }

        public string Descripcion { get; set; }

        public string FechaContratacion { get; set; }

        public string FechaBaja { get; set; }

        public string FechaDeAnulacion { get; set; }

        public string Situacion { get; set; }

        public string Observaciones { get; set; }

        [Key]
        [Column(Order = 5)]
        public string FechaPago { get; set; }

        public string ImportePago { get; set; }

        public string SituacionPago { get; set; }

        public string ComisionPago { get; set; }

        public string NumeroMutualista { get; set; }

        public string FechaDeCreacion { get; set; }
    }
}