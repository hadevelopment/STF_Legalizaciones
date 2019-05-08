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
    public class ProductoRepository : BaseRepository<Producto>, IProductoRepository
    {
        public ProductoRepository(AppDataContext context) : base(context)
        {

        }
        public IEnumerable<SelectListItem> listaProductosPorCategoria(int categoryId = 0)
        {
            if (categoryId == 0)
            {
                return All().ToList().Select(x => new SelectListItem()
                {
                    Text = x.Nombre,
                    Value = x.Id.ToString()
                });
            }
            return All().ToList().Where(x => x.CategoriaID == categoryId).Select(x => new SelectListItem()
            {
                Text = x.Nombre,
                Value = x.Id.ToString()
            });
        }
    }
}
