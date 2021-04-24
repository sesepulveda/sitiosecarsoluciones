using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Web.SitioPublico.Models
{
    public class ResultFacturacion
    {
        public int IdFacturacion { get; set; }
        public string FechaDocumento { get; set; }
        public string Monto { get; set; }
        public string Servicio { get; set; }
        public string TipoDocumento { get; set; }
        public string Estado { get; set; }
    }
}