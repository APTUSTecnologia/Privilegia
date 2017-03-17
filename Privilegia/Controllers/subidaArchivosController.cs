using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FileHelpers;
using Privilegia.Models;
using Privilegia.Models.FacturacionPartners;
using Privilegia.Models.Partner;
using Privilegia.ViewModels;

namespace Privilegia.Controllers
{
    public class SubidaArchivosController : Controller
    {
        private IPartnerRepository _partnerRepository;
        private IFacturacionPremiosRepository _facturacionPremiosRepository;

        public SubidaArchivosController()
        {
            _partnerRepository = new PartnerRepository();
            _facturacionPremiosRepository = new FacturacionPremiosRepository();
        }
        // GET: subidaArchivos
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CargarArchivo(string proveedor, HttpPostedFileBase archivo)
        {

            if (archivo != null && archivo.ContentLength > 0)
            {
                byte[] contenido;
                using (var reader = new System.IO.BinaryReader(archivo.InputStream))
                {
                    contenido = reader.ReadBytes(archivo.ContentLength);
                }

                string texto = System.Text.Encoding.UTF8.GetString(contenido);

               
                //modelo.Logo = logo;
                FicheroViewModel modelo;
                switch (proveedor)
                {
                    case "Guijuelo":
                        var engine = new FileHelperEngine<FicheroGuijuelo>();
                        engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
                        List<FicheroGuijuelo> datos = engine.ReadString(texto).ToList();

                        if (engine.ErrorManager.HasErrors)
                            ViewBag.errores = engine.ErrorManager.Errors;

                        modelo = new FicheroViewModel()
                        {
                            Errores = engine.ErrorManager.Errors.ToList(),
                            ContenidoArchivo = texto,
                            RegistrosCorrectos = datos.Count
                        };

                        if (modelo.RegistrosCorrectos > 0 && _partnerRepository.ExistePartner(datos[0].NIF_PROV))
                        {
                            _facturacionPremiosRepository.InsertarGuijuelo(datos);
                        }

                        //TODO Cargamos los registros correcto en la BBDD
                        return View("VerDatosArchivo", modelo);

                    case "YoRespondo":
                        var engineYoRespondo = new FileHelperEngine<FicheroYoRespondo>();
                        engineYoRespondo.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
                        List<FicheroYoRespondo> datosYoRespondo = engineYoRespondo.ReadString(texto).ToList();

                        if (engineYoRespondo.ErrorManager.HasErrors)
                            ViewBag.errores = engineYoRespondo.ErrorManager.Errors;

                        modelo = new FicheroViewModel()
                        {
                            Errores = engineYoRespondo.ErrorManager.Errors.ToList(),
                            ContenidoArchivo = texto,
                            RegistrosCorrectos = datosYoRespondo.Count
                        };

                        //TODO Cargamos los registros correcto en la BBDD
                        return View("VerDatosArchivo", modelo);
                    default:
                        return View("Index");
                }
            }

            return View("Index");

        }

        [HttpPost]
        public ActionResult GuardarDatosGuijuelo(FicheroViewModel modeloVista)
        {

            var engine = new FileHelperEngine<FicheroGuijuelo>();
            engine.ErrorManager.ErrorMode = ErrorMode.IgnoreAndContinue;
            List<FicheroGuijuelo> datos = engine.ReadString(modeloVista.ContenidoArchivo).ToList();

            // si esta todo correcto viewBag notificacion
            return View("Index");
        }
    }
}