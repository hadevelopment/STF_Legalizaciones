using Legalizaciones.Web.Engine;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Legalizaciones.Web.Helpers
{
    public class Structure
    {
        public string NombreDestinatario { get; set; }
        public string NumeroDocumento { get; set; }
        public string Direccion { get; set; }
        public string Fecha { get; set; }
    }

    public interface IEmail
    {
        Boolean SendEmail(string Destinatario, string Nombre, string SolicitudId, string Direccion);
    }

    public class Email : IEmail
    {
        private readonly IHostingEnvironment env;

        public Email(IHostingEnvironment env)
        {
            this.env = env;
        }

        public Boolean SendEmail(string Destinatario, string Nombre, string SolicitudId, string Direccion)
        {
            List<string> listaDestino = new List<string>();
            //listaDestino.Add("d.sanchez@innova4j.com");
            //listaDestino.Add("efrainmejiasc@gmail.com");
            listaDestino.Add(Destinatario);
            //listaDestino.Add("ha.development.org@gmail.com");
            //listaDestino.Add("abetancourt@innova4j.com");

            Structure model = new Structure
            {
                Fecha = DateTime.Now.ToString("dd/MM/yyyy"),
                NombreDestinatario = Nombre,
                NumeroDocumento = SolicitudId,
                Direccion = Direccion
            };
            //****************************************************************************************************
            string body = System.IO.Path.Combine(env.WebRootPath, "EmailTemplate", "TemplateEmail.cshtml");
            EngineMailSend Enviar = new EngineMailSend("Prueba Notificacion STF", body, string.Empty, listaDestino, model);
            bool resultado = Enviar.EnviarMail();
            //*****************************************************************************************************
            string msjGet = string.Empty;
            if (resultado)
            {
                msjGet = "Notificacion enviada satisfactoriamente";
                return true;
            }
            else
            {
                msjGet = Enviar.ErrorEnviando();
                return false;
            }
        }
    }
}
