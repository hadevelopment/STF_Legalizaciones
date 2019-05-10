using Legalizaciones.Interface;
using Legalizaciones.Model;
using Legalizaciones.Data.AppDbContext;
using Legalizaciones.Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Legalizaciones.Data.Repository
{
    public class EmpleadoPermisoRepository : BaseRepository<EmpleadoPermiso>, IEmpleadoPermisoRepository
    {
        public EmpleadoPermisoRepository(AppDataContext context) : base(context)
        {

        }
    }
}
