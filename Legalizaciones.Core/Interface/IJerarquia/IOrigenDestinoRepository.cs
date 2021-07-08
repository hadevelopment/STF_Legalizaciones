using System.Collections.Generic;
using Legalizaciones.Interface.BaseRepository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Legalizaciones.Interface.IJerarquia
{
    public interface IOrigenDestinoRepository : IRepository<Model.Jerarquia.OrigenDestino>
    {
        IEnumerable<SelectListItem> listaOrigenDestino();
    }
}
