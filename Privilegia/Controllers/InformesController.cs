﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Privilegia.Models;
using Privilegia.Models.Direcciones;
using Privilegia.Models.FacturacionPartners;
using Privilegia.Models.FacturacionPublicidad;
using Privilegia.Models.FacturasPremios;
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
        private IFacturasPremiosRepository _facturasPremiosRepository;
        private IFacturacionPremiosRepository _facturacionPremiosRepository;

        public InformesController()
        {
            _partnerRepository = new PartnerRepository();
            _productosRespository = new ProductosRespository();
            _publicidadRepository = new PublicidadRepository();
            _facturacionPublicidadRepository = new FacturacionPublicidadRepository();
            _direccionRepository = new DireccionRepository();
            _facturasPremiosRepository = new FacturasPremiosRepository();
            _facturacionPremiosRepository = new FacturacionPremiosRepository();
        }
        // GET: Informes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VerFacturaPremios(string id)
        {
            var factura = _facturasPremiosRepository.ObtenerFacturaPremiosPorIdFactura(id);
            var elementos = _facturacionPremiosRepository.ObtenerFacturaPremiosPorIdFactura(id);
            var partner = _partnerRepository.ObtenerPartnerPorId(factura.IdPartner);

            partner.DireccionPrincipal =
               (DireccionPrincipal)_direccionRepository.ObtenerDireccionPorIdPartner(partner.Id.ToString());


            var modelo = new FacturaPremiosViewModel()
            {
                Partner = partner,
                Lista = elementos,
                Fecha = factura.FechaDeCreacion,
                Factura = _facturasPremiosRepository.ObtenerFacturaPremiosPorIdFactura(id)

            };

            return new Rotativa.PartialViewAsPdf(modelo);

        }

        public ActionResult VerFacturaPublicidad(string id)
        {
            var temp = _facturacionPublicidadRepository.ObtenerFacturacion().First(m => m.IdFactura == id);
            var partner = _partnerRepository.ObtenerPartnerPorId(temp.IdPartner);

            partner.DireccionPrincipal =
                (DireccionPrincipal)_direccionRepository.ObtenerDireccionPorIdPartner(partner.Id.ToString());

            var modelo = new FacturaPublicidadViewModel()
            {
                Partner = partner,
                ListadePublicidad = new List<PublicidadModel>(),
                Fecha = temp.FechaCreacion
            };



            var registrosFactura = _facturacionPublicidadRepository.ObtenerFacturacion().Where(m => m.IdFactura == id).ToList();

            modelo.Factura = registrosFactura?.First();

            foreach (var registro in registrosFactura)
            {
                var publicidad = _publicidadRepository.ObtenerPublicidadPorId(registro.IdPublicidad);
                modelo.ListadePublicidad.Add(publicidad);

            }

            return new Rotativa.PartialViewAsPdf(modelo);
        }

        public ActionResult EditarFacturaPremios(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var factura = _facturasPremiosRepository.ObtenerFacturaPremiosPorIdFactura(id);

            if (factura == null)
            {
                return HttpNotFound();
            }

            return View("EditarFacturaPremios", factura);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarFacturaPremios(FacturasPremiosModel factura)
        {
            if (ModelState.IsValid)
            {

                _facturasPremiosRepository.Actualizar(factura);
                
                return RedirectToAction("FacturasPremiosPartner");

            }

            return View("EditarFacturaPremios", factura);
        }

        public ActionResult EditarFacturaPublicidad(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var factura = _facturacionPublicidadRepository.ObtenerFacturaPorIdFactura(id);

            if (factura == null)
            {
                return HttpNotFound();
            }

            return View("EditarFacturaPublicidad", factura);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarFacturaPublicidad(FacturacionPublicidadModel factura)
        {
            if (ModelState.IsValid)
            {
                var facturas =
                    _facturacionPublicidadRepository.ObtenerFacturacion().Where(m => m.IdFactura == factura.IdFactura).ToList();

                if (facturas.Any())
                {
                    foreach (var item in facturas)
                    {
                        item.Titulo = factura.Titulo;
                        item.Concepto = factura.Concepto;
                        item.Total = factura.Total;
                        item.Estado = factura.Estado;
                        _facturacionPublicidadRepository.Actualizar(item);
                    }



                    return RedirectToAction("Publicidad");

                }

                return View("EditarFacturaPublicidad", factura);
            }

            return View("EditarFacturaPublicidad", factura);
        }

        public ActionResult Publicidad()
        {
            var listaPartners = _partnerRepository.ObtenerPartners();

            ViewBag.listPartners = listaPartners.ToList().OrderBy(m => m.Nombre).Where(m => m.FechaBaja == null);

            return View();
        }

        public ActionResult FacturasPremiosPartner()
        {
            var listaPartners = _partnerRepository.ObtenerPartners();

            ViewBag.listPartners = listaPartners.ToList().OrderBy(m => m.Nombre).Where(m => m.FechaBaja == null);

            return View();
        }

        public ActionResult InformePremiosProductosPartner()
        {
            var listaPartners = _partnerRepository.ObtenerPartners();

            ViewBag.listPartners = listaPartners.ToList().OrderBy(m => m.Nombre).Where(m => m.FechaBaja == null);

            return View();
        }

        public ActionResult InformePremiosPorPartner()
        {
            var listaPartners = _partnerRepository.ObtenerPartners();

            ViewBag.listPartners = listaPartners.ToList().OrderBy(m => m.Nombre).Where(m => m.FechaBaja == null);

            return View();
        }
        public ActionResult InformePremiosMutualista()
        {
            return View();
        }

        public ActionResult CargarFacturasPremios(string idPartner)
        {
            var listaFacturas = new List<FacturasPremiosModel>();

            listaFacturas = idPartner.IsEmpty() ? _facturasPremiosRepository.ObtenerFacturacionPremios() : _facturasPremiosRepository.ObtenerFacturasPremiosPorIdPartner(idPartner);

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
                    Convert.ToString(c.Id), c.Partner.Nombre, c.FechaDeCreacion, c.Estado, c.Total
                         };

            return Json(new
            {
                iTotalRecords = listaFacturas?.Count(),
                iTotalDisplayRecords = listaFacturas.Count(),
                aaData = result
            },
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult CargarFacturasPublicidad(string idPartner)
        {
            var listaFacturas = new List<FacturacionPublicidadModel>();

            if (!idPartner.IsEmpty())
            {
                listaFacturas = _facturacionPublicidadRepository.ObtenerFacturacionPorIdPartner(idPartner);
            }
            else
            {
                listaFacturas = _facturacionPublicidadRepository.ObtenerFacturacion();

                var listaFinal = new List<FacturacionPublicidadModel>();
                foreach (var item in listaFacturas.GroupBy(m => m.IdFactura))
                {
                    listaFinal.Add(item.First());
                }

                listaFacturas = listaFinal;

            }

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
                    Convert.ToString(c.Id), c.Partner.Nombre, c.FechaCreacion, c.PlanDeMedios.ToString(), c.Total, c.Estado,
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

        public ActionResult CargarInformePremiosProductosPartner(string idPartner)
        {
            IEnumerable<IGrouping<string, FacturacionPremiosModel>> listaPremiosProducto;
            var listafinal = new List<FacturacionPremiosModel>();
            if (idPartner.IsEmpty())
            {
                listaPremiosProducto = _facturacionPremiosRepository.ObtenerFacturacionPremios().GroupBy(m => m.NifProveed).ToList();

                //models.key = nif Proveedor
                foreach (var partner in listaPremiosProducto)
                {
                    //partner.key es el partner
                    var sum = 0.0;
                    foreach (var producto in partner.GroupBy(m => m.CodigoPcto))
                    {
                        //producto.Key es el codigo del producto

                        foreach (var fecha in producto.GroupBy(m => m.FechaPago))
                        {
                            foreach (var premio in fecha)
                            {
                                sum += double.Parse(premio.ComisionPago);
                            }

                            listafinal.Add(new FacturacionPremiosModel()
                            {
                                Id = Guid.NewGuid(),
                                CodigoPcto = producto.Key,
                                NifProveed = partner.Key,
                                ComisionPago = sum.ToString("##.###"),
                                FechaPago = fecha.Key
                            });
                        }


                    }
                }

                var result = from c in listafinal
                             select new[]
                             {
                             c.NifProveed, c.CodigoPcto, c.FechaPago, c.ComisionPago
                        };

                return Json(new
                {
                    iTotalRecords = result?.Count(),
                    iTotalDisplayRecords = result.Count(),
                    aaData = result
                }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var partner = _partnerRepository.ObtenerPartners().First(m => m.Id == Guid.Parse(idPartner));

                listaPremiosProducto = _facturacionPremiosRepository.ObtenerFacturasPremiosPorNifPartner(partner.Cif).GroupBy(m => m.CodigoPcto);

                var sum = 0.0;
                var premiosProducto = listaPremiosProducto as IList<IGrouping<string, FacturacionPremiosModel>> ?? listaPremiosProducto.ToList();

                foreach (var models in premiosProducto)
                {
                    foreach (var producto in premiosProducto)
                    {
                        //producto.Key codigo producto

                        foreach (var premio in producto)
                        {
                            sum += double.Parse(premio.ComisionPago);
                        }

                        listafinal.Add(new FacturacionPremiosModel()
                        {
                            Id = Guid.NewGuid(),
                            CodigoPcto = producto.Key,
                            NifProveed = partner.Cif,
                            ComisionPago = sum.ToString("##.###")
                        });
                    }

                    var result = from c in listafinal
                                 select new[]
                                 {
                            Convert.ToString(c.Id), c.NifProveed, c.CodigoPcto, c.ComisionPago
                        };

                    return Json(new
                    {
                        iTotalRecords = result?.Count(),
                        iTotalDisplayRecords = result.Count(),
                        aaData = result
                    }, JsonRequestBehavior.AllowGet);
                }

                return Json(new
                {
                    iTotalRecords = 0,
                    iTotalDisplayRecords = 0,
                    aaData = new List<FacturacionPremiosModel>()
                },
                     JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult CargarInformePremiosPartner(string idPartner)
        {
            List<FacturacionPremiosModel> listaPremios;

            if (idPartner.IsEmpty())
            {
                listaPremios = _facturacionPremiosRepository.ObtenerFacturacionPremios();
            }
            else
            {
                var partner = _partnerRepository.ObtenerPartners().First(m => m.Id == Guid.Parse(idPartner));
                listaPremios = _facturacionPremiosRepository.ObtenerFacturasPremiosPorNifPartner(partner.Cif);
            }

            if (listaPremios.Count > 0)
            {
                var result = from c in listaPremios
                             select new[]
                             {
                         c.NifProveed, c.FechaDeCreacion, c.ComisionPago
                    };

                return Json(new
                {
                    iTotalRecords = listaPremios?.Count(),
                    iTotalDisplayRecords = listaPremios.Count(),
                    aaData = result
                },
                    JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    iTotalRecords = 0,
                    iTotalDisplayRecords = 0,
                    aaData = listaPremios
                },
                   JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult CargarInformePremiosMutualista()
        {
            List<FacturacionPremiosModel> listaPremios;

            listaPremios = _facturacionPremiosRepository.ObtenerFacturasPremiosPorMutualista();

            if (listaPremios.Count > 0)
            {
                var result = from c in listaPremios
                             select new[]
                             {
                         c.NifProveed, c.CodigoCliente, c.CodigoPcto,c.ComisionPago , c.FechaDeCreacion
                    };

                return Json(new
                {
                    iTotalRecords = listaPremios?.Count(),
                    iTotalDisplayRecords = listaPremios.Count(),
                    aaData = result
                },
                    JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    iTotalRecords = 0,
                    iTotalDisplayRecords = 0,
                    aaData = listaPremios
                },
                   JsonRequestBehavior.AllowGet);
            }

        }
    }
}