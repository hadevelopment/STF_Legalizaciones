using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Legalizaciones.Web.Models;

namespace Legalizaciones.Web.Engine
{
    public class EngineDb
    {
        private string StringConexion = "Data Source=sql7005.site4now.net;Initial Catalog=DB_A486E4_Legalizaciones;user id=DB_A486E4_Legalizaciones_admin; password=Innova4j";

        public List<InfoLegalizacion> ListadoClientGroup_Empresa(string SpName)
        {
            List<InfoLegalizacion> InfoLegalizacion = new List<InfoLegalizacion>();
            DataTable dataTabla = new DataTable();
            SqlConnection Conexion = new SqlConnection(StringConexion);
            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand(SpName, Conexion);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter dataAdaptador = new SqlDataAdapter(command);
                dataAdaptador.Fill(dataTabla);
                Conexion.Close();
            }
            EngineStf Funcion = new EngineStf();
            InfoLegalizacion = Funcion.ConvertirToListSolicitud(dataTabla);
            return InfoLegalizacion;
        }

        public int ContadorDeContratos(string SpName, DateTime fechaInicial, DateTime fechaFinal)
        {
            int count = 0;
            object obj = new object();
            SqlConnection Conexion = new SqlConnection(StringConexion);
            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand(SpName, Conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@fechaInicial", fechaInicial);
                command.Parameters.AddWithValue("@fechaFinal", fechaFinal);
                obj = command.ExecuteScalar();
                if (obj != null && obj != DBNull.Value)
                    count = Convert.ToInt32(obj);
                Conexion.Close();
            }
            return count;

        }
    }
}
