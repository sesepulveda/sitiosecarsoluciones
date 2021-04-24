using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Web.SitioPublico.Bcp
{
    public class Session
    {
        public const string SessionInfoPago = "SessionInfoPago";

        public static Object Obtener(string session)
        {
            return (Object)System.Web.HttpContext.Current.Session[session];
        }

        public static void Cerrar(string session)
        {
            System.Web.HttpContext.Current.Session[session] = null;
        }

        public static void Crear(string session, Object obj)
        {
            System.Web.HttpContext.Current.Session[session] = obj;
        }
    }
}