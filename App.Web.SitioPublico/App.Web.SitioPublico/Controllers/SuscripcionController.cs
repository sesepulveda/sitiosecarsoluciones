using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Transbank.Webpay;

namespace App.Web.SitioPublico.Controllers
{
    public class SuscripcionController : Controller
    {
        // GET: Suscripcion
        public ActionResult PagoCliente(int id)
        {
            var ambiente = int.Parse(WebConfigurationManager.AppSettings["Produccion"]);
            string returnUrl = "", finalUrl = "";
            var transaction = new Webpay(Configuration.ForTestingWebpayPlusNormal()).NormalTransaction;

            Models.ResultInfoPagar infoPago = JsonConvert.DeserializeObject<Models.ResultInfoPagar>(Bcp.Facturacion.ObtenerInfoPagar(id).Content);

            if (ambiente != 1)
            {
                returnUrl = "http://localhost:52769/Suscripcion/Return";
                finalUrl = "http://localhost:52769/Suscripcion/Final";
            }
            else
            {
                returnUrl = "http://www.secarsoluciones.cl/Suscripcion/Return";
                finalUrl = "http://www.secarsoluciones.cl/Suscripcion/Final";
            }

            var sessionId = ambiente != 1 ? "597020000540" : infoPago.CodigoTransabank;
            var initResult = transaction.initTransaction(infoPago.Monto, infoPago.NroOrden, sessionId, returnUrl, finalUrl);
            var tokenWs = initResult.token;
            var formAction = initResult.url;

            ViewBag.Amount = "$" + infoPago.Monto.ToString("N0");
            ViewBag.BuyOrden = infoPago.NroOrden;
            ViewBag.TokenWs = tokenWs;
            ViewBag.FormAction = formAction;

            ViewBag.Vencimiento = infoPago.Vencimiento;
            ViewBag.Servicio = infoPago.Servicio;
            ViewBag.Produccion = ambiente;
            ViewBag.Comercio = infoPago.Comercio;
            ViewBag.RutCliente = infoPago.RutCliente;

            return View();
        }

        public async Task<ActionResult> Return()
        {
            Models.ResultPago pago = new Models.ResultPago();

            var transaction = new Webpay(Configuration.ForTestingWebpayPlusNormal()).NormalTransaction;
            string tokenWs = Request.Form["token_ws"];
            var result = transaction.getTransactionResult(tokenWs);
            var output = result.detailOutput[0];

            ViewBag.ResponseCode = output.responseCode;
            ViewBag.NroOrden = result.buyOrder;

            Models.ResultInfoPagar infoPago = JsonConvert.DeserializeObject<Models.ResultInfoPagar>(Bcp.Facturacion.ObtenerInfoPagar(int.Parse(result.buyOrder)).Content);
            pago.IdIngreso = 0;
            pago.IdFacturacion = infoPago.IdFacturacion;
            pago.Fecha = new Bcp.Util().FechaActual;
            pago.IdTipoMedioPago = (int)Bcp.TipoMedioPago.WebPay;
            pago.IdDetalleIngreso = 0;
            pago.IdTipoBanco = (int)Bcp.TipoMedioPago.WebPay;
            pago.NroCuenta = string.Empty;
            pago.Monto = (int)output.amount;
            pago.NroOrden = result.buyOrder;
            pago.CodigoRespuesta = output.responseCode;
            pago.CodigoAutorizacion = output.authorizationCode;
            pago.UrlRedireccion = result.urlRedirection;
            pago.Token = tokenWs;
            pago.SessionId = result.sessionId;
            pago.NumeroTarjeta = result.cardDetail.cardNumber;
            pago.FechaAutorizacion = result.accountingDate;
            pago.FechaTransaccion = result.transactionDate.ToString("dd-MM-yyyy HH:mm:ss");
            pago.TipoCodigoAutorizacion = result.VCI;
            pago.TipoPago = output.paymentTypeCode;
            pago.NumeroCuotas = output.sharesNumber; ;
            pago.CodigoComercio = output.commerceCode;

            if (output.responseCode == 0)
            {
                if (JsonConvert.DeserializeObject<bool>(Bcp.Facturacion.IngresarPago(pago).Content))
                {
                    var envio = await Bcp.Mail.EnviarMailPagoTransbank(infoPago.NombreCliente, "Comprobante de pago Secar Soluciones", infoPago.CorreoCliente, pago.NroOrden, infoPago.Comercio, "$" + pago.Monto.ToString("N0"),
                        pago.CodigoAutorizacion.ToString(), pago.FechaTransaccion, Bcp.TipoPagoWebPay.DescTipoPago(output.paymentTypeCode), pago.NumeroCuotas.ToString(),
                        pago.NumeroTarjeta, infoPago.Servicio);
                }
            }

            ViewBag.AuthorizationCode = pago.CodigoAutorizacion;
            ViewBag.Amount = "$" + pago.Monto.ToString("N0");
            ViewBag.UrlRedirection = pago.UrlRedireccion;
            ViewBag.TokenWs = pago.Token;
            ViewBag.SessionId = pago.SessionId;
            ViewBag.CardNumber = pago.NumeroTarjeta;
            ViewBag.CardExpirationDate = result.cardDetail.cardExpirationDate == null ? "0" : result.cardDetail.cardExpirationDate;
            ViewBag.AccountingDate = pago.FechaAutorizacion;
            ViewBag.TransaccionDate = pago.FechaTransaccion;
            ViewBag.Vci = pago.TipoCodigoAutorizacion;
            ViewBag.PaymentTypeCode = pago.TipoPago;
            ViewBag.DescPaymentTypeCode = Bcp.TipoPagoWebPay.DescTipoPago(output.paymentTypeCode);
            ViewBag.SharesNumber = pago.NumeroCuotas;
            ViewBag.CommerceCode = pago.CodigoComercio;
            ViewBag.Commerce = infoPago.Comercio;
            ViewBag.Service = infoPago.Servicio;
            ViewBag.RutCliente = infoPago.RutCliente;

            return View();
        }

        public ActionResult Final()
        {
            ViewBag.Produccion = int.Parse(WebConfigurationManager.AppSettings["Produccion"]);
            return View();
        }

    }
}