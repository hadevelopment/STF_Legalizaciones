﻿using Legalizaciones.Interface.BaseRepository;
using Legalizaciones.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Legalizaciones.Interface.IEmpresa
{
    public interface ICompaniaRepository : IRepository<Model.Empresa.Compania>
    {
        IEnumerable<SelectListItem> CompaniaForDropdown();
    }
}
