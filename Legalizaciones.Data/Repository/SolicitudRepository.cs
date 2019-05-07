using Legalizaciones.Interface;
using Legalizaciones.Model;
using Legalizaciones.Data.AppDbContext;
using Legalizaciones.Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Legalizaciones.Data.Repository
{
    public class SolicitudRepository : BaseRepository<Solicitud>, ISolicitudRepository
    {
        public SolicitudRepository(AppDataContext context) : base(context)
        {

        }

    }
}
