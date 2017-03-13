using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Privilegia.Models
{
    public class DireccionModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Calle { get; set; }
        [Required]
        [StringLength(4, MinimumLength = 1)]
        public string Numero { get; set; }
        [Required]
        [StringLength(5)]
        [Display(Name = "C.P.")]
        public string CodigoPostal { get; set; }
        [Required]
        [StringLength(50)]
        public string Municipio { get; set; }
        [Required]
        [StringLength(50)]
        public string Provincia { get; set; }

        public string PartnerId { get; set; }

    }

    public class DireccionPrincipal : DireccionModel
    {
       
    }

    public class DireccionSecundaria : DireccionModel
    {

    }
}