using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Web.SitioPublico.Bcp
{
    public class TipoPagoWebPay
    {
        public static string DescTipoPago(string tipoPago)
        {
            switch(tipoPago)
            {
                case "VD":
                    return "Venta Debito";
                case "VN":
                    return "Venta Normal";
                case "VC":
                    return "Venta en cuotas";
                case "SI":
                    return "3 Cuotas sin interees";
                case "S2":
                    return "2 Cuotas sin interees";
                case "NC":
                    return "Cuotas sin interes";
                case "VP":
                    return "Venta Prepago";
                default:
                    return "";
            }
        }
    }
}