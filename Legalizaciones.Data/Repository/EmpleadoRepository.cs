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
    public class EmpleadoRepository : BaseRepository<Empleado>, IEmpleadoRepository
    {
        public EmpleadoRepository(AppDataContext context) : base(context)
        {

        }

        public IEnumerable<SelectListItem> listaEmpleados()
        {
            return All().ToList().Select(x => new SelectListItem()
            {
                Text = x.Nombre + " " + x.Apellido,
                Value = x.Id.ToString()
            });
        }
    }
}
