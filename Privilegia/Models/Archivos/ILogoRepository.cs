using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Privilegia.Models.Archivos
{
    public interface ILogoRepository : IBaseRepository<Logo>
    {
        List<Logo> ObtenerTodosLosLogos();

        Logo ObtenerLogoPorId(string id);

        Logo ObtenerLogoPorIdPartner(string idPartner);
    }
}