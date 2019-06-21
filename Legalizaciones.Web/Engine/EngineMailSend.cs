using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Legalizaciones.Web.Models;

namespace Legalizaciones.Web.Engine
{
    public class EngineMailSend
    {
        private string Asunto { get; set; }
        private string Cuerpo { get; set; }
        private string RutaArchivoAdjunto { get; set; }
        private List<string> MensajePara { get; set; }
        private string ErrorSend { get; set; }


        public EngineMailSend() { }

        public EngineMailSend(StructureMail model)
        {
            this.Asunto = model.Subject;
            this.Cuerpo = File.ReadAllText(model.Body);
            this.Cuerpo = ReplaceParameters(model, this.Cuerpo);
            this.RutaArchivoAdjunto = model.PathAdjunto;
            this.MensajePara = model.Destinatario;
        }

        public bool EnviarMail()
        {
            bool resultado = false;
            if (this.Asunto == string.Empty && this.Cuerpo == string.Empty && this.MensajePara.Count > 0)
            {
                ErrorSend = "La notificacion debe contener : Asunto , Cuerpo y Destinatario";
                return resultado;
            }
            try
            {
                MailMessage mensaje = new MailMessage();
                mensaje.From = new MailAddress("Studio F Notificaciones <studiofnotificaciones@gmail.com>");
                mensaje.Subject = Asunto;
                mensaje.SubjectEncoding = System.Text.Encoding.UTF8;
                mensaje.Body = Cuerpo;
                mensaje.BodyEncoding = System.Text.Encoding.UTF8;
                mensaje.IsBodyHtml = true;
                mensaje = SetMsjPara(mensaje);
                if (RutaArchivoAdjunto != string.Empty)
                    mensaje.Attachments.Add(new Attachment(RutaArchivoAdjunto));
                SmtpClient servidor = new SmtpClient();
                servidor.Credentials = new System.Net.NetworkCredential("studiofnotificaciones", "studiof1234");
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

        private MailMessage SetMsjPara(MailMessage mensaje)
        {
            foreach (string email in this.MensajePara)
            { 
                if (EmailEsValido(email))
                   mensaje.To.Add(new MailAddress(email));
            }
            return mensaje;
        }

        private bool EmailEsValido(string email)
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

        private string ReplaceParameters(StructureMail model, string cuerpo)
        {
            cuerpo = cuerpo.Replace("@Model.Fecha", model.Fecha);
            cuerpo = cuerpo.Replace("@Model.NombreDestinatario", model.NombreDestinatario);
            cuerpo = cuerpo.Replace("@Model.TipoDocumento", model.TipoDocumento);
            cuerpo = cuerpo.Replace("@Model.NumeroDocumento",model.NumeroDocumento);
            cuerpo = cuerpo.Replace("@Model.NombreEmpleado", model.NombreEmpleado);
            cuerpo = cuerpo.Replace("@Model.CedulaEmpleado", model.CedulaEmpleado);
            cuerpo = cuerpo.Replace("@Model.CargoEmpleado", model.CargoEmpleado);
            cuerpo = cuerpo.Replace("@Model.Concepto", model.Concepto);
            cuerpo = cuerpo.Replace("@Model.Zona", model.Zona);
            cuerpo = cuerpo.Replace("@Model.Moneda", model.Moneda);
            cuerpo = cuerpo.Replace("@Model.Monto", model.Monto);
            cuerpo = cuerpo.Replace("@Model.DatosStf", model.DatosStf);
            return cuerpo;
        }

        public string ErrorEnviando ()
        {
            return ErrorSend;
        }

    }
}
