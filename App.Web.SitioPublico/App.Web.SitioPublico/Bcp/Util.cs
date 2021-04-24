using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Web.SitioPublico.Bcp
{
    public class Util
    {
        public DateTime FechaActual
        {
            get
            {
                return DateTime.Now.ToUniversalTime().AddHours(int.Parse(System.Configuration.ConfigurationManager.AppSettings["ZonaHoraria"]));
            }
        }
    }
}