using Legalizaciones.Interface.BaseRepository;
using Legalizaciones.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Legalizaciones.Interface
{
    public interface IProveedorRepository : IRepository<Proveedor>
    {
        IEnumerable<SelectListItem> listaProveedor();
    }
}
