using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Privilegia.Models.Archivos;

namespace Privilegia.Controllers
{
    public class LogosController : Controller
    {
        private ILogoRepository _logoRepository;
        public LogosController()
        {
            _logoRepository = new LogoRepository();
        }
        // GET: Logos
        public ActionResult Index(string id)
        {
            var fileToRetrieve = _logoRepository.ObtenerLogoPorId(id);

            return File(fileToRetrieve.Content, fileToRetrieve.ContentType);
        }
    }
}