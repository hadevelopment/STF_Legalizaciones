﻿using Legalizaciones.Interface.IJerarquia;
using Legalizaciones.Model.Jerarquia;
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
    public class CiudadRepository : BaseRepository<Ciudad>, ICiudadRepository
    {
        public CiudadRepository(AppDataContext context) : base(context)
        {

        }

        public IEnumerable<SelectListItem> listaCiudades()
        {
            return All().ToList().Select(x => new SelectListItem()
            {
                Text = x.Nombre,
                Value = x.Id.ToString()
            });
        }
    }
}

