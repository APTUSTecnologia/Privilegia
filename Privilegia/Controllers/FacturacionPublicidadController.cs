using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JQueryDataTables.Models;
using Privilegia.Models;
using Privilegia.Models.FacturacionPublicidad;
using Privilegia.Models.Partner;
using Privilegia.Models.Publicidad;

namespace Privilegia.Controllers
{
    public class FacturacionPublicidadController : Controller
    {
        private IPartnerRepository _partnerRepository;
        private IPublicidadRepository _publicidadRepository;
        private IFacturacionPublicidadRepository _facturacionPublicidadRepository;

        public FacturacionPublicidadController()
        {
            _partnerRepository = new PartnerRepository();
            _publicidadRepository = new PublicidadRepository();
            _facturacionPublicidadRepository = new FacturacionPublicidadRepository();

        }

        // GET: Facturacion
        public ActionResult Index()
        {
            var listaPartners = _partnerRepository.ObtenerPartnersInternos();

            ViewBag.listPartners = listaPartners.ToList().OrderBy(m => m.Nombre).Where(m => m.FechaBaja == null);

            return View();
        }

        public ActionResult CargarPublicidad(string idPartner, string mes)
        {

            var listaPublicidad = _publicidadRepository.ObtenerPublicidadPorIdPartner(idPartner);
            List<PublicidadModel> listafinal = new List<PublicidadModel>();

            if (listaPublicidad != null && listaPublicidad.Count > 0)
            {
                foreach (var producto in listaPublicidad)
                {
                    var partner = _partnerRepository.ObtenerPartnerPorId(producto.IdPartner);
                    producto.Partner = partner;
                }
            }

            foreach (var publi in listaPublicidad)
            {
                var prueba = DateTime.Parse(publi.FechaInicio).Month.ToString();
                if (prueba.Equals(mes))
                {
                    // Estamos en el mes correcto 
                    listafinal.Add(publi);

                }
            }

            var result = from c in listafinal
                         select
                         new[]
                         {
                    Convert.ToString(c.Id), c.Partner.Nombre, c.FechaInicio, c.FechaFin, c.NombreEspacioPublicidad,
                    c.NombreParteEspacioPublicidad, c.Importe
                         };

            return Json(new
            {
                iTotalRecords = listaPublicidad?.Count(),
                iTotalDisplayRecords = listafinal.Count(),
                aaData = result
            },
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExisteFactura(string idPartner, string mes)
        {
            var listaPublicidad = _publicidadRepository.ObtenerPublicidadPorIdPartner(idPartner);
            List<PublicidadModel> listafinal = new List<PublicidadModel>();
            string idFactura = "";
            var generada = false;
            foreach (var publi in listaPublicidad)
            {
                var prueba = DateTime.Parse(publi.FechaInicio).Month.ToString();
                if (prueba.Equals(mes))
                {
                    // Estamos en el mes correcto 
                    listafinal.Add(publi);
                    if (publi.IdFactura != null)
                    {
                        idFactura = publi.IdFactura;
                        //Ya tenemos factura asociada
                        generada = true;
                    }
                }
            }

            if (generada)
            {
                return Json(new { success = false, responseText = "Esta factura ya ha sido generada" , idFactura = idFactura},
                    JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = true, responseText = "Podemos crear factura" }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult CrearFactura(string idPartner, string mes)
        {
            var listaPublicidad = _publicidadRepository.ObtenerPublicidadPorIdPartner(idPartner);
            var listafinal = new List<PublicidadModel>();
            var existeFactura = false;
            string idFacturaExistente = null;
            foreach (var publi in listaPublicidad)
            {
                var mesFechaInicioPubliCtx = DateTime.Parse(publi.FechaInicio).Month.ToString();
                if (mesFechaInicioPubliCtx.Equals(mes))
                {
                    // Estamos en el mes correcto 
                    //Añadimos producto a la lista
                    listafinal.Add(publi);

                    if (publi.IdFactura != null)
                    {
                        idFacturaExistente = publi.IdFactura;
                        existeFactura = true;
                    }

                }
            }

            try
            {
                if (existeFactura)
                {
                    //Tenemos Factura la actualizamos
                    foreach (var publi in listafinal)
                    {
                        //Borramos los registros existentes 

                        var registro =
                            _facturacionPublicidadRepository.ObtenerFacturaPorIdPublicidad(publi.Id.ToString());

                        if (registro != null)
                        {
                             _facturacionPublicidadRepository.Eliminar(registro);
                        }

                        //nos recoremos la lista para ponerle el id de la factura a al objeto publicidad
                        publi.IdFactura = idFacturaExistente;
                        var registroFactura = new FacturacionPublicidadModel()
                        {
                            Id = Guid.NewGuid(),
                            IdFactura = publi.IdFactura,
                            IdPartner = idPartner,
                            FechaCreacion = DateTime.Today.ToLongDateString(),
                            IdPublicidad = publi.Id.ToString()
                        };

                        //Insertamos los registros de la factura 
                        _facturacionPublicidadRepository.Insertar(registroFactura);
                        //Actualizamos los registros de publicidad
                        _publicidadRepository.Actualizar(publi);

                    }
                    return Json(new { success = true, responseText = "Correcta Creacion" , idFactura = idFacturaExistente }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    //No tenemos Factura creamos una nueva
                    idFacturaExistente = Guid.NewGuid().ToString();

                    foreach (var publi in listafinal)
                    {
                        //nos recoremos la lista para ponerle el id de la factura a al objeto publicidad
                        publi.IdFactura = idFacturaExistente;
                        var registroFactura = new FacturacionPublicidadModel()
                        {
                            Id = Guid.NewGuid(),
                            IdFactura = publi.IdFactura,
                            IdPartner = idPartner,
                            FechaCreacion = DateTime.Today.ToLongDateString(),
                            IdPublicidad = publi.Id.ToString()
                        };

                        //Insertamos los registros de la factura 
                        _facturacionPublicidadRepository.Insertar(registroFactura);
                        //Actualizamos los registros de publicidad
                        _publicidadRepository.Actualizar(publi);

                    }
                    return Json(new { success = true, responseText = "Correcta Creacion" , idFactura= idFacturaExistente }, JsonRequestBehavior.AllowGet);

                }


               
            }
            catch (Exception)
            {
                return Json(new { success = false, responseText = "Error" },
                    JsonRequestBehavior.AllowGet);
                throw;
            }

        }
    }
}