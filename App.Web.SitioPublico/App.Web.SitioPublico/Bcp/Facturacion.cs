using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Web.SitioPublico.Bcp
{
    public class Facturacion
    {
        public static IRestResponse ObtenerFacturacion(int rutCliente, int idEmpresa)
        {
            var client = new RestClient("https://secarsolucionesapp.cl/Cliente/ObtenerFacturacion");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("rutCliente", rutCliente);
            request.AddParameter("idEmpresa", idEmpresa);

            return  client.Execute(request);            
        }

        public static IRestResponse ObtenerInfoPagar(int idFacturacion)
        {
            var client = new RestClient("https://secarsolucionesapp.cl/Facturacion/ObtenerInfoApagar");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("idFacturacion", idFacturacion);

            return client.Execute(request);
        }

        public static IRestResponse IngresarPago(Models.ResultPago pago)
        {
            var client = new RestClient("https://secarsolucionesapp.cl/Ingreso/Pagar");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("IdIngreso", pago.IdIngreso);
            request.AddParameter("IdFacturacion", pago.IdFacturacion);
            request.AddParameter("Fecha", pago.Fecha);
            request.AddParameter("IdTipoMedioPago", pago.IdTipoMedioPago);
            request.AddParameter("IdDetalleIngreso", pago.IdDetalleIngreso);
            request.AddParameter("IdTipoBanco", pago.IdTipoBanco);
            request.AddParameter("NroCuenta", pago.NroCuenta);
            request.AddParameter("Monto", pago.Monto);
            request.AddParameter("NroOrden", pago.NroOrden);
            request.AddParameter("CodigoRespuesta", pago.CodigoRespuesta);
            request.AddParameter("CodigoAutorizacion", pago.CodigoAutorizacion);
            request.AddParameter("UrlRedireccion", pago.UrlRedireccion);
            request.AddParameter("Token", pago.Token);
            request.AddParameter("SessionId", pago.SessionId);
            request.AddParameter("NumeroTarjeta", pago.NumeroTarjeta);
            request.AddParameter("FechaAutorizacion", pago.FechaAutorizacion);
            request.AddParameter("FechaTransaccion", pago.FechaTransaccion);
            request.AddParameter("TipoCodigoAutorizacion", pago.TipoCodigoAutorizacion);
            request.AddParameter("TipoPago", pago.TipoPago);
            request.AddParameter("NumeroCuotas", pago.NumeroCuotas);
            request.AddParameter("CodigoComercio", pago.CodigoComercio);

            return client.Execute(request);
        }
    }
}