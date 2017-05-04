using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;
using FileHelpers;
using FileHelpers.Events;
using Privilegia.Models;
using Privilegia.Models.FacturacionPartners;
using Privilegia.Models.FacturasPremios;
using Privilegia.Models.Ficheros;
using Privilegia.Models.Incidencias;
using Privilegia.Models.Partner;
using Privilegia.ViewModels;

namespace Privilegia.Controllers
{
    public class SubidaArchivosController : Controller
    {
        private IPartnerRepository _partnerRepository;
        private IFacturacionPremiosRepository _facturacionPremiosRepository;
        private IFacturasPremiosRepository _facturasPremiosRepository;
        private IIncidenciaRepository _incidenciaRepository;

        public SubidaArchivosController()
        {
            _partnerRepository = new PartnerRepository();
            _facturacionPremiosRepository = new FacturacionPremiosRepository();
            _facturasPremiosRepository = new FacturasPremiosRepository();
            _incidenciaRepository = new IncidenciasRepository();
        }

        // GET: subidaArchivos
        public ActionResult Index()
        {
            //miramos si hay temp activos y los eliminamos

            var temp = _facturacionPremiosRepository.ObtenerFacturacionPremios().Where(m => m.Temp == 1).ToList();

            if (!temp.Any()) return View();

            foreach (var registro in temp)
            {
                _facturacionPremiosRepository.Eliminar(registro);
            }

            //Aqui tenemos que mandar los partners
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CargarArchivo(string proveedor, HttpPostedFileBase archivo)
        {
            try
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES");

                if (archivo != null && archivo.ContentLength > 0)
                {
                    byte[] contenido;
                    using (var reader = new System.IO.BinaryReader(archivo.InputStream))
                    {
                        contenido = reader.ReadBytes(archivo.ContentLength);
                    }

                    string texto = Encoding.UTF8.GetString(contenido);



                    //modelo.Logo = logo;
                    FicheroViewModel modelo = new FicheroViewModel();
                    List<ErrorInfo> errores = new List<ErrorInfo>();
                    PartnerModel partner = _partnerRepository.ObtenerPartners().FirstOrDefault(m => m.Nombre.ToLower().Contains(proveedor.ToLower()));


                    if (partner != null && partner.Nombre == "Yo Respondo" || partner.Nombre == "Renta 4" ||
                        partner.Nombre == "Woltos Kluwe" || partner.Nombre == "Yo Respondo" || partner.Nombre == "Viajes el Corte Ingles" ||
                        partner.Nombre == "Avis" || partner.Nombre == "Haq" || partner.Nombre == "Endesa")
                    {
                        var engineYoRespondo = new FileHelperEngine<FicheroYoRespondo>();
                        engineYoRespondo.AfterReadRecord += AfterEvent;
                        engineYoRespondo.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
                        List<FicheroYoRespondo> datosArchivo = engineYoRespondo.ReadString(texto).ToList();

                        if (engineYoRespondo.ErrorManager.HasErrors)
                            errores = engineYoRespondo.ErrorManager.Errors.ToList();

                        if (datosArchivo.Count > 0 && _partnerRepository.ExistePartner(datosArchivo[0].NIF_PROVEED) &&
                            datosArchivo[0].NIF_PROVEED == partner.Cif)
                        {
                            //Tenemos datos correctos , existe el partner y esta bien seleccionado
                            //Creamos Factura
                            modelo.Cargados = _facturacionPremiosRepository.MapearStandar(datosArchivo);
                            modelo.Partner = partner;
                            modelo.RegistrosCorrectos = datosArchivo.Count;

                            if (errores.Count > 0)
                            {
                                //Tratamos los errores 
                                modelo.ListaRegistrosConErrores = _facturacionPremiosRepository.MapearErrores(errores);
                                modelo.Incidencias = new List<IncidenciaModel>();
                                //Creamos las incidencias
                                if (modelo.ListaRegistrosConErrores.Count > 0)
                                {
                                    var cont = 0;
                                    foreach (var error in errores)
                                    {
                                        var incidencia = new IncidenciaModel()
                                        {
                                            Id = Guid.NewGuid(),
                                            FechaDeCreacion = DateTime.Today.ToShortDateString(),
                                            Descripcion = error.ExceptionInfo.Message,
                                            Linea = error.LineNumber.ToString(),
                                            NombreFichero = error.ExceptionInfo.Source,
                                            CifPartner = partner.Cif
                                        };
                                        _incidenciaRepository.Insertar(incidencia);

                                        modelo.Incidencias.Add(incidencia);

                                        modelo.ListaRegistrosConErrores[cont].IdIncidencia = incidencia.Id.ToString();
                                        cont++;
                                    }
                                    //Meto los correctos en un String para facilitar el post en la siguiente vista
                                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                                    modelo.JsonCorrectos = serializer.Serialize(modelo.Cargados);

                                    return View("EditarErrores", modelo);
                                }
                                else
                                {

                                    //Hay errores en el archivo pero ya estan registrados en base de datos correctamente
                                    return RedirectToAction("VerDatosArchivo", new { nif = modelo.Partner.Cif });
                                }
                            }

                            return RedirectToAction("VerDatosArchivo", new { nif = modelo.Partner.Cif });

                        }
                        else if (errores.Count > 0 && _partnerRepository.ExistePartner(datosArchivo[0].NIF_PROVEED) &&
                            datosArchivo[0].NIF_PROVEED == partner.Cif)
                        {
                            //No tenemos registros correctos estan todos mal


                            modelo.ListaRegistrosConErrores = _facturacionPremiosRepository.MapearErrores(errores);
                            modelo.Partner = partner;

                            return View("EditarErrores", modelo);

                        }
                        else
                        {
                            //No tenemos correctos o no existe el partner del archivo o no coincide el partner seleccionado con el que viene en el archivo
                            ViewBag.error = "del partner al crear los registros.";

                            return View("Index");
                        }

                    }
                    else if (partner.Nombre.ToLower() == "la caixa")
                    {

                        var objeto = ObjectToXML(texto, typeof(Carga));
                        Carga archivoLaCaixa = (Carga)objeto;
                        if (objeto != null && archivoLaCaixa.NumRegsOk > 0)
                        {
                            modelo.Cargados = _facturacionPremiosRepository.MapearLaCaixa(archivoLaCaixa);
                            foreach (var item in modelo.Cargados)
                            {
                                item.NifProveed = partner.Cif;
                            }
                            modelo.Partner = partner;
                            modelo.RegistrosCorrectos = archivoLaCaixa.NumRegsProces;
                        }


                    }
                    else if (partner.Nombre.ToLower() == "axa")
                    {
                        var engineAxa = new FileHelperEngine<FicheroAxa>();
                        engineAxa.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
                        List<FicheroAxa> datosArchivo = engineAxa.ReadString(texto).ToList();

                        if (engineAxa.ErrorManager.HasErrors)
                            errores = engineAxa.ErrorManager.Errors.ToList();
                    }

                }

                ViewBag.error = "de fichero.";

                return View("Index");
            }
            catch (Exception ex)
            {
                ViewBag.error = "al procesar el archivo.";

                return View("Index");
            }
            

        }
        public Object ObjectToXML(string xml, Type objectType)
        {

            StringReader strReader = null; //UTF 16 StringReader
            XmlSerializer serializer = null;
            XmlTextReader xmlReader = null;
            Object obj = null;
            try
            {
                strReader = new StringReader(xml);
                serializer = new XmlSerializer(objectType);
                xmlReader = new XmlTextReader(strReader);
                obj = serializer.Deserialize(xmlReader);
            }
            catch (Exception exp)
            {
                return exp;
            }
            finally
            {
                if (xmlReader != null)
                {
                    xmlReader.Close();
                }
                if (strReader != null)
                {
                    strReader.Close();
                }
            }
            return obj;
        }
        [HttpPost]
        public ActionResult CrearFacturaPremios(List<FacturacionPremiosModel> modelo)
        {
            if (modelo != null)
            {
                if (modelo.Count > 0)
                {
                    var total = 0.0;
                    var partner = _partnerRepository.ObtenerPartners().First(m => m.Cif == modelo[0].NifProveed);

                    var factura = new FacturasPremiosModel()
                    {
                        Id = Guid.NewGuid(),
                        FechaDeCreacion = DateTime.Today.ToShortDateString(),
                        IdPartner = partner.Id.ToString(),
                        Estado = "Emitida",
                        Concepto = "Premios Privilegia",
                        Titulo = "Factura Premios Privilegia" + partner.Nombre
                    };

                    _facturasPremiosRepository.Insertar(factura);

                    //Añadimos los registros a BBDD

                    foreach (var registro in modelo)
                    {
                        total = total + Double.Parse(registro.ComisionPago);
                        registro.Temp = 0;
                        registro.IdFactura = factura.Id.ToString();
                        _facturacionPremiosRepository.Actualizar(registro);
                    }

                    //total = total + (total * 0.21);
                    factura.Total = Math.Round(total, 2).ToString(CultureInfo.InvariantCulture);

                    return Json(new { success = true, responseText = "Success", idFactura = factura.Id }, JsonRequestBehavior.AllowGet);
                }

                return Json("An Error Has occoured");
            }
            else
            {
                return Json("An Error Has occoured");
            }

        }


        public ActionResult EditarErrores(FicheroViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                foreach (var var in modelo.ListaRegistrosConErrores)
                {
                    //hay mas de un error comprobamos 1 a 1 por si podemos ir quitando
                    if (TryValidateModel(var))
                    {
                        modelo.ListaRegistrosConErrores.Remove(var);

                        var incidencia = _incidenciaRepository.ObtenerTodasLasIncidencias().First(m => m.Id == Guid.Parse(var.IdIncidencia));

                        _incidenciaRepository.Eliminar(incidencia);

                    }
                }
                return View("EditarErrores", modelo);
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var data = serializer.Deserialize<List<FacturacionPremiosModel>>(modelo.JsonCorrectos);

            foreach (var registro in modelo.ListaRegistrosConErrores)
            {
                //eliminamos la incidencia porque esta solucionada
                var incidencia = _incidenciaRepository.ObtenerTodasLasIncidencias().FirstOrDefault(m => m.Id == Guid.Parse(registro.IdIncidencia));

                _incidenciaRepository.Eliminar(incidencia);

                registro.Temp = 1;

                _facturacionPremiosRepository.Insertar(registro);

                data.Add(registro);

            }

            modelo.Partner = _partnerRepository.ObtenerPartners().First(m => m.Cif == data[0].NifProveed);

            modelo.Cargados = data;

            modelo.RegistrosCorrectos = data.Count;



            return RedirectToAction("VerDatosArchivo", new { nif = modelo.Partner.Cif });
        }


        private void AfterEvent(EngineBase engine, AfterReadEventArgs<FicheroYoRespondo> e)
        {
            //  we want to drop all records with no freight
            if (e.Record.IMPORTE_PAGO == 0)
            {
                e.SkipThisRecord = true;
                throw new Exception("Importe incorrecto, El importe no puede ser 0");

            }
            if (e.Record.VALOR == 0)
            {
                e.SkipThisRecord = true;
                throw new Exception("Valor incorrecto, El importe no puede ser 0");

            }



        }

        public ActionResult VerDatosArchivo(string nif)
        {
            if (nif != null)
            {
                var partner = _partnerRepository.ObtenerPartners().First(m => m.Cif == nif);
                var listadeTemp = _facturacionPremiosRepository.ObtenerFacturasPremiosPorNifPartner(nif).Where(m => m.Temp == 1).ToList();


                ViewBag.Nombre = partner.Nombre;

                return View("VerDatosArchivo", listadeTemp);


            }
            else
            {
                var listadeTemp2 = _facturacionPremiosRepository.ObtenerFacturacionPremios().Where(m => m.Temp == 1).ToList();

                if (listadeTemp2.Any())
                {
                    var partner = _partnerRepository.ObtenerPartners().First(m => m.Cif == listadeTemp2[0].NifProveed);

                    ViewBag.Nombre = partner.Nombre;

                    return View("VerDatosArchivo", listadeTemp2);
                }
               

            }

            ViewBag.error = "No hay elementos para visualizar";

            return View("Index");



        }
    }
}