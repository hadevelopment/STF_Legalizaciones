using Legalizaciones.Data.AppDbContext;
using Legalizaciones.Data.Repository.Base;
using Legalizaciones.Interface.ISolicitud;
using Legalizaciones.Model.ItemSolicitud;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace Legalizaciones.Data.Repository
{

    public class TasaRepository : BaseRepository<Tasa>, ITasaRepository
    {

        public TasaRepository(AppDataContext context) : base(context)
        {

        }


      
    }
}
