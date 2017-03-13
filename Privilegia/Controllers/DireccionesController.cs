using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Privilegia.Models;
using Privilegia.Models.Direcciones;
using Privilegia.Models.Partner;

namespace Privilegia.Controllers
{
    public class DireccionesController : Controller
    {
        private IDireccionRepository _direccionRepository;

        public DireccionesController()
        {
            _direccionRepository = new DireccionRepository();
        }
        public ActionResult Index(string idPartner)
        {
            ViewBag.PartnerId = idPartner;
            var addresses = _direccionRepository.ObtenerTodaslasDireccionesSecundariasPorIdPartner(idPartner);

            return PartialView("Index", addresses.ToList());
        }

        //[ChildActionOnly]
        //public ActionResult List(string id)
        //{
        //    ViewBag.PersonID = id;
        //    var addresses = _direccionRepository.ObtenerTodaslasDireccionesSecundariasPorId(id);

        //    return PartialView("List", addresses.ToList());
        //}

        public ActionResult CrearDireccionSecundaria(string idPartner)
        {
            DireccionSecundaria address = new DireccionSecundaria { PartnerId = idPartner};


            return PartialView(address);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearDireccionSecundaria([Bind(Include = "Id,Calle,Numero,CodigoPostal,Provincia,Municipio,PartnerId")] DireccionSecundaria direccion)
        {
            if (ModelState.IsValid)
            {
                direccion.Id = Guid.NewGuid();
               
                _direccionRepository.Insertar(direccion);

                string url = Url.Action("Index", "Direcciones", new { idPartner = direccion.PartnerId });
                return Json(new { success = true, url = url });
            }

            return PartialView(direccion);
        }

        public ActionResult EditarDireccionSecundaria(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var direccion = _direccionRepository.ObtenerDireccionSecundariaPorId(id);
            if (direccion == null)
            {
                return HttpNotFound();
            }

            return PartialView("EditarDireccionSecundaria", direccion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarDireccionSecundaria([Bind(Include = "Id,Calle,Numero,CodigoPostal,Provincia,Municipio,PartnerId")] DireccionSecundaria direccion)
        {
            if (ModelState.IsValid)
            {
                _direccionRepository.Actualizar(direccion);

                string url = Url.Action("Index", "Direcciones", new { idPartner = direccion.PartnerId });
                return Json(new { success = true, url = url });
            }

            return PartialView("EditarDireccionSecundaria", direccion);
        }

        public ActionResult BorrarDireccionSecundaria(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DireccionSecundaria direccion = _direccionRepository.ObtenerDireccionSecundariaPorId(id);
            if (direccion == null)
            {
                return HttpNotFound();
            }
            return PartialView("BorrarDireccionSecundaria", direccion);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult BorrarDireccionSecundariaConfirmado(string id)
        {
            DireccionSecundaria direccion = _direccionRepository.ObtenerDireccionSecundariaPorId(id);

            _direccionRepository.Eliminar(direccion);

            string url = Url.Action("Index", "Direcciones", new { idPartner = direccion.PartnerId });
            return Json(new { success = true, url = url });

        }
    }
}