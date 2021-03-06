﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using JQueryDataTables.Models;
using Privilegia.Helpers;
using Privilegia.Models;
using Privilegia.Models.Partner;
using Privilegia.Models.Productos;
using Privilegia.Models.Publicidad;

namespace Privilegia.Controllers
{

    public class PublicidadController : Controller
    {
        private IPartnerRepository _partnerRepository;
        private IProductosRespository _productosRespository;
        private IPublicidadRepository _publicidadRepository;

        public PublicidadController()
        {
            _partnerRepository = new PartnerRepository();
            _productosRespository = new ProductosRespository();
            _publicidadRepository = new PublicidadRepository();

        }

        // GET: Publicidad
        public ActionResult Index()
        {

            var listaPartners = _partnerRepository.ObtenerPartnersInternos();

            ViewBag.listPartners = listaPartners.ToList().OrderBy(m => m.Nombre).Where(m => m.FechaBaja == null);


            return View();
        }

        // GET: Publicidad/Details/5
        public ActionResult Detalles(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var publicidad = _publicidadRepository.ObtenerPublicidadPorId(id);

            var partner = _partnerRepository.ObtenerPartnerPorId(publicidad.IdPartner);

            publicidad.Partner = partner;

            if (publicidad == null)
            {
                return HttpNotFound();
            }
            return View(publicidad);
        }

        // GET: Publicidad/Create
        [HttpGet]
        public ActionResult CreatePublicidad()
        {
            var publicidad = new PublicidadModel();

            var partners = _partnerRepository.ObtenerPartnersInternos().Where(m => m.FechaBaja == null);

            ViewBag.partners = new SelectList((from li in partners
                orderby li.Nombre
                select li).ToList(), "Id", "Nombre");

            var espacios = _publicidadRepository.ObtenerEspaciosDePublicidad();

            ViewBag.espacios = new SelectList((from li in espacios
                orderby li.Nombre
                select li).ToList(), "Nombre", "Nombre");

            return View(publicidad);
        }

        [HttpPost]
        public ActionResult CargarPartes(string espacio)
        {
            var idEspacio = _publicidadRepository.ObtenerEspaciosDePublicidad().First(m => m.Nombre == espacio).Id;

            try
            {
                var listaPartes =
                    _publicidadRepository.ObtenerPartesEspacioDePublicidadPorIdEspacio(idEspacio.ToString());

                return Json(new {lista = listaPartes.OrderBy(m => m.Nombre), success = true});
            }
            catch (Exception ex)
            {
                return Json(new {success = false});
                throw;
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePublicidad(PublicidadModel modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    modelo.Id = Guid.NewGuid();
                    modelo.FechaCreacion = DateTime.Today.ToLongDateString();
                    _publicidadRepository.Insertar(modelo);

                    return RedirectToAction("Index");
                }

                var partners = _partnerRepository.ObtenerPartnersInternos().Where(m => m.FechaBaja == null);

                ViewBag.partners = new SelectList((from li in partners
                    orderby li.Nombre
                    select li).ToList(), "Id", "Nombre");

                var espacios = _publicidadRepository.ObtenerEspaciosDePublicidad();

                ViewBag.espacios = new SelectList((from li in espacios
                    orderby li.Nombre
                    select li).ToList(), "Nombre", "Nombre");


                return View(modelo);
            }
            catch (Exception)
            {
                return View(modelo);
                throw;
            }
        }

        public ActionResult EditarPublicidad(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var publicidad = _publicidadRepository.ObtenerPublicidadPorId(id);

            var partner = _partnerRepository.ObtenerPartnerPorId(publicidad.IdPartner);
            publicidad.Partner = partner;

            var partners = _partnerRepository.ObtenerPartnersInternos().Where(m => m.FechaBaja == null);
            ViewBag.partners = new SelectList((from li in partners
                orderby li.Nombre
                select li).ToList(), "Id", "Nombre");

            var espacios = _publicidadRepository.ObtenerEspaciosDePublicidad();

            ViewBag.espacios = new SelectList((from li in espacios
                orderby li.Nombre
                select li).ToList(), "Nombre", "Nombre");

            if (publicidad == null)
            {
                return HttpNotFound();
            }

            return View(publicidad);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPublicidad(PublicidadModel modelo)
        {
            if (ModelState.IsValid)
            {
                _publicidadRepository.Actualizar(modelo);

                return RedirectToAction("Index");
            }
            return View(modelo);
        }


        public ActionResult CalendarioPublicidad()
        {
            return View();
        }


        public ActionResult GetEventosCalendario()
        {
            //var fromDate = ConvertFromUnixTimestamp(start);
            //var toDate = ConvertFromUnixTimestamp(end);

            //Get the events
            //You may get from the repository also
            var eventList = GetListaCalendarioPublicidad();

            var rows = eventList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        public List<TimeTableModel> GetListaCalendarioPublicidad()
        {
            try
            {
                var listaPublicidad = _publicidadRepository.ObtenerTodaLaPublicidad();
                var listaPartners = _partnerRepository.ObtenerPartnersInternos().Where(m => m.FechaBaja == null).ToList();

                List<TimeTableModel> myList = new List<TimeTableModel>();
                foreach (var publicidad in listaPublicidad)
                {
                    var partner = listaPartners.First(m => m.Id == Guid.Parse(publicidad.IdPartner));
                    var id = publicidad.Id.ToString();
                    var title = partner.Nombre + "\n" + publicidad.NombreEspacioPublicidad + "-->" + publicidad.NombreParteEspacioPublicidad;
                    var dateStart = publicidad.FechaInicio;
                    var dateEnd = publicidad.FechaFin;
                    var color = "";
                    var descripcion = "<ul><li>Partner : " + partner.Nombre + "</li>" +
                                      "<li>Espacio: " + publicidad.NombreEspacioPublicidad + "</li>" +
                                      "<li>Parte: " + publicidad.NombreParteEspacioPublicidad + "</li>" +
                                      "<li>Fecha de Inicio: " + dateStart + "</li>" +
                                      "<li>Fecha de Fin: " + dateEnd + "</li></ul>";



                    //Convert Implicity typed var to Date Time
                    DateTime realStartDate = Convert.ToDateTime(dateStart);
                    DateTime realEndDate = Convert.ToDateTime(dateEnd).AddDays(1);


                    //Convert Date Time to ISO
                    String sendStartDate = realStartDate.ToString("s");
                    String sendEndDate = realEndDate.ToString("s");

                    switch (publicidad.NombreEspacioPublicidad.ToLower())
                    {
                        case "revista":
                            color = "#27a0c9";//Azul
                            break;
                        case "web":
                            color = "#23ad00";//Verde
                            break;
                        case "boletin oficial":
                            color = "#ff5413";//Naranja
                            break;
                        default:
                            color = "#d22323";//rojo oscuro
                            break;
                    }

                    myList.Add(new TimeTableModel(id,title, sendStartDate, sendEndDate, color, descripcion));
 
                }
              
                return myList;
            }
            catch (Exception)
            {
                return new List<TimeTableModel>();
                throw;
            }
        }
        private static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        public ActionResult CargarPublicidad(JQueryDataTableParamModel param)
        {
            var listPublicidad = _publicidadRepository.ObtenerTodaLaPublicidad();

            if (listPublicidad != null && listPublicidad.Count > 0)
            {
                foreach (var producto in listPublicidad)
                {
                    var partner = _partnerRepository.ObtenerPartnerPorId(producto.IdPartner);
                    producto.Partner = partner;
                }
            }

            IEnumerable<PublicidadModel> filteredCompanies;
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                //Optionally check whether the columns are searchable at all 
                var isNombreEmpresaSearchable = Convert.ToBoolean(Request["bSearchable_1"]);
                var isEspacioPublicidadSearchable = Convert.ToBoolean(Request["bSearchable_4"]);
                var isParteEspacioPublicidadSearchable = Convert.ToBoolean(Request["bSearchable_5"]);

                filteredCompanies = listPublicidad?.ToList()
                   .Where(c => isNombreEmpresaSearchable && c.Partner.Nombre.ToLower().Contains(param.sSearch.ToLower())
                               ||
                               isEspacioPublicidadSearchable && c.NombreEspacioPublicidad.ToLower().Contains(param.sSearch.ToLower())
                               ||
                               isParteEspacioPublicidadSearchable && c.NombreParteEspacioPublicidad.ToLower().Contains(param.sSearch.ToLower()));
            }
            else
            {
                //Used if particulare columns are filtered 
                var nombreEmpresaFilter = Convert.ToString(Request["sSearch_1"]);
                var espacioPublicidadFilter = Convert.ToString(Request["sSearch_4"]);
                var parteEspcioPublicidadFilter = Convert.ToString(Request["sSearch_5"]);

                filteredCompanies = listPublicidad?.ToList()
                                    .Where(c => (nombreEmpresaFilter == "" || c.Partner.Nombre.ToLower().Contains(nombreEmpresaFilter.ToLower()))
                                                &&
                                                (espacioPublicidadFilter == "" || c.NombreEspacioPublicidad.ToLower().Contains(espacioPublicidadFilter.ToLower()))
                                                &&
                                                (parteEspcioPublicidadFilter == "" || c.NombreParteEspacioPublicidad.ToLower().Contains(parteEspcioPublicidadFilter.ToLower()))
                                               );

            }

            var isNombreEmpresaSortable = Convert.ToBoolean(Request["bSortable_1"]);
            var isEspacioPublicidadSortable = Convert.ToBoolean(Request["bSortable_4"]);
            var isParteEspacioPublicidadSortable = Convert.ToBoolean(Request["bSortable_5"]);
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<PublicidadModel, string> orderingFunction = (c => sortColumnIndex == 1 && isNombreEmpresaSortable ? c.Partner.Nombre :
                                                          sortColumnIndex == 2 && isEspacioPublicidadSortable ? c.NombreEspacioPublicidad :
                                                          sortColumnIndex == 3 && isParteEspacioPublicidadSortable ? c.NombreParteEspacioPublicidad :
                                                          "");

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredCompanies = filteredCompanies.OrderBy(orderingFunction);
            else
                filteredCompanies = filteredCompanies.OrderByDescending(orderingFunction);

            var displayedCompanies = filteredCompanies.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = from c in displayedCompanies select new[] { Convert.ToString(c.Id), c.Partner.Nombre, c.FechaInicio, c.FechaFin , c.NombreEspacioPublicidad, c.NombreParteEspacioPublicidad, c.Importe , c.IdFactura};
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = listPublicidad?.Count(),
                iTotalDisplayRecords = filteredCompanies.Count(),
                aaData = result
            },
                        JsonRequestBehavior.AllowGet);
        }

    }
}