using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using DevExtreme.AspNet.Data;
using JQueryDataTables.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Privilegia.Models;
using Privilegia.Models.Archivos;
using Privilegia.Models.Direcciones;
using Privilegia.Models.Partner;
using Privilegia.Models.PersonasDeContacto;
using Privilegia.Models.Productos;
using Privilegia.Models.Tipos;
using Privilegia.ViewModels;
using WebGrease.Css.Ast.Selectors;


namespace Privilegia.Controllers
{
    public class PartnersController : Controller
    {
        private IPartnerRepository _partnerRepository;
        private IDireccionRepository _direccionRepository;
        private IPersonaDeContactoRepository _personaDeContactoRepository;
        private ITiposRepository _tiposRepository;
        private ILogoRepository _logoRepository;

        public PartnersController()
        {
            _partnerRepository = new PartnerRepository();
            _direccionRepository = new DireccionRepository();
            _personaDeContactoRepository = new PersonaDeContactoRepository();
            _tiposRepository = new TiposRepository();
            _logoRepository = new LogoRepository();

        }

        // GET: Partners
        public ActionResult Index()
        {
            return View();
        }

        // GET: Productos/Details/5
        public ActionResult Detalles(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var partner = _partnerRepository.ObtenerPartnerPorId(id);

            if (partner == null)
            {
                return HttpNotFound();
            }

            partner.Logo = _logoRepository.ObtenerLogoPorIdPartner(id);

            return View(partner);
        }

