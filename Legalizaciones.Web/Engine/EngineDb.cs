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

        public List<InfoLegalizacion> SolicitudesAntPendientesLegalizacion (string SpName,string empleadoCedula = "")
        {
            List<InfoLegalizacion> InfoLegalizacion = new List<InfoLegalizacion>();
            try
            {

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
            }
            catch { }
           
            return InfoLegalizacion;
        }

        public List <string> TiposDocumentos (string SpName)
        {
            List<string> documento = new List<string>();
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
            documento = Funcion.TiposDocumentos(dataTabla);
            return documento;
        }

        public List<DataAprobacion> AprobadoresTipoSolicitud (string SpName , string tipoSolicitud,int estatus = 1 )
        {
            SqlConnection Conexion = new SqlConnection(StringConexion);
            List<DataAprobacion> dataList = new List<DataAprobacion>();
            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand(SpName, Conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@tipoSolicitud", tipoSolicitud);
                command.Parameters.AddWithValue("@estatus", estatus);
                DataTable dt = new DataTable();
                SqlDataReader lector = command.ExecuteReader();
                int n = 0;
                while (lector.Read())
                {
                    DataAprobacion data = new DataAprobacion();
                    data.CedulaAprobador = lector.GetString(0);
                    data.NombreAprobador = lector.GetString(1);
                    data.EmailAprobador = lector.GetString(2);
                    data.Orden = lector.GetInt32(3);
                    data.Descripcion = lector.GetString(4);
                    dataList.Insert(n, data);
                    n++;
                }
                lector.Close();
                Conexion.Close();
            }
            return dataList;
        }
    }
}
