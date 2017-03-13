using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Privilegia.Models.Direcciones
{
    public class DireccionRepository : BaseRepository<DireccionModel>, IDireccionRepository
    {
        public List<DireccionModel> ObtenerTodasLasDirecciones()
        {
            using (Contexto context = new Contexto())
            {
                return context.Direcciones.OfType<DireccionModel>().ToList();
            }
        }

        public List<DireccionModel> ObtenerTodaslasDireccionesPorIdPartner(string idPartner)
        {
            using (Contexto context = new Contexto())
            {
                return context.Direcciones.OfType<DireccionModel>().Where(d => d.PartnerId == idPartner).ToList();
            }
        }

        public List<DireccionPrincipal> ObtenerTodasLasDireccionesPrincipales()
        {
            using (Contexto context = new Contexto())
            {
                return context.Direcciones.OfType<DireccionPrincipal>().ToList();
            }
        }

        public List<DireccionSecundaria> ObtenerTodaslasDireccionesSecundarias()
        {
            using (Contexto context = new Contexto())
            {
                return context.Direcciones.OfType<DireccionSecundaria>().ToList();
            }
        }

        public List<DireccionSecundaria> ObtenerTodaslasDireccionesSecundariasPorIdPartner(string idPartner)
        {
            using (Contexto context = new Contexto())
            {
                return context.Direcciones.OfType<DireccionSecundaria>().Where(d => d.PartnerId == idPartner).ToList();
            }
        }

        public DireccionSecundaria ObtenerDireccionSecundariaPorId(string id)
        {
            using (Contexto context = new Contexto())
            {
                var guid = Guid.Parse(id);
                return context.Set<DireccionSecundaria>().FirstOrDefault(x => x.Id == guid);
            }
        }

        public DireccionModel ObtenerDireccionPorId(string id)
        {
            using (Contexto context = new Contexto())
            {
                var guid = Guid.Parse(id);
                return context.Set<DireccionModel>().FirstOrDefault(x => x.Id == guid);
            }
        }

        public DireccionModel ObtenerDireccionPorIdPartner(string idPartner)
        {
            using (Contexto context = new Contexto())
            {
                return context.Set<DireccionModel>().FirstOrDefault(x => x.PartnerId == idPartner);
            }
        }
    }
}