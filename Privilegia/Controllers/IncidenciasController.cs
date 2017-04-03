using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using JQueryDataTables.Models;
using Privilegia.Models.Incidencias;
using Privilegia.Models.Partner;
using Privilegia.Models.Productos;

namespace Privilegia.Controllers
{
    public class IncidenciasController : Controller
    {
        private IPartnerRepository _partnerRepository;
        private IIncidenciaRepository _incidenciaRepository;
        public IncidenciasController()
        {
            _partnerRepository = new PartnerRepository();
            _incidenciaRepository = new IncidenciasRepository();

        }
        // GET: Incidencias
        public ActionResult Index()
        {
            var listaPartners = _partnerRepository.ObtenerPartnersInternos();

            ViewBag.listPartners = listaPartners.ToList().OrderBy(m => m.Nombre).Where(m => m.FechaBaja == null);

            return View();
        }
        public ActionResult CargarIncidencias(JQueryDataTableParamModel param)
        {
            var listIncidencias = _incidenciaRepository.ObtenerTodasLasIncidencias();
            IEnumerable<IncidenciaModel> filteredCompanies;
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                //Optionally check whether the columns are searchable at all 
                var isCifEmpresaSearchable = Convert.ToBoolean(Request["bSearchable_1"]);



                filteredCompanies = listIncidencias?.ToList()
                   .Where(c => isCifEmpresaSearchable && c.CifPartner.ToLower().Contains(param.sSearch.ToLower()));
            }
            else
            {
                //Used if particulare columns are filtered 
                var cifEmpresaFilter = Convert.ToString(Request["sSearch_1"]);
                var datefilter = Convert.ToString(Request["sSearch_2"]);

                if (!cifEmpresaFilter.IsEmpty())
                {
                    cifEmpresaFilter = _partnerRepository.ObtenerPartners().First(m => m.Nombre == cifEmpresaFilter).Cif;
                }
                if (!datefilter.IsEmpty())
                    datefilter = datefilter.Replace("-", "/");

                filteredCompanies = listIncidencias?.ToList()
                                    .Where(c => (cifEmpresaFilter == "" || c.CifPartner.ToLower().Contains(cifEmpresaFilter.ToLower()))
                                     &&
                                     (datefilter == "" || c.FechaDeCreacion.Contains(datefilter)));

            }


            var displayedCompanies = filteredCompanies.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = from c in displayedCompanies select new[] { Convert.ToString(c.Id), c.CifPartner,c.FechaDeCreacion, c.Linea, c.Descripcion };
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = listIncidencias?.Count(),
                iTotalDisplayRecords = filteredCompanies.Count(),
                aaData = result
            },
                        JsonRequestBehavior.AllowGet);
        }
    }
}