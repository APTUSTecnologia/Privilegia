using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileHelpers;
using Privilegia.Models;
using Privilegia.Models.FacturacionPartners;
using Privilegia.Models.Incidencias;

namespace Privilegia.ViewModels
{
    public class FicheroViewModel
    {
        //public string ContenidoArchivo { get; set; }
        public List<IncidenciaModel> Incidencias { get; set; }

        public List<FacturacionPremiosModel> Cargados { get; set; }

        public List<FacturacionPremiosModel> ListaRegistrosConErrores { get; set; }

        public int RegistrosCorrectos { get; set; }

        public PartnerModel Partner { get; set; }

        public string JsonCorrectos { get; set; }
    }
}