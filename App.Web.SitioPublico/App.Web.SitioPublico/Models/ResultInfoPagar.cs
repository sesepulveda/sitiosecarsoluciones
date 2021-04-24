using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Web.SitioPublico.Models
{
    public class ResultInfoPagar
    {
        public int IdFacturacion { get; set; }
        public int Monto { get; set; }
        public string SessionId { get; set; }
        public string Vencimiento { get; set; }
        public string Servicio { get; set; }
        public string Comercio { get; set; }
        public string NroOrden { get; set; }
        public string CodigoTransabank { get; set; }
        public int RutCliente { get; set; }
        public string NombreCliente { get; set; }
        public string CorreoCliente { get; set; }
    }
}