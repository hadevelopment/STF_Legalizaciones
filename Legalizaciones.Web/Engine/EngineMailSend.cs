using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Legalizaciones.Web.Engine
{
    public class EngineMailSend
    {

        public string Asunto { get; set; }
        public string Cuerpo { get; set; }
        public string RutaArchivoAdjunto { get; set; }
        public List<string> MensajePara { get; set; }
        private string ErrorSend { get; set; }
        //string patrh = System.Web.Hosting.HostingEnviroment.MapPath();
        private readonly IHostingEnvironment env;
        string path = string.Empty;

        public EngineMailSend(IHostingEnvironment _env )
        {
            this.env = _env;
        }

        public EngineMailSend(IHostingEnvironment _env , string subject, string body , string pathAdjunto, List<string> msjTo)
        {
            this.env = _env;
            this.Asunto = subject;
            this.Cuerpo = body;
            this.RutaArchivoAdjunto = pathAdjunto;
            this.MensajePara = msjTo;
            path = Path.Combine(env.WebRootPath, "EmailTemplate", "AprobacionSolicitudAnt.html");
        }

        public bool EnviarMail()
        {
            bool resultado = false;
            string template = path;
            try
            {
                MailMessage mensaje = new MailMessage();
                mensaje.From = new MailAddress("efrainmejiasc@gmail.com");
                mensaje.Subject = Asunto;
                mensaje.SubjectEncoding = System.Text.Encoding.UTF8;
                mensaje.Body = Cuerpo;
                mensaje.BodyEncoding = System.Text.Encoding.UTF8;
                mensaje.IsBodyHtml = true;
                mensaje = SetMsjPara(mensaje);
                if (RutaArchivoAdjunto != string.Empty)
                    mensaje.Attachments.Add(new Attachment(RutaArchivoAdjunto));
                SmtpClient servidor = new SmtpClient();
                servidor.Credentials = new System.Net.NetworkCredential("efrainmejiasc", "1234santiago");
                servidor.Port = 587;
                servidor.Host = "smtp.gmail.com";
                servidor.EnableSsl = true;
                servidor.Send(mensaje);
                mensaje.Dispose();
                resultado = true;
            }
            catch(Exception ex)
            {
                this.ErrorSend  = ex.ToString();
            }
            return resultado;
        }

        public MailMessage SetMsjPara( MailMessage mensaje )
        {
            foreach (string email in this.MensajePara)
            { 
                if (EmailEsValido(email))
                   mensaje.To.Add(new MailAddress(email));
            }
            return mensaje;
        }

        public bool EmailEsValido(string email)
        {
            string expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            bool resultado = false;
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                    resultado = true;    
            }
            return resultado;
        }

        public string ErrorEnviando ()
        {
            return ErrorSend;
        }
    }
}
