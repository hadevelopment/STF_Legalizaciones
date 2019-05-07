using Legalizaciones.Interface;
using Legalizaciones.Model;
using Legalizaciones.Data.AppDbContext;
using Legalizaciones.Data.Repository.Base;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Legalizaciones.Data.Repository
{
    public class FlujoAprobacionesRepository : BaseRepository<FlujoAprobacion>, IFlujoAprobacionesRepository
    {
        public FlujoAprobacionesRepository(AppDataContext context) :base(context)
        {

        }
    }
}
