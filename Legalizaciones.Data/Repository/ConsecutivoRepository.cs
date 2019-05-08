using Legalizaciones.Interface.ISolicitud;
using Legalizaciones.Model.ItemSolicitud;
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
    public class ConsecutivoRepository : BaseRepository<ConsecutivoSolicitud>, IConsecutivoRepository
    {
        public ConsecutivoRepository(AppDataContext context) : base(context)
        {

        }

        public ConsecutivoSolicitud getConsecutivo()
        {
            return new ConsecutivoSolicitud();
        }
    }
}