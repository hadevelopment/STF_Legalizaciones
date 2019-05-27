using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Legalizaciones.Web.Models;
using Legalizaciones.Model;
using Legalizaciones.Model.Workflow;

namespace Legalizaciones.Web.Engine
{
    public class EngineDb
    {
        public static string DefaultConnection { get; set; }
        private SqlConnection Conexion = new SqlConnection(EngineDb.DefaultConnection);


        public List<InfoLegalizacion> SolicitudesAntPendientesLegalizacion (string SpName,string empleadoCedula)
        {
            List<InfoLegalizacion> InfoLegalizacion = new List<InfoLegalizacion>();
            DataTable dataTabla = new DataTable();
            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand(SpName, Conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@EmpleadoCedula", "");
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

        public List<Flujo> ObtenerFlujoSolicitud(int SolicitudId)
        {
            List<Flujo> FlujoSolicitud = new List<Flujo>();
            DataTable dataTabla = new DataTable();
            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand("Sp_GetFlujoSolicitud", Conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@SolicitudId", SolicitudId);
                SqlDataAdapter dataAdaptador = new SqlDataAdapter(command);
                dataAdaptador.Fill(dataTabla);
                Conexion.Close();
            }
            EngineStf Funcion = new EngineStf();
            if(dataTabla.Rows.Count > 0)
            {
                FlujoSolicitud = Funcion.ConvertirToListFlujo(dataTabla);
            }
            else
            {
                return null;
            }

            return FlujoSolicitud;
        }

        public Aprobacion ObtenerSolicitudesPorAprobar(string Cedula)
        {
            Aprobacion Aprobacion = new Aprobacion();
            DataSet dataSet = new DataSet();
            
            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand("Sp_GetSolicitudesAprobacion", Conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Cedula", Cedula);
                SqlDataAdapter dataAdaptador = new SqlDataAdapter(command);
                dataAdaptador.Fill(dataSet);
                Conexion.Close();
            }
            EngineStf Funcion = new EngineStf();
            if (dataSet.Tables.Count > 0)
            {
                DataTable dtPasosAnticipo = dataSet.Tables[0];
                DataTable dtPasosLegalizacion = dataSet.Tables[1];
                DataTable dtAnticipos = dataSet.Tables[2];
                DataTable dtLegalizaciones = dataSet.Tables[3];

                if (dtPasosAnticipo.Rows.Count > 0)
                    Aprobacion.PasosAsignadosAnticipo = Funcion.ConvertirToListPasos(dtPasosAnticipo);

                if (dtPasosLegalizacion.Rows.Count > 0)
                    Aprobacion.PasosAsignadosLegalizacion = Funcion.ConvertirToListPasos(dtPasosLegalizacion);

                if (dtAnticipos.Rows.Count > 0)
                    Aprobacion.Anticipos = Funcion.ConvertirToListAnticipos(dtAnticipos);

                if (dtLegalizaciones.Rows.Count > 0)
                    Aprobacion.Legalizaciones = Funcion.ConvertirToListLegalizaciones(dtLegalizaciones);
            }
            else
            {
                return null;
            }

            return Aprobacion;
        }


        public bool GestionAnticipo(int Id, int TipoAccion, string Usuario, string Motivo = "")
        {
            bool result = false;
            DataTable dataTable = new DataTable();

            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand("Sp_GestionAnticipo", Conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Id", Id);
                command.Parameters.AddWithValue("@TipoAccion", TipoAccion);
                command.Parameters.AddWithValue("@Usuario", Usuario);
                command.Parameters.AddWithValue("@Motivo", Motivo);
                SqlDataAdapter dataAdaptador = new SqlDataAdapter(command);
                dataAdaptador.Fill(dataTable);
                Conexion.Close();
            }

            string res = dataTable.Rows[0]["Respuesta"].ToString();
            if (res.Contains("Success"))
                result = true;

            if (res.Contains("Error"))
                result = false;

            return result;
        }


        public bool GestionLegalizacion(int Id, int TipoAccion, string Usuario, string Motivo = "")
        {
            bool result = false;
            DataTable dataTable = new DataTable();

            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand("Sp_GestionLegalizacion", Conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Id", Id);
                command.Parameters.AddWithValue("@TipoAccion", TipoAccion);
                command.Parameters.AddWithValue("@Usuario", Usuario);
                command.Parameters.AddWithValue("@Motivo", Motivo);
                SqlDataAdapter dataAdaptador = new SqlDataAdapter(command);
                dataAdaptador.Fill(dataTable);
                Conexion.Close();
            }

            string res = dataTable.Rows[0]["Respuesta"].ToString();
            if (res.Contains("Success"))
                result = true;

            if (res.Contains("Error"))
                result = false;

            return result;
        }
    }
}
