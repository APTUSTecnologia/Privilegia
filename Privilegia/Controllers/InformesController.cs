using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Privilegia.Models;
using Privilegia.Models.Direcciones;
using Privilegia.Models.FacturacionPublicidad;
using Privilegia.Models.Partner;
using Privilegia.Models.Productos;
using Privilegia.Models.Publicidad;
using Privilegia.ViewModels;
using Rotativa;

namespace Privilegia.Controllers
{
    public class InformesController : Controller
    {
        private IPartnerRepository _partnerRepository;
        private IProductosRespository _productosRespository;
        private IPublicidadRepository _publicidadRepository;
        private IFacturacionPublicidadRepository _facturacionPublicidadRepository;
        private IDireccionRepository _direccionRepository;

        public InformesController()
        {
            _partnerRepository = new PartnerRepository();
            _productosRespository = new ProductosRespository();
            _publicidadRepository = new PublicidadRepository();
            _facturacionPublicidadRepository = new FacturacionPublicidadRepository();
            _direccionRepository = new DireccionRepository();
        }
        // GET: Informes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VerFactura(string id)
        {
            var temp = _facturacionPublicidadRepository.ObtenerFacturacion().First(m => m.IdFactura == id);
            var partner = _partnerRepository.ObtenerPartnerPorId(temp.IdPartner);

            partner.DireccionPrincipal =
                (DireccionPrincipal) _direccionRepository.ObtenerDireccionPorIdPartner(partner.Id.ToString());

            var modelo = new FacturaPublicidadViewModel()
            {
                Partner = partner,
                ListadePublicidad = new List<PublicidadModel>(),
                Fecha = temp.FechaCreacion
            };


            var registrosFactura = _facturacionPublicidadRepository.ObtenerFacturacion().Where(m => m.IdFactura == id);

            foreach (var registro in registrosFactura)
            {
                var publicidad = _publicidadRepository.ObtenerPublicidadPorId(registro.IdPublicidad);
                modelo.ListadePublicidad.Add(publicidad);

            }

            return new Rotativa.PartialViewAsPdf(modelo);
        }

        public ActionResult Publicidad()
        {
            var listaPartners = _partnerRepository.ObtenerPartners();

            ViewBag.listPartners = listaPartners.ToList().OrderBy(m => m.Nombre).Where(m => m.FechaBaja == null);

            return View();
        }

        public ActionResult CargarFacturas(string idPartner)
        {

            var listaFacturas = _facturacionPublicidadRepository.ObtenerFacturacionPorIdPartner(idPartner);

            if (listaFacturas.Count > 0)
            {
                foreach (var producto in listaFacturas)
                {
                    var partner = _partnerRepository.ObtenerPartnerPorId(producto.IdPartner);
                    producto.Partner = partner;
                }
            }

            var result = from c in listaFacturas
                         select new[] {
                    Convert.ToString(c.Id), c.Partner.Nombre, c.FechaCreacion, c.PlanDeMedios.ToString(), c.Total,
                    c.IdFactura
                         };

            return Json(new
            {
                iTotalRecords = listaFacturas?.Count(),
                iTotalDisplayRecords = listaFacturas.Count(),
                aaData = result
            },
                JsonRequestBehavior.AllowGet);
        }
    }
}