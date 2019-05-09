using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Legalizaciones.Web.Helpers
{
    public class Alerta
    {
        /// <summary>
        /// error, info, success
        /// </summary>
        public string Estatus { get; set; }
        public string Mensaje { get; set; }

        public Alerta(string estatus, string mensaje)
        {
            this.Estatus = estatus;
            this.Mensaje = mensaje;
        }

    }
}
