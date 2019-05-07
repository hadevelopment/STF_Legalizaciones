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
    public class ServicioRepository : BaseRepository<ServicioDetalle>, IServicioRepository
    {
        public ServicioRepository(AppDataContext context) : base(context)
        {

        }
        public IEnumerable<SelectListItem> listaServiciosPorProveedor(int proveedorId = 0)
        {
            if (proveedorId == 0)
            {
                return All().ToList().Select(x => new SelectListItem()
                {
                    Text = x.Nombre,
                    Value = x.Id.ToString()
                });
            }
            return All().ToList().Where(x => x.ProveedorID == proveedorId).Select(x => new SelectListItem()
            {
                Text = x.Nombre,
                Value = x.Id.ToString()
            });
        }
    }
}
