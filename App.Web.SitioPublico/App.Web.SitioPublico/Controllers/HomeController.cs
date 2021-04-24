using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;
using Newtonsoft.Json;

namespace App.Web.SitioPublico.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Produccion = int.Parse(WebConfigurationManager.AppSettings["Produccion"]);
            return View();
        }

        public async Task<ActionResult> EnviarSolicitud(string nombre, string asunto, string mensaje, string correo)
        {
            var empresa = await Bcp.Mail.EnviarMail(nombre, "Secar Soluciones - " + asunto, mensaje + " " + nombre + " " + correo, "sesepulveda@secarsoluciones.cl");
            var cliente = await Bcp.Mail.EnviarMail(nombre, "Contacto Secar Soluciones", "Gracias por contactarnos, en las proximas horas un ejecutivo resolvera  esta solicitud<br/>" + mensaje, correo);
            return Json(true);
        }

    }
}