using Legalizaciones.Web.Engine;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Legalizaciones.Web.Models;

namespace Legalizaciones.Web.Helpers
{
    public interface IEmail
    {
      Boolean SendEmail(StructureMail model);
    }

    public class Email : IEmail
    {
        private readonly IHostingEnvironment env;

        public Email(IHostingEnvironment env)
        {
           this.env = env;
        }

        public Boolean SendEmail(StructureMail model)
        {
            model.Body = System.IO.Path.Combine(env.WebRootPath, "EmailTemplate", "TemplateEmail.cshtml");
            EngineMailSend Enviar = new EngineMailSend(model);
            bool resultado = Enviar.EnviarMail();
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
