using Legalizaciones.Interface;
using Legalizaciones.Model;
using Legalizaciones.Data.AppDbContext;
using Legalizaciones.Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Legalizaciones.Data.Repository
{
    public class SolicitudAprobadorRepository : BaseRepository<SolicitudAprobador>, ISolicitudAprobadorRepository
    {
        public SolicitudAprobadorRepository(AppDataContext context) : base(context)
        {

        }
    }
}
