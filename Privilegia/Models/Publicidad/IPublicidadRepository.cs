using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Privilegia.Models.Publicidad
{
    public interface IPublicidadRepository : IBaseRepository<PublicidadModel>
    {
        List<PublicidadModel> ObtenerTodaLaPublicidad();

        PublicidadModel ObtenerPublicidadPorIdEspacio(string idEspacio);

        List<PublicidadModel> ObtenerPublicidadPorIdPartner(string idPartner);

        PublicidadModel ObtenerPublicidadPorIdParteEspacio(string idEspacio);

        PublicidadModel ObtenerPublicidadPorId(string id);

        List<EspacioPublicidadModel> ObtenerEspaciosDePublicidad();

        List<ParteEspacioPublicidadModel> ObtenerPartesEspacioDePublicidadPorIdEspacio(string idEspacio);

        List<ParteEspacioPublicidadModel> ObtenerPartesEspacioDePublicidad();
    }
}