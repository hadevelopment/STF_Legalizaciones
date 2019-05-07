﻿using Legalizaciones.Interface.BaseRepository;
using Legalizaciones.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Legalizaciones.Interface.ISolicitud
{
    public interface IEstadoSolicitudRepository : IRepository<EstadoSolicitud>
    {
       // IEnumerable<SelectListItem> listaEstados();
    }
}