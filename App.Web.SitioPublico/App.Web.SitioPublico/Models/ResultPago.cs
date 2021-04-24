using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Web.SitioPublico.Models
{
    public class ResultPago
    {
        public int IdIngreso { get; set; }
        public int IdFacturacion { get; set; }
        public DateTime Fecha { get; set; }
        public int IdTipoMedioPago { get; set; }
        public int IdDetalleIngreso { get; set; }
        public int IdTipoBanco { get; set; }
        public string NroCuenta { get; set; }
        public int Monto { get; set; }
        public string NroOrden { get; set; }
        public int CodigoRespuesta { get; set; }
        public string CodigoAutorizacion { get; set; }
        public string UrlRedireccion { get; set; }
        public string Token { get; set; }
        public string SessionId { get; set; }
        public string NumeroTarjeta { get; set; }
        public string FechaAutorizacion { get; set; }
        public string FechaTransaccion { get; set; }
        public string TipoCodigoAutorizacion { get; set; }
        public string TipoPago { get; set; }
        public int NumeroCuotas { get; set; }
        public string CodigoComercio { get; set; }
    }
}