using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JQueryDataTables.Models;
using Privilegia.Models;
using Privilegia.Models.Partner;
using Privilegia.Models.Productos;

namespace Privilegia.Controllers
{
    public class ProductosController : Controller
    {
        private IPartnerRepository _partnerRepository;
        private IProductosRespository _productosRespository;
        public ProductosController()
        {
            _partnerRepository = new PartnerRepository();
            _productosRespository = new ProductosRespository();
        }
        // GET: Productos
        public ActionResult Index()
        {
            var listaPartners = _partnerRepository.ObtenerPartnersInternos();

            ViewBag.listPartners = listaPartners.ToList().OrderBy(m => m.Nombre).Where(m => m.FechaBaja == null);

            return View();
        }
        // GET: Productos/Details/5
        public ActionResult Detalles(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var producto = _productosRespository.ObtnerProductoPorIdProducto(id);

            var partner = _partnerRepository.ObtenerPartnerPorId(producto.IdPartner);

            producto.Partner = partner;

            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Productos/Create
        [HttpGet]
        public ActionResult CreateProducto()
        {
            var producto = new ProductoModel();

            var partners = _partnerRepository.ObtenerPartnersInternos().Where(m => m.FechaBaja == null); 

            ViewBag.partners = new SelectList((from li in partners
                                               orderby li.Nombre
                                               select li).ToList(), "Id", "Nombre");

            return View(producto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProducto(ProductoModel modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    modelo.Id = Guid.NewGuid();
                    modelo.FechaCreacion = DateTime.Today.ToShortDateString();
                    _productosRespository.Insertar(modelo);

                    return RedirectToAction("Index");
                }

                var partners = _partnerRepository.ObtenerPartnersInternos();

                ViewBag.partners = new SelectList((from li in partners
                                                   orderby li.Nombre
                                                   select li).ToList(), "Id", "Nombre");

                return View(modelo);
            }
            catch (Exception ex)
            {
                return View(modelo);
                throw;
            }
        }


        public ActionResult EditarProducto(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

           
            var producto = _productosRespository.ObtnerProductoPorIdProducto(id);
            var partner = _partnerRepository.ObtenerPartnerPorId(producto.IdPartner);
            producto.Partner = partner;

            var partners = _partnerRepository.ObtenerPartnersInternos();

            ViewBag.partners = new SelectList((from li in partners
                                               orderby li.Nombre
                                               select li).ToList(), "Id", "Nombre");

            if (producto == null)
            {
                return HttpNotFound();
            }

            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarProducto(ProductoModel producto)
        {
            if (ModelState.IsValid)
            {
                _productosRespository.Actualizar(producto);

                return RedirectToAction("Index");
            }

            var partners = _partnerRepository.ObtenerPartnersInternos();

            ViewBag.partners = new SelectList((from li in partners
                                               orderby li.Nombre
                                               select li).ToList(), "Id", "Nombre");
            return View(producto);
        }


        // GET: People/Delete/5
        public ActionResult EliminarProducto(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductoModel producto = _productosRespository.ObtnerProductoPorIdProducto(id);

            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: People/Delete/5
        [ValidateAntiForgeryToken]
        public ActionResult EliminarProductoConfirmed(string id)
        {
            var producto = _productosRespository.ObtnerProductoPorIdProducto(id);
            
            if (producto != null)
            {
                producto.FechaBaja = DateTime.Today.ToShortDateString();
                producto.Estado = "Baja";
                _productosRespository.Actualizar(producto);
            }


            return RedirectToAction("Index");
        }

        public ActionResult CargarProductos(JQueryDataTableParamModel param)
        {
            var listproductos = _productosRespository.ObtenerTodosLosProductos();

            if (listproductos != null && listproductos.Count > 0)
            {
                foreach (var producto in listproductos)
                {
                    var partner = _partnerRepository.ObtenerPartnerPorId(producto.IdPartner);
                    producto.Partner = partner;
                }
            }

            IEnumerable<ProductoModel> filteredCompanies;
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                //Optionally check whether the columns are searchable at all 
                var isNombreEmpresaSearchable = Convert.ToBoolean(Request["bSearchable_1"]);
                var isNombreSearchable = Convert.ToBoolean(Request["bSearchable_2"]);
                var isEstadoSearchable = Convert.ToBoolean(Request["bSearchable_3"]);

                filteredCompanies = listproductos?.ToList()
                   .Where(c => isNombreEmpresaSearchable && c.Partner.Nombre.ToLower().Contains(param.sSearch.ToLower())
                               ||
                               isNombreSearchable && c.Codigo.ToLower().Contains(param.sSearch.ToLower())
                               ||
                               isEstadoSearchable && c.Estado.ToLower().Contains(param.sSearch.ToLower()));
            }
            else
            {
                //Used if particulare columns are filtered 
                var nombreEmpresaFilter = Convert.ToString(Request["sSearch_1"]);
                var nombreFilter = Convert.ToString(Request["sSearch_2"]);
                var estadoFilter = Convert.ToString(Request["sSearch_3"]);

                filteredCompanies = listproductos?.ToList()
                                    .Where(c => (nombreEmpresaFilter == "" || c.Partner.Nombre.ToLower().Contains(nombreEmpresaFilter.ToLower()))
                                                &&
                                                (nombreFilter == "" || c.Codigo.ToLower().Contains(nombreFilter.ToLower()))
                                                &&
                                                (estadoFilter == "" || c.Estado.ToLower().Contains(estadoFilter.ToLower()))
                                               );

            }

            var isNombreEmpresaSortable = Convert.ToBoolean(Request["bSortable_1"]);
            var isNombreSortable = Convert.ToBoolean(Request["bSortable_2"]);
            var isEstadoSortable = Convert.ToBoolean(Request["bSortable_3"]);
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<ProductoModel, string> orderingFunction = (c => sortColumnIndex == 1 && isNombreEmpresaSortable ? c.Partner.Nombre :
                                                          sortColumnIndex == 2 && isNombreSortable ? c.Codigo :
                                                          sortColumnIndex == 3 && isEstadoSortable ? c.Estado :
                                                          "");

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredCompanies = filteredCompanies.OrderBy(orderingFunction);
            else
                filteredCompanies = filteredCompanies.OrderByDescending(orderingFunction);

            var displayedCompanies = filteredCompanies.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = from c in displayedCompanies select new[] { Convert.ToString(c.Id), c.Partner.Nombre, c.Codigo, c.Estado };
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = listproductos?.Count(),
                iTotalDisplayRecords = filteredCompanies.Count(),
                aaData = result
            },
                        JsonRequestBehavior.AllowGet);
        }
    }
}