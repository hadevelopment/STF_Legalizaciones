using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Legalizaciones.Web.Models;

namespace Legalizaciones.Web.Engine
{
    public class EngineStf
    {

        public List<InfoLegalizacion> ConvertirToListSolicitud(DataTable dt)
        {
            List<InfoLegalizacion> list = new List<InfoLegalizacion>();
            int n = 0;
            foreach (DataRow row in dt.Rows)
            {
                InfoLegalizacion item = new InfoLegalizacion();
                if (row[0] != DBNull.Value) 
                    item.Id = Convert.ToInt32(row[0]);
                if (row[1] != DBNull.Value)
                    item.FechaCreacion = row[1].ToString().Substring(0, 10);
                if (row[2] != DBNull.Value)
                    item.NumeroSolicitud = row[2].ToString();
                if (row[3] != DBNull.Value)
                    item.Concepto = row[3].ToString();
                if (row[4] != DBNull.Value)
                    item.Monto = Convert.ToDouble(row[4]);
                if (row[5] != DBNull.Value)
                    item.Concepto = row[5].ToString();
                if (row[6] != DBNull.Value)
                    item.Tasa = Convert.ToDouble(row[6]);
                if (row[7] != DBNull.Value)
                    item.EmpleadoCedula = row[7].ToString();
                if (row[8] != DBNull.Value)
                    item.Estado = row[8].ToString();
                if (row[9] != DBNull.Value)
                    item.IdDocErp = Convert.ToInt32(row[9]);
                if (row[10] != DBNull.Value)
                    item.FechaEntrega = row[10].ToString().Substring(0, 10);
                if (row[11] != DBNull.Value)
                    item.FechaVencimiento = row[11].ToString().Substring(0, 10);
                if (row[12] != DBNull.Value)
                    item.Id = Convert.ToInt32(row[12]);
                list.Insert(n, item);
                n++;
            }
            return list;
        }
    }
}
