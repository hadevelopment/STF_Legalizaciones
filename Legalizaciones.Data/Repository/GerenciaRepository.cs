﻿using Legalizaciones.Interface.IEmpresa;
using Legalizaciones.Model.Empresa;
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
    public class GerenciaRepository : BaseRepository<Gerencia>, IGerenciaRepository
    {
        public GerenciaRepository(AppDataContext context) : base(context)
        {

        }

        public IEnumerable<SelectListItem> GerenciaForDropdown()
        {
            return All().ToList().Select(x => new SelectListItem()
            {
                Text = x.Nombre,
                Value = x.Id.ToString()
            });
        }
    }
}