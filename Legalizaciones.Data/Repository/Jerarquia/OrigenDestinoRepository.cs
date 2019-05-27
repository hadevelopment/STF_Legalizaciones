using Legalizaciones.Data.AppDbContext;
using Legalizaciones.Data.Repository.Base;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Legalizaciones.Interface.IJerarquia;
using Legalizaciones.Model.Jerarquia;

namespace Legalizaciones.Data.Repository.Jerarquia
{
    public class OrigenDestinoRepository : BaseRepository<OrigenDestino>, IOrigenDestinoRepository
    {
        public OrigenDestinoRepository(AppDataContext context) : base(context)
        {

        }

        public IEnumerable<SelectListItem> listaOrigenDestino()
        {
            return All().ToList().Select(x => new SelectListItem()
            {
                Text = x.Nombre,
                Value = x.Id.ToString()
            });
        }
    }
}
