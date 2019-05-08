using Legalizaciones.Interface;
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
    public class MatrizAprobacionesRepository : BaseRepository<MatrizAprobacion>, IMatrizAprobacionesRepository
    {
        public MatrizAprobacionesRepository(AppDataContext context) :  base(context)
        {

        }

        public IEnumerable<SelectListItem> MatrizAprobacionesForDropdown()
        {
            return All().ToList().Select(x => new SelectListItem()
            {
                Text = x.Nombre,
                Value = x.Id.ToString(),
            });
        }
    }
}
