﻿using Legalizaciones.Interface.BaseRepository;
using Legalizaciones.Model.WorkFlow;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Legalizaciones.Interface
{
    public interface IMatrizAprobacionesRepository : IRepository<MatrizAprobacion>
    {
        IEnumerable<SelectListItem> MatrizAprobacionesForDropdown();
    }
}
