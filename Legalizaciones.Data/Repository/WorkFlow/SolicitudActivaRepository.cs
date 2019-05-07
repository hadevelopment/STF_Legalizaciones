using Legalizaciones.Interface.WorkFlow;
using Legalizaciones.Model.WorkFlow;
using Legalizaciones.Data.AppDbContext;
using Legalizaciones.Data.Repository.Base;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Legalizaciones.Data.Repository.WorkFlow
{
    public class SolicitudActivaRepository : BaseRepository<SolicitudActiva>, ISolicitudActivaRepository
    {
        public SolicitudActivaRepository(AppDataContext context) : base(context)
        {
        }
    }
}

