using Legalizaciones.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Legalizaciones.Web.Models.ViewModel
{
    public class HomeViewModel
    {
        public SelectList ListaEmpleados { get; set; }

        public string Cedula { get; set; }

    }
}
