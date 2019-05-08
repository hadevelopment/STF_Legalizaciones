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
                    item.Id = Convert.ToInt32(row[1]);
                list.Insert(n, item);
                n++;
            }
            return list;
        }
    }
}
