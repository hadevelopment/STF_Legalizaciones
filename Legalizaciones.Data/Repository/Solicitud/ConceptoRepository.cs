﻿using Legalizaciones.Interface.ISolicitud;
using Legalizaciones.Model.ItemSolicitud;
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
    public class ConceptoRepository : BaseRepository<Concepto>, IConceptoRepository
    {
        public ConceptoRepository(AppDataContext context) : base(context)
        {

        }

        public IEnumerable<SelectListItem> listaConceptos()
        {
            return All().ToList().Select(x => new SelectListItem()
            {
                Text = x.Nombre,
                Value = x.Id.ToString()
            });
        }
    }
}