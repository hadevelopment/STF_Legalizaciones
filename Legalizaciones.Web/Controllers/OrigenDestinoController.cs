using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Legalizaciones.Interface.IJerarquia;
using Legalizaciones.Model.Jerarquia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Legalizaciones.Web.Controllers
{
    public class OrigenDestinoController : Controller
    {
        private readonly IOrigenDestinoRepository origenDestinoRepository;
        private readonly IPaisRepository paisRepository;

        public OrigenDestinoController(
            IOrigenDestinoRepository origenDestinoRepository,
            IPaisRepository paisRepository
        )
        {
            this.origenDestinoRepository = origenDestinoRepository;
            this.paisRepository = paisRepository;
        }

        public IActionResult Index()
        {
            List<OrigenDestino> OrigenDestino = origenDestinoRepository.All().Where(x => x.Estatus == 1).ToList();
            foreach (var item in OrigenDestino)
            {
                item.Pais = paisRepository.Find(long.Parse(item.PaisId.ToString()));
            }
            return View(OrigenDestino);
        }

        /*     ****************************************************************************
       *     Descripcion: VISTA para acceder a datos de la Solicitud. 
       *     Creada: 05-06-2019 
       *     Autor: Javier Rodriguez    
             **************************************************************************** */

        [HttpPost]
        [Route("Guardar")]
        [ValidateAntiForgeryToken]
        public ActionResult Guardar([Bind] OrigenDestino origenDestino)
        {
            if (ModelState.IsValid)
            {
                origenDestino.Estatus = 1;
                
                if (origenDestino.Id == 0)
                {
                    origenDestino.FechaCreacion = DateTime.Today;
                    origenDestinoRepository.Insert(origenDestino);
                }
                else
                {
                    origenDestinoRepository.Update(origenDestino);
                }
            }

            return RedirectToAction("Index", "OrigenDestino");
        }


        /*     ****************************************************************************
        *     Descripcion: VISTA para acceder a datos de la Solicitud. 
        *     Creada: 05-06-2019 
        *     Autor: Javier Rodriguez    
              **************************************************************************** */

        [HttpGet]
        [Route("Visualizar")]
        public ActionResult Visualizar(int id)
        {
            var origenDestino = origenDestinoRepository.Find(id);
            origenDestino.Pais = paisRepository.Find(long.Parse(origenDestino.PaisId.ToString()));

            return View(origenDestino);
        }


        /*    ****************************************************************************
*     Descripcion: VISTA para acceder a datos de la Solicitud. 
*     Creada: 05-07-2019 
*     Autor: Javier Rodriguez    
     **************************************************************************** */

        [HttpGet]
        [Route("OrigenDestino/Crear")]
        public ActionResult Crear()
        {
            var origenDestino = new OrigenDestino();
            origenDestino.Id = 0;

            return View(origenDestino);
        }

        /*     ****************************************************************************
       *     Descripcion: VISTA para acceder a datos de la Solicitud. 
       *     Creada: 05-06-2019 
       *     Autor: Javier Rodriguez    
             **************************************************************************** */

        [HttpGet]
        [Route("OrigenDestino/Editar")]
        public ActionResult Editar(int id)
        {
            var origenDestino = origenDestinoRepository.Find(id);
            origenDestino.Pais = paisRepository.Find(long.Parse(origenDestino.PaisId.ToString()));

            return View(origenDestino);
        }


        /*     ****************************************************************************
      *     Descripcion: VISTA para eliminar logicamente. 
      *     Creada: 05-06-2019 
      *     Autor: Javier Rodriguez    
            **************************************************************************** */

        [HttpPost]
        [Route("OrigenDestino/Eliminar")]
        public ActionResult Eliminar([Bind] OrigenDestino origenDestino)
        {
            origenDestino.Estatus = 0;
            origenDestinoRepository.Update(origenDestino);

            return RedirectToAction("Index", "OrigenDestino");
        }

        [HttpPost]
        [Route("OrigenDestino/ImportData")]
        public ActionResult ImportData()
        {
            IFormFile file = Request.Form.Files[0];
            string pathFiles = Directory.GetCurrentDirectory() + "\\wwwroot\\files\\";
            string fullPath = Path.Combine(pathFiles, file.FileName);
            StringBuilder sb = new StringBuilder();
            if (file.Length > 0)
            {
                string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                ISheet sheet;

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    OrigenDestino origenDestino;
                    file.CopyTo(stream);
                    stream.Position = 0;

                    XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                    sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   

                    //if (sFileExtension == ".xls")
                    //{
                    //    try
                    //    {
                    //        HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                    //        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                    //    }
                    //    catch (Exception)
                    //    {
                    //        XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                    //        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                    //    }
                    //}
                    //else
                    //{
                    //    XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                    //    sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                    //}

                    IRow headerRow = sheet.GetRow(0); //Get Header Row
                    int cellCount = headerRow.LastCellNum;

                    sb.Append("<table id='tblZonas' class='table table - bordered table - hover myDataTable'>");
                    sb.Append("<thead><tr><th>Id</th><th> Nombre </th><th> Pais </th></tr></thead>");

                    sb.Append("<tr>");
                    for (int i = 1; i <= sheet.LastRowNum; i++) //Read Excel File
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;
                        if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;

                        origenDestino = new OrigenDestino()
                        {
                            Nombre = row.GetCell(0).ToString(),
                            PaisId = paisRepository.All()
                                .FirstOrDefault(x => x.Nombre.Contains(row.GetCell(1).ToString())).Id,
                            Estatus = 1
                        };

                        origenDestinoRepository.Insert(origenDestino);

                        sb.Append("<td>" + i.ToString() + "</td>");
                        sb.Append("<td>" + row.GetCell(0).ToString() + "</td>");
                        sb.Append("<td>" + row.GetCell(1).ToString() + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</tr></table>");
                }
            }

            //return this.Content("<thead><tr><td>datos</td><td>datos</td></tr></thead><tbody><tr><td>datos</td><td>datos</td></tr></tbody>");
            return this.Content(sb.ToString());
        }
    }
}