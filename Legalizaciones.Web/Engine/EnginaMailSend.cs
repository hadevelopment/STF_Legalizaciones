using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Legalizaciones.Web.Engine
{
    public class EnginaMailSend
    {
        public string Asunto { get; set; }
        public string Cuerpo { get; set; }
        public string RutaArchivoAdjunto { get; set; }
        private string ErrorSend { get; set; }


        public bool EnviarMail()
        {
            bool resultado = false;
            try
            {
                MailMessage mensaje = new MailMessage();
                SmtpClient servidor = new SmtpClient();
                mensaje.From = new MailAddress("efrainmejiasc@gmail.com");
                mensaje.Subject = Asunto;
                mensaje.SubjectEncoding = System.Text.Encoding.UTF8;
                mensaje.Body = Cuerpo;
                mensaje.BodyEncoding = System.Text.Encoding.UTF8;
                mensaje.IsBodyHtml = true;
                mensaje.To.Add(new MailAddress("efrainmejiasc@gmail.com"));
                if (RutaArchivoAdjunto != string.Empty) { mensaje.Attachments.Add(new Attachment(RutaArchivoAdjunto)); }
                servidor.Credentials = new System.Net.NetworkCredential("efrainmejiasc", "1234santiago");
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

        public string ErrorEnviando ()
        {
            return ErrorSend;
        }
    }
}
