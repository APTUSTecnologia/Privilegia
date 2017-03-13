using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Privilegia.Models;
using Privilegia.Models.Direcciones;
using Privilegia.Models.Partner;
using Privilegia.Models.Productos;
using Privilegia.Models.Publicidad;

namespace Privilegia.Controllers
{
    public class HomeController : Controller
    {
        private IPartnerRepository _partnerRepository;
        private IProductosRespository _productosRespository;
        private IPublicidadRepository _publicidadRepository;
        public HomeController()
        {
            _partnerRepository = new PartnerRepository();
            _productosRespository = new ProductosRespository();
            _publicidadRepository = new PublicidadRepository();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AjaxMethodPublicidad()
        {
            List<object> chartData = new List<object>();

            chartData.Add(new object[]
                            {
                            "Partner", "Count"
                            });

            var partners = _partnerRepository.ObtenerPartners();

            foreach (var partner in partners)
            {
                var count = _publicidadRepository.ObtenerPublicidadPorIdPartner(partner.Id.ToString()).Count;
                chartData.Add(new object[]
                       {
                            partner.Nombre, count
                       });

            }

            //{ new object[] { "Trickle", "Count" }, new object[] { "Ga", 50 }, new object[] { "Ga", 50 } };

            return Json(chartData);
        }
        [HttpPost]
        public JsonResult AjaxMethodProductos()
        {
            List<object> chartData = new List<object>();

            var partners = _partnerRepository.ObtenerPartnersInternos();
            chartData.Add(new object[]
                            {
                            "Partners","Productos"
                            });

            foreach (var partner in partners)
            {
                var count = _productosRespository.ObtenerTodosLosProductosPorIdPartner(partner.Id.ToString()).Count;
                chartData.Add(new object[]
                       {
                            partner.Nombre, count
                       });

            }

            //{ new object[] { "Trickle", "Count" }, new object[] { "Ga", 50 }, new object[] { "Ga", 50 } };

            return Json(chartData);
        }
        [HttpPost]
        public JsonResult AjaxMethodPartners()
        {
            List<object> chartData = new List<object>();

            chartData.Add(new object[]
                            {
                            "Tipo", "Count"
                            });

            var partners = _partnerRepository.ObtenerPartners();

            var countInterno = 0;
            var countExterno = 0;

            foreach (var partner in partners)
            {
                if (partner.GetType() == typeof(PartnerExterno))
                {
                    countExterno++;
                }
                else
                {
                    countInterno++;
                }
               

            }
            chartData.Add(new object[]
                      {
                            "Internos", countInterno
                      });
            chartData.Add(new object[]
                      {
                            "Externos", countExterno
                      });

            //{ new object[] { "Trickle", "Count" }, new object[] { "Ga", 50 }, new object[] { "Ga", 50 } };

            return Json(chartData);
        }
    }
}