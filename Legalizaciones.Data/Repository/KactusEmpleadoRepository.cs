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
    public class KactusEmpleadoRepository : BaseRepository<KactusEmpleado>, IKactusEmpleadoRepository
    {
        public KactusEmpleadoRepository(AppDataContext context) : base(context)
        {

        }

        public IEnumerable<SelectListItem> listaEmpleados()
        {
            return All().ToList().Select(x => new SelectListItem()
            {
                Text = x.PrimerNombre + " " + x.PrimerApellido,
                Value = x.NumeroDeIdentificacion.ToString()
            });
        }

        public KactusEmpleado getEmpleadoID(int idEmpleado)
        {
            return Find(idEmpleado);
        }
        public KactusEmpleado getEmpleadoCedula(string cedula)
        {
            return All().Where(m => m.NumeroDeIdentificacion == cedula).Single();
        }
    }
}