        public JsonResult DeleteInterno(int id)
        {
            try
            {
                return Json(new {success = true});
            }
            catch (Exception ex)
            {
                return Json(JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteExterno(int id)
        {
            try
            {
                return Json(new {success = true});
            }
            catch (Exception ex)
            {
                return Json(JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult CreateExterno()
        {
            var listtipos = _tiposRepository.ObtenerTipos();

            ViewBag.listTipos = new SelectList((from li in listtipos
                orderby li.Nombre
                select li).ToList(), "Nombre", "Nombre");

            var partner = new PartnerExterno()
            {
                Id = Guid.NewGuid()
            };
            return View(partner);
        }

        // GET: Partners/Create
        [HttpGet]
        public ActionResult CreateInterno()
        {
            var partner = new PartnerInterno()
            {
                Id = Guid.NewGuid(),
                //DireccionSecundaria = new DireccionSecundaria(),
                PersonasDeContacto = new List<PersonaContactoModel>()
            };
            return View(partner);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateExterno(PartnerExterno modelo, HttpPostedFileBase image)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    modelo.FechaCreacion = DateTime.Today.ToShortDateString();
                    modelo.DireccionPrincipal.Id = Guid.NewGuid();
                    modelo.DireccionPrincipal.PartnerId = modelo.Id.ToString();

                    if (image != null && image.ContentLength > 0)
                    {
                        var logo = new Logo
                        {
                            FileName = System.IO.Path.GetFileName(image.FileName),
                            FileType = FileType.Logo,
                            ContentType = image.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(image.InputStream))
                        {
                            logo.Content = reader.ReadBytes(image.ContentLength);
                        }
                        //modelo.Logo = logo;
                        logo.Id = Guid.NewGuid();
                        logo.IdPartner = modelo.Id.ToString();

                        _logoRepository.Insertar(logo);
                    }

                    _partnerRepository.Insertar(modelo);



                    return RedirectToAction("Index");
                }
                var listtipos = _tiposRepository.ObtenerTipos();

                ViewBag.listTipos = new SelectList((from li in listtipos
                                                    orderby li.Nombre
                                                    select li).ToList(), "Nombre", "Nombre");

                return View(modelo);
            }
            catch (Exception ex)
            {
                return View(modelo);
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateInterno(PartnerInterno modelo, HttpPostedFileBase image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    modelo.FechaCreacion = DateTime.Today.ToShortDateString();
                    modelo.DireccionPrincipal.Id = Guid.NewGuid();
                    modelo.DireccionPrincipal.PartnerId = modelo.Id.ToString();

                    //modelo.Logo.IdPartner = modelo.Id.ToString();

                    if (image != null && image.ContentLength > 0)
                    {
                        var logo = new Logo
                        {
                            FileName = System.IO.Path.GetFileName(image.FileName),
                            FileType = FileType.Logo,
                            ContentType = image.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(image.InputStream))
                        {
                            logo.Content = reader.ReadBytes(image.ContentLength);
                        }
                        //modelo.Logo = logo;
                        logo.Id = Guid.NewGuid();
                        logo.IdPartner = modelo.Id.ToString();

                        _logoRepository.Insertar(logo);
                    }
                    //modelo.DireccionSecundaria.Id = Guid.NewGuid();

                   
                    _partnerRepository.Insertar(modelo);

                    return RedirectToAction("Index");
                }
                return View(modelo);
            }
            catch (Exception ex)
            {
                return View(modelo);
                throw;
            }
        }

        // GET: PartnerInterno/Edit/5
        public ActionResult EditarPartnerInterno(string id )
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var direccion = _direccionRepository.ObtenerTodasLasDireccionesPrincipales().First(d => d.PartnerId == id);
            var partner = _partnerRepository.ObtenerPartnersInternos().First(p => p.Id == Guid.Parse(id));

            partner.DireccionPrincipal = direccion;


            if (partner == null)
            {
                return HttpNotFound();
            }

            partner.Logo = _logoRepository.ObtenerLogoPorIdPartner(id);

            return View(partner);
        }

        // POST: PartnerInterno/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPartnerInterno(PartnerInterno partner, HttpPostedFileBase imagen)
        {
            if (ModelState.IsValid)
            {
                if (imagen != null && imagen.ContentLength > 0)
                {
                    if (partner.Logo != null && partner.Logo.FileType == FileType.Logo)
                    {
                        _logoRepository.Eliminar(_logoRepository.ObtenerLogoPorId(partner.Logo.Id.ToString()));
                    }
                    var logo = new Logo
                    {
                        FileName = System.IO.Path.GetFileName(imagen.FileName),
                        FileType = FileType.Logo,
                        ContentType = imagen.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(imagen.InputStream))
                    {
                        logo.Content = reader.ReadBytes(imagen.ContentLength);
                    }
                    logo.Id = partner.Logo != null ? partner.Logo.Id : Guid.NewGuid();
                    logo.IdPartner = partner.Id.ToString();
                    partner.Logo = logo;

                    _logoRepository.Insertar(logo);
                }

                _partnerRepository.Actualizar(partner);

                return RedirectToAction("Index");
            }
            return View(partner);
        }

        // GET: PartnerExterno/Edit/5
        public ActionResult EditarPartnerExterno(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var direccion = _direccionRepository.ObtenerTodasLasDireccionesPrincipales().First(d => d.PartnerId == id);

            var listtipos = _tiposRepository.ObtenerTipos();

            ViewBag.listTipos = new SelectList((from li in listtipos
                                                orderby li.Nombre
                                                select li).ToList(), "Nombre", "Nombre");

            var partner = _partnerRepository.ObtenerPartnersExternos().First(p => p.Id == Guid.Parse(id));

            partner.DireccionPrincipal = direccion;

            if (partner == null)
            {
                return HttpNotFound();
            }
            partner.Logo = _logoRepository.ObtenerLogoPorIdPartner(id);

            return View(partner);
        }

        // POST: PartnerExterno/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPartnerExterno(PartnerExterno partner, HttpPostedFileBase imagen)
        {
            if (ModelState.IsValid)
            {
               

                if (imagen != null && imagen.ContentLength > 0)
                {
                    if (partner.Logo != null && partner.Logo.FileType == FileType.Logo)
                    {
                        _logoRepository.Eliminar(_logoRepository.ObtenerLogoPorId(partner.Logo.Id.ToString()));
                    }
                    var logo = new Logo
                    {
                        FileName = System.IO.Path.GetFileName(imagen.FileName),
                        FileType = FileType.Logo,
                        ContentType = imagen.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(imagen.InputStream))
                    {
                        logo.Content = reader.ReadBytes(imagen.ContentLength);
                    }
                    logo.Id = partner.Logo != null ? partner.Logo.Id : Guid.NewGuid();
                    logo.IdPartner = partner.Id.ToString();
                    partner.Logo = logo;

                    _logoRepository.Insertar(logo);
                }

                _partnerRepository.Actualizar(partner);

                return RedirectToAction("Index");
            }

            var listtipos = _tiposRepository.ObtenerTipos();

            ViewBag.listTipos = new SelectList((from li in listtipos
                                                orderby li.Nombre
                                                select li).ToList(), "Nombre", "Nombre");

            return View(partner);
        }

        // GET: People/Delete/5
        public ActionResult Eliminar(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartnerModel partner = _partnerRepository.ObtenerPartnerPorId(id);

            if (partner == null)
            {
                return HttpNotFound();
            }

            partner.Logo = _logoRepository.ObtenerLogoPorIdPartner(id);

            return View(partner);
        }

        public ActionResult EliminarConfirmed(string id)
        {
            PartnerModel partner = _partnerRepository.ObtenerPartnerPorId(id);

            partner.Estado = "Baja";
            partner.FechaBaja = DateTime.Today.ToShortDateString();
            
            _partnerRepository.Actualizar(partner);

            //var direcciones = _direccionRepository.ObtenerTodaslasDireccionesPorIdPartner(id);
            //if (direcciones != null)
            //{
            //    foreach (var dire in direcciones)
            //    {
            //        _direccionRepository.Eliminar(dire);
            //    }
            //}

            //var personasDeContacto = _personaDeContactoRepository.ObetenerPersonasDeContactoPorIdPartner(id);

            //if (personasDeContacto != null)
            //{
            //    foreach (var per in personasDeContacto)
            //    {
            //        _personaDeContactoRepository.Eliminar(per);
            //    }
            //}
            return RedirectToAction("Index");
        }

        public ActionResult CargarProveedoresInternos(JQueryDataTableParamModel param)
        {
            var listInternos = _partnerRepository.ObtenerPartnersInternos().Where(m => m.FechaBaja == null).ToList();

            foreach (var interno in listInternos)
            {
                var dire = _direccionRepository.ObtenerDireccionPorIdPartner(interno.Id.ToString());
                var contactoPrincipal =
                    _personaDeContactoRepository.ObetenerPersonasDeContactoPorIdPartner(interno.Id.ToString()).Where(m => m.Principal == true);
                        
                interno.PersonasDeContacto = contactoPrincipal.ToList();
                interno.DireccionPrincipal = (DireccionPrincipal) dire;
            }

            IEnumerable<PartnerInterno> filteredCompanies;
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                //Used if particulare columns are filtered 
                var nombreFilter = Convert.ToString(Request["sSearch_1"]);
                var contactoFilter = Convert.ToString(Request["sSearch_2"]);

                //Optionally check whether the columns are searchable at all 
                var isNombreSearchable = Convert.ToBoolean(Request["bSearchable_1"]);
                var isContactoSearchable = Convert.ToBoolean(Request["bSearchable_2"]);


                filteredCompanies = listInternos?.ToList()
                    .Where(c => isNombreSearchable && c.Nombre.ToLower().Contains(param.sSearch.ToLower())
                                ||
                                isContactoSearchable && c.Cif.ToLower().Contains(param.sSearch.ToLower()));

            }
            else
            {
                filteredCompanies = listInternos;
            }

            var isNombreSortable = Convert.ToBoolean(Request["bSortable_1"]);
            var isContactoSortable = Convert.ToBoolean(Request["bSortable_2"]);
            
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<PartnerInterno, string> orderingFunction = (c => sortColumnIndex == 1 && isNombreSortable
                ? c.Nombre
                : sortColumnIndex == 2 && isContactoSortable
                    ? c.DireccionPrincipal.Calle: "");

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            filteredCompanies = sortDirection == "asc" ? filteredCompanies.OrderBy(orderingFunction) : filteredCompanies.OrderByDescending(orderingFunction);

            var displayedCompanies = filteredCompanies.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            
            var result = from c in displayedCompanies
                select new[]
                {
                    Convert.ToString(c.Id),
                    c.Nombre,
                    c.PersonasDeContacto.First().Nombre,
                    c.PersonasDeContacto.First().Telefono ,
                    c.ActividadProfesional,
                    c.FechaAlta};

            return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = listInternos.Count(),
                    iTotalDisplayRecords = filteredCompanies.Count(),
                    aaData = result
                },
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult CargarProveedoresExternos(JQueryDataTableParamModel param)
        {
            var listExternos = _partnerRepository.ObtenerPartnersExternos();

            IEnumerable<PartnerExterno> filteredCompanies;
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                //Used if particulare columns are filtered 
                var nombreFilter = Convert.ToString(Request["sSearch_1"]);
                var tipoFilter = Convert.ToString(Request["sSearch_2"]);


                //Optionally check whether the columns are searchable at all 
                var isNombreSearchable = Convert.ToBoolean(Request["bSearchable_1"]);
                var isTipoSearchable = Convert.ToBoolean(Request["bSearchable_3"]);


                filteredCompanies = listExternos?.ToList()
                    .Where(c => isNombreSearchable && c.Nombre.ToLower().Contains(param.sSearch.ToLower())
                                ||
                                isTipoSearchable && c.Tipo.ToLower().Contains(param.sSearch.ToLower()));
            }
            else
            {
                filteredCompanies = listExternos;
            }

            var isNombreSortable = Convert.ToBoolean(Request["bSortable_1"]);
            var isTipoSortable = Convert.ToBoolean(Request["bSortable_2"]);
            var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            Func<PartnerExterno, string> orderingFunction = (c => sortColumnIndex == 1 && isNombreSortable 
                ? c.Nombre: sortColumnIndex == 2 && isTipoSortable
                       ? c.FechaAlta: "");

            var sortDirection = Request["sSortDir_0"]; // asc or desc
            if (sortDirection == "asc")
                filteredCompanies = filteredCompanies.OrderBy(orderingFunction);
            else
                filteredCompanies = filteredCompanies.OrderByDescending(orderingFunction);

            var displayedCompanies = filteredCompanies.Skip(param.iDisplayStart).Take(param.iDisplayLength);
            var result = from c in displayedCompanies select new[] {Convert.ToString(c.Id), c.Nombre, c.Tipo , c.FechaAlta};
            return Json(new
                {
                    sEcho = param.sEcho,
                    iTotalRecords = listExternos.Count(),
                    iTotalDisplayRecords = filteredCompanies.Count(),
                    aaData = result
                },
                JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult GuardarTipo(string nombre)
        {
            var tipo = new TipoModel()
            {
                Id = Guid.NewGuid(),
                Nombre = nombre
            };

            try
            {
                _tiposRepository.Insertar(tipo);
                return Json(new { id= tipo.Nombre,  success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
                throw;
            }

        }
    }
}
