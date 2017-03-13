using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Privilegia.Models;
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

        public InformesController()
        {
            _partnerRepository = new PartnerRepository();
            _productosRespository = new ProductosRespository();
            _publicidadRepository = new PublicidadRepository();
            _facturacionPublicidadRepository = new FacturacionPublicidadRepository();
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
    }
}