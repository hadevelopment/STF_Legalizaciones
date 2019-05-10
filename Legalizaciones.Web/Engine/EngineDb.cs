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
        public static string DefaultConnection { get; set; }
        private string StringConexion = EngineDb.DefaultConnection;

        public List<InfoLegalizacion> SolicitudesAntPendientesLegalizacion (string SpName,string empleadoCedula)
        {
            List<InfoLegalizacion> InfoLegalizacion = new List<InfoLegalizacion>();
            DataTable dataTabla = new DataTable();
            SqlConnection Conexion = new SqlConnection(StringConexion);
            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand(SpName, Conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@EmpleadoCedula", empleadoCedula);
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
