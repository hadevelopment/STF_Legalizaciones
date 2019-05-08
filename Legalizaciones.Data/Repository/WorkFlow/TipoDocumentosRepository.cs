using Legalizaciones.Interface.WorkFlow;
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
    public class TipoDocumentosRepository : BaseRepository<TipoDocumento>, ITipoDocumentosRepository
    {
        public TipoDocumentosRepository(AppDataContext context) : base(context)
        {

        }
    }
}

