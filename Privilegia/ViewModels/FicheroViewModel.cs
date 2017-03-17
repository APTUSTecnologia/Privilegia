using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileHelpers;
using Privilegia.Models;

namespace Privilegia.ViewModels
{
    public class FicheroViewModel
    {
        public string ContenidoArchivo { get; set; }

        public List<ErrorInfo> Errores { get; set; }

        public int RegistrosCorrectos { get; set; }
    }
}