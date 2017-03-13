using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Privilegia.Models.Publicidad
{
    public class PublicidadRepository : BaseRepository<PublicidadModel>, IPublicidadRepository
    {
        public List<PublicidadModel> ObtenerTodaLaPublicidad()
        {
            using (Contexto context = new Contexto())
            {
                return context.Publicidad.OfType<PublicidadModel>().ToList();
            }
        }
        public PublicidadModel ObtenerPublicidadPorId(string id)
        {
            using (Contexto context = new Contexto())
            {
                var guid = Guid.Parse(id);
                return context.Set<PublicidadModel>().FirstOrDefault(x => x.Id == guid);
            }
        }

        public List<EspacioPublicidadModel> ObtenerEspaciosDePublicidad()
        {
            using (Contexto context = new Contexto())
            {
                return context.EspaciosPublicidad.OfType<EspacioPublicidadModel>().ToList();
            }
        }

        public List<ParteEspacioPublicidadModel> ObtenerPartesEspacioDePublicidadPorIdEspacio(string idEspacio)
        {
            using (Contexto context = new Contexto())
            {
                return context.PartesEspaciosPublicidad.OfType<ParteEspacioPublicidadModel>().Where(d => d.IdEspacioPublicidad == idEspacio).ToList();
            }
        }

        public List<PublicidadModel> ObtenerPublicidadPorIdPartner(string idPartner)
        {
            using (Contexto context = new Contexto())
            {
                return context.Publicidad.OfType<PublicidadModel>().Where(d => d.IdPartner == idPartner).ToList();
            }
        }

        public PublicidadModel ObtenerPublicidadPorIdEspacio(string idEspacio)
        {
            throw new NotImplementedException();
        }


        public PublicidadModel ObtenerPublicidadPorIdParteEspacio(string idEspacio)
        {
            throw new NotImplementedException();
        }

       
    }
}