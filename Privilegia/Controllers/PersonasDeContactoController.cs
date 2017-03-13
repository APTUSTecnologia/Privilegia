using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Privilegia.Models;
using Privilegia.Models.PersonasDeContacto;

namespace Privilegia.Controllers
{
    public class PersonasDeContactoController : Controller
    {
        private IPersonaDeContactoRepository _personaDeContactoRepository;
        public PersonasDeContactoController()
        {
            _personaDeContactoRepository = new PersonaDeContactoRepository();
        }
        // GET: PersonasDeContacto
        public ActionResult Index(string idPartner)
        {
            ViewBag.PartnerId = idPartner;
            var personasDeContacto = _personaDeContactoRepository.ObetenerPersonasDeContactoPorIdPartner(idPartner);

            return PartialView("Index", personasDeContacto.ToList());
        }

        public ActionResult CrearPersonaDeContacto(string idPartner)
        {
            PersonaContactoModel address = new PersonaContactoModel { PartnerId = idPartner };


            return PartialView(address);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearPersonaDeContacto([Bind(Include = "Id,Nombre,Apellidos,Cargo,Telefono,Principal,PartnerId")] PersonaContactoModel persona)
        {
            if (ModelState.IsValid)
            {
                persona.Id = Guid.NewGuid();

                _personaDeContactoRepository.Insertar(persona);

                string url = Url.Action("Index", "PersonasDeContacto", new { idPartner = persona.PartnerId });
                return Json(new { success = true, url = url });
            }

            return PartialView(persona);
        }

        public ActionResult EditarPersonaDeContacto(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var persona = _personaDeContactoRepository.ObetenerPersonaDeContactoPorId(id);
            if (persona == null)
            {
                return HttpNotFound();
            }

            return PartialView("EditarPersonaDeContacto", persona);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPersonaDeContacto([Bind(Include = "Id,Nombre,Apellidos,Cargo,Telefono,Principal,PartnerId")] PersonaContactoModel persona)
        {
            if (ModelState.IsValid)
            {
                _personaDeContactoRepository.Actualizar(persona);

                string url = Url.Action("Index", "PersonasDeContacto", new { idPartner = persona.PartnerId });
                return Json(new { success = true, url = url });
            }

            return PartialView("EditarPersonaDeContacto", persona);
        }

        public ActionResult BorrarPersonaDeContacto(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonaContactoModel persona = _personaDeContactoRepository.ObetenerPersonaDeContactoPorId(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return PartialView("BorrarPersonaDeContacto", persona);
        }

        [HttpPost, ActionName("BorrarPersonaDeContacto")]
        [ValidateAntiForgeryToken]
        public ActionResult BorrarPersonaDeContactoConfirmado(string id)
        {
            PersonaContactoModel persona = _personaDeContactoRepository.ObetenerPersonaDeContactoPorId(id);

            _personaDeContactoRepository.Eliminar(persona);

            string url = Url.Action("Index", "PersonasDeContacto", new { idPartner = persona.PartnerId });
            return Json(new { success = true, url = url });

        }

    }
}