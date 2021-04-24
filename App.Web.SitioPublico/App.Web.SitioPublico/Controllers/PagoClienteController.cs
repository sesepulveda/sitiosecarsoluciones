using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace App.Web.SitioPublico.Controllers
{
    public class PagoClienteController : Controller
    {
        // GET: PagoCliente
        public ActionResult Index()
        {
            ViewBag.Produccion = int.Parse(WebConfigurationManager.AppSettings["Produccion"]);
            return View();
        }

        public ActionResult Buscador(string cliente)
        {
            int rutCliente = int.Parse(cliente.Replace(".", "").Split('-')[0]);
            int idEmpresa = int.Parse(WebConfigurationManager.AppSettings["IdEmpresa"]);
            Bcp.Session.Cerrar(Bcp.Session.SessionInfoPago);
            List<Models.ResultFacturacion> infoPago = JsonConvert.DeserializeObject<List<Models.ResultFacturacion>>(Bcp.Facturacion.ObtenerFacturacion(rutCliente, idEmpresa).Content);

            ViewBag.Facturacion = infoPago.OrderByDescending(o => o.Estado).ToList();
            ViewBag.Produccion = int.Parse(WebConfigurationManager.AppSettings["Produccion"]);
            return View();
        }

        public ActionResult ExisteCliente(string rut)
        {
            int rutCliente = int.Parse(rut.Replace(".", "").Split('-')[0]);
            int idEmpresa = int.Parse(WebConfigurationManager.AppSettings["IdEmpresa"]);
            Object cliente = JsonConvert.DeserializeObject<Object>(Bcp.Cliente.Obtener(rutCliente,idEmpresa).Content);
            return Json(cliente == null ? false : true);
        }
    }
}