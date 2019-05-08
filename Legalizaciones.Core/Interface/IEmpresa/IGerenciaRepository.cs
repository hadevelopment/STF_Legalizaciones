using Legalizaciones.Interface.BaseRepository;
using Legalizaciones.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Legalizaciones.Interface.IEmpresa
{
    public interface IGerenciaRepository : IRepository<Model.Empresa.Gerencia>
    {
        IEnumerable<SelectListItem> GerenciaForDropdown();
    }
}

