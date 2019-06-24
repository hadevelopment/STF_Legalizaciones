using Legalizaciones.Interface.BaseRepository;
using Legalizaciones.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Legalizaciones.Interface
{
    public interface IKactusEmpleadoRepository : IRepository<KactusEmpleado>
    {
        IEnumerable<SelectListItem> listaEmpleados();
        KactusEmpleado getEmpleadoID(int idEmpleado);
        KactusEmpleado getEmpleadoCedula(string cedula);
    }
}
