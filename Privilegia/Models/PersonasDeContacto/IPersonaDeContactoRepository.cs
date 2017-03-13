using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Privilegia.Models.PersonasDeContacto
{
    public interface IPersonaDeContactoRepository :IBaseRepository<PersonaContactoModel>
    {
        List<PersonaContactoModel> ObtenerTodasLasPersonasDeContacto();

        List<PersonaContactoModel> ObetenerPersonasDeContactoPorIdPartner(string idPartner);

        PersonaContactoModel ObetenerPersonaDeContactoPorId(string id);
    }
}