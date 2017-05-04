using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Privilegia.Models
{
    public class PersonaContactoModel
    {
        public Guid Id { get; set; }

        [Required]
        [RegularExpression("^((?!^Nombre$)[a-zA-Z_áéíóúñ\\s])+$", ErrorMessage = "El campo nombre no está escrito correctamente")]
        public string Nombre { get; set; }

        [Required]
        [RegularExpression("^((?!^Apellidos$)[a-zA-Z_áéíóúñ\\s])+$", ErrorMessage = "El campo apellidos no está escrito correctamente")]
        public string Apellidos { get; set; }

        [Required]
        [Phone]
        public string Telefono { get; set; }

        [Required]
        public string Cargo { get; set; }

        [Required]
        [Display (Name = "Es principal")]
        public Boolean Principal { get; set; }

        public string PartnerId { get; set; }
        public virtual PartnerInterno PartnerInterno { get; set; }

    }
}