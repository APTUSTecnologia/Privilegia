using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Privilegia.Models.PersonasDeContacto
{
    public class PersonaDeContactoRepository : BaseRepository<PersonaContactoModel> , IPersonaDeContactoRepository
    {
        public List<PersonaContactoModel> ObtenerTodasLasPersonasDeContacto()
        {
            using (Contexto context = new Contexto())
            {
                return context.PersonasDeContacto.OfType<PersonaContactoModel>().ToList();
            }
        }

        public List<PersonaContactoModel> ObetenerPersonasDeContactoPorIdPartner(string idPartner)
        {
            using (Contexto context = new Contexto())
            {
                return context.PersonasDeContacto.OfType<PersonaContactoModel>().Where(d => d.PartnerId == idPartner).ToList();
            }
        }

        public PersonaContactoModel ObetenerPersonaDeContactoPorId(string id)
        {
            using (Contexto context = new Contexto())
            {
                var guid = Guid.Parse(id);
                return context.Set<PersonaContactoModel>().FirstOrDefault(x => x.Id == guid);
            }
        }
    }
}