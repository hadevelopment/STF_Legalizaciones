﻿using Legalizaciones.Interface;
using Legalizaciones.Model;
using Legalizaciones.Data.AppDbContext;
using Legalizaciones.Data.Repository.Base;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Legalizaciones.Model.Workflow;

namespace Legalizaciones.Data.Repository
{
    public class HistoricoLegalizacionRepository : BaseRepository<HistoricoLegalizacion>, IHistoricoLegalizacionRepository
    {
        public HistoricoLegalizacionRepository(AppDataContext context) :base(context)
        {

        }
    }
}