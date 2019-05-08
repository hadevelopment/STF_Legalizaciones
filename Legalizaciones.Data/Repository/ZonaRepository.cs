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
using Legalizaciones.Model.Jerarquia;

namespace Legalizaciones.Data.Repository
{
    public class ZonaRepository : BaseRepository<Zona>, IZonaRepository
    {
        public ZonaRepository(AppDataContext context) : base(context)
        {

        }

        public IEnumerable<SelectListItem> listaZonas()
        {
            return All().ToList().Select(x => new SelectListItem()
            {
                Text = x.Nombre,
                Value = x.Id.ToString()
            });
        }
    }
}