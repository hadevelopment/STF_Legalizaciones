using Microsoft.AspNetCore.Hosting;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Microsoft.AspNetCore.Http;

namespace Legalizaciones.Web.Helpers
{
    public class ImportExportModel
    {
        private string _Scheme;
        private HostString _Host;
        private string _WebRootPath;

        public string Scheme
        {
            get { return _Scheme;  }
            set { _Scheme = value; }
        }

        public HostString Host
        {
            get { return _Host; }
            set { _Host = value; }
        }

        public string WebRootPath
        {
            get { return _WebRootPath; }
            set { _WebRootPath = value; }
        }

        public MemoryStream OnPostExport(string sFileName)
        {
            string sWebRootFolder = Directory.GetCurrentDirectory() + "\\wwwroot\\files\\";
            string URL = string.Format("{0}://{1}/{2}", _Scheme, _Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            var memory = new MemoryStream();
            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Demo");
                IRow row = excelSheet.CreateRow(0);

                row.CreateCell(0).SetCellValue("ID");
                row.CreateCell(1).SetCellValue("Name");
                row.CreateCell(2).SetCellValue("Age");

                row = excelSheet.CreateRow(1);
                row.CreateCell(0).SetCellValue(1);
                row.CreateCell(1).SetCellValue("Kane Williamson");
                row.CreateCell(2).SetCellValue(29);

                row = excelSheet.CreateRow(2);
                row.CreateCell(0).SetCellValue(2);
                row.CreateCell(1).SetCellValue("Martin Guptil");
                row.CreateCell(2).SetCellValue(33);

                row = excelSheet.CreateRow(3);
                row.CreateCell(0).SetCellValue(3);
                row.CreateCell(1).SetCellValue("Colin Munro");
                row.CreateCell(2).SetCellValue(23);

                workbook.Write(fs);
            }
            using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
            {
                stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return memory;
        }
    }
}
