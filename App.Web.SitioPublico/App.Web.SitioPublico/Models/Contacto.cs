using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Web.SitioPublico.Models
{
    public class Contacto
    {
        public string Mail { get; set; }
        public string Nombre { get; set; }
        public string Mensaje { get; set; }
        public string Asunto { get; set; }
        public string AsuntoMail { get; set; }
    }
}