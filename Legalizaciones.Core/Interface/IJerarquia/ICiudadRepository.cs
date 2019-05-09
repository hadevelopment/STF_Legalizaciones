using Legalizaciones.Interface.BaseRepository;
using Legalizaciones.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Legalizaciones.Interface.IJerarquia
{
    public interface ICiudadRepository : IRepository<Model.Jerarquia.Ciudad>
    {
        IEnumerable<SelectListItem> listaCiudades();
    }
}