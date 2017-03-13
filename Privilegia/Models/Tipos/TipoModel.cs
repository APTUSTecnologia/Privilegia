using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Privilegia.Models.Tipos
{
    public class TipoModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Tipo")]
        public string Nombre { get; set; }

    }
}