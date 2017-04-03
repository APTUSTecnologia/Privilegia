using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Privilegia.Models.FacturacionPartners;

namespace Privilegia.Models.Incidencias
{
    public class IncidenciaModel
    {
        public Guid Id { set; get; }

        public string FechaDeCreacion { get; set; }

        public string CifPartner { get; set; }

        public string NombreFichero { get; set; }

        public string Linea { get; set; }

        public string Descripcion { get; set; }

        public string CampoErroneo { get; set; }

    }
}