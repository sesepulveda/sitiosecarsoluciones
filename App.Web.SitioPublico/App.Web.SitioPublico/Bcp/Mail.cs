using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace App.Web.SitioPublico.Bcp
{
    public class Mail
    {
        public static async Task<bool> EnviarMail(string nombre, string asunto, string mensaje, string correo)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string mailSmtp = (string)WebConfigurationManager.AppSettings["mailSmtp"];
            string servidorSmtp = (string)WebConfigurationManager.AppSettings["servidorSmtp"];
            int puertoSmtp = int.Parse(WebConfigurationManager.AppSettings["puertoSmtp"]);
            string passMailSmtp = (string)WebConfigurationManager.AppSettings["passMailSmtp"];

            try
            {
                MailMessage msg = new MailMessage();
                msg.To.Add(new MailAddress(correo));
                msg.From = new MailAddress(mailSmtp);
                msg.IsBodyHtml = true;
                msg.Subject = asunto;
                msg.Body = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "BodyMail/bodyContacto.html").ReadToEnd()
                    .Replace("[NOMBRE]", nombre)
                    .Replace("[MENSAJE]", mensaje);

                SmtpClient clienteSmtp = new SmtpClient(servidorSmtp, puertoSmtp);
                clienteSmtp.Credentials = new NetworkCredential(mailSmtp, passMailSmtp);
                clienteSmtp.EnableSsl = true;

                clienteSmtp.Send(msg);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static async Task<bool> EnviarMailPagoTransbank(string nombre, string asunto, string correo, string orden, string comercio,
            string monto,string codigo, string fecha, string medioPago,string cuotas, string tarjeta, string servicio)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string mailSmtp = (string)WebConfigurationManager.AppSettings["mailSmtp"];
            string servidorSmtp = (string)WebConfigurationManager.AppSettings["servidorSmtp"];
            int puertoSmtp = int.Parse(WebConfigurationManager.AppSettings["puertoSmtp"]);
            string passMailSmtp = (string)WebConfigurationManager.AppSettings["passMailSmtp"];

            try
            {
                MailMessage msg = new MailMessage();
                msg.To.Add(new MailAddress(correo));
                msg.From = new MailAddress(mailSmtp);
                msg.IsBodyHtml = true;
                msg.Subject = asunto;
                msg.Body = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "BodyMail/bodyPagoTransbank.html").ReadToEnd()
                    .Replace("[NOMBRE]", nombre)
                    .Replace("[NROORDEN]", orden)
                    .Replace("[COMERCIO]", comercio)
                    .Replace("[MONTO]", monto)
                    .Replace("[CODIGO]", codigo)
                    .Replace("[FECHA]", fecha)
                    .Replace("[MEDIOPAGO]", medioPago)
                    .Replace("[CUOTAS]", cuotas)
                    .Replace("[TARJETA]", tarjeta)
                    .Replace("[SERVICIO]", servicio);                    

                SmtpClient clienteSmtp = new SmtpClient(servidorSmtp, puertoSmtp);
                clienteSmtp.Credentials = new NetworkCredential(mailSmtp, passMailSmtp);
                clienteSmtp.EnableSsl = true;

                clienteSmtp.Send(msg);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}