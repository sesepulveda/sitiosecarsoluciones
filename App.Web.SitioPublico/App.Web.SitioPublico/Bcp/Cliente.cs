using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Web.SitioPublico.Bcp
{
    public class Cliente
    {
        public static IRestResponse Obtener(int rutCliente,int idEmpresa)
        {
            var client = new RestClient("https://secarsolucionesapp.cl/Cliente/Obtener");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("rutCliente", rutCliente);
            request.AddParameter("idEmpresa", idEmpresa);

            return client.Execute(request);
        }
    }
}