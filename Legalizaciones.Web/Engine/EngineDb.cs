using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Legalizaciones.Web.Models;
using Legalizaciones.Model;
using Legalizaciones.Model.Workflow;
using Legalizaciones.Erp.Models;


namespace Legalizaciones.Web.Engine
{
    public class EngineDb
    {
        public static string DefaultConnection { get; set; }
        private SqlConnection Conexion = new SqlConnection(EngineDb.DefaultConnection);
        

        public List<InfoLegalizacion> SolicitudesAntPendientesLegalizacion (string empleadoCedula)
        {
            List<InfoLegalizacion> InfoLegalizacion = new List<InfoLegalizacion>();
            try
            {

                DataTable dataTabla = new DataTable();
                using (Conexion)
                {
                    Conexion.Open();
                    SqlCommand command = new SqlCommand("Sp_GetSolicitudesAnticiposPendientesLegalizacion", Conexion);
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

            catch(Exception ex)
            {
                throw;
            }
           
            return InfoLegalizacion;
        }

        public List<InfoLegalizacion> SolicitudesAntPendientesLegalizacionFiltrar(string cedula, DateTime fechaDesde, DateTime fechaHasta)
        {
            List<InfoLegalizacion> InfoLegalizacion = new List<InfoLegalizacion>();
            try
            {
                DataTable dataTabla = new DataTable();
                using (Conexion)
                {
                    Conexion.Open();
                    SqlCommand command = new SqlCommand("Sp_GetSolicitudesAnticiposPendientesLegalizacion", Conexion);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.Add(new SqlParameter("@EmpleadoCedula", SqlDbType.VarChar));
                    command.Parameters.Add(new SqlParameter("@fechaDesde", SqlDbType.VarChar));
                    command.Parameters.Add(new SqlParameter("@fechaHasta", SqlDbType.VarChar));
                    command.Parameters[0].Value = cedula;
                    command.Parameters[1].Value = fechaDesde;
                    command.Parameters[2].Value = fechaHasta;
                    

                    SqlDataAdapter dataAdaptador = new SqlDataAdapter(command);
                    dataAdaptador.Fill(dataTabla);
                    Conexion.Close();
                }

                EngineStf Funcion = new EngineStf();
                InfoLegalizacion = Funcion.ConvertirToListSolicitud(dataTabla);
            }
            catch (Exception ex)
            {
                throw;
            }

            return InfoLegalizacion;
        }

    
        public List<string> TiposDocumentos(string SpName)
        {
            List<string> documento = new List<string>();
            DataTable dataTabla = new DataTable();
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


        public List<DocumentoTipo> DocumentType(string SpName)
        {
            DataTable dataTabla = new DataTable();
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
            List<DocumentoTipo> tipo = Funcion.DocumentType(dataTabla);
            return tipo;
        }

        public List<FlujoDescripcion> DescripcionFlujo(string SpName, int id)
        {
            DataTable dataTabla = new DataTable();
            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand(SpName, Conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Id", id);
                SqlDataAdapter dataAdaptador = new SqlDataAdapter(command);
                dataAdaptador.Fill(dataTabla);
                Conexion.Close();
            }
            EngineStf Funcion = new EngineStf();
            List<FlujoDescripcion> tipo = Funcion.DescripcionFlujo(dataTabla);
            return tipo;
        }


        public List<Destinos> Destino (string SpName)
        {
            DataTable dataTabla = new DataTable();
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
            List<Destinos> destino = Funcion.Destino(dataTabla);
            return destino;
        }

        public Monedas [] Moneda (string SpName,int id)
        {
            DataTable dataTabla = new DataTable();
            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand(SpName, Conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", id);
                SqlDataAdapter dataAdaptador = new SqlDataAdapter(command);
                dataAdaptador.Fill(dataTabla);
                Conexion.Close();
            }
            EngineStf Funcion = new EngineStf();
            Monedas [] moneda = Funcion.Moneda(dataTabla);
            return moneda;
        }

        public List<DataAprobacion> AprobadoresTipoSolicitud(string SpName, string tipoSolicitud, int estatus = 1)
        {
            List<DataAprobacion> dataList = new List<DataAprobacion>();
            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand(SpName, Conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@tipoSolicitud", tipoSolicitud);
                command.Parameters.AddWithValue("@estatus", estatus);
                SqlDataReader lector = command.ExecuteReader();
                int n = 0;
                while (lector.Read())
                {
                    DataAprobacion data = new DataAprobacion();
                    data.NivelAprobador = lector.GetString(0);
                    data.DescripcionAprobador = lector.GetString(1);
                    data.Orden = lector.GetInt32(2);
                    data.Descripcion = lector.GetString(3);
                    data.Id = lector.GetInt32(4);
                    data.MontoMinimo = lector.GetFloat(5);
                    data.MontoMaximo = lector.GetFloat(6);
                    data.Destino = lector.GetString(7);
                    data.DestinoId = lector.GetInt32(8);
                    data.TipoSolicitud = lector.GetString(9);
                    dataList.Insert(n, data);
                    n++;
                }
                lector.Close();
                Conexion.Close();
            }
            return dataList;
        }


        public List<DataAprobacion> AprobadoresTipoSolicitud(string SpName, DataAprobacion model)
        {
            List<DataAprobacion> dataList = new List<DataAprobacion>();
            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand(SpName, Conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Update", model.Update);
                command.Parameters.AddWithValue("@TipoSolicitud", model.TipoSolicitud);
                command.Parameters.AddWithValue("@Estatus", model.Estatus);
                command.Parameters.AddWithValue("@FechaCreacion", model.FechaCreacion);
                command.Parameters.AddWithValue("@Descripcion", model.Descripcion);
                command.Parameters.AddWithValue("@NivelAprobador", model.NivelAprobador);
                command.Parameters.AddWithValue("@DescripcionAprobador", model.DescripcionAprobador);
                command.Parameters.AddWithValue("@Orden", model.Orden);
                command.Parameters.AddWithValue("@DestinoId", model.DestinoId);
                command.Parameters.AddWithValue("@MontoMaximo", model.MontoMaximo);
                command.Parameters.AddWithValue("@MontoMinimo", model.MontoMinimo);
                SqlDataReader lector = command.ExecuteReader();
                int n = 0;
                while (lector.Read())
                {
                    DataAprobacion data = new DataAprobacion();
                    data.NivelAprobador = lector.GetString(0);
                    data.DescripcionAprobador = lector.GetString(1);
                    data.Orden = lector.GetInt32(2);
                    data.Descripcion = lector.GetString(3);
                    data.Id = lector.GetInt32(4);
                    data.MontoMinimo = lector.GetFloat(5);
                    data.MontoMaximo = lector.GetFloat(6);
                    data.Destino = lector.GetString(7);
                    data.DestinoId = lector.GetInt32(8);
                    data.TipoSolicitud = lector.GetString(9);
                    dataList.Insert(n, data);
                    n++;
                }
                lector.Close();
                Conexion.Close();
            }
            return dataList;
        }


        public List<DataAprobacion> AprobadoresFlujoSolicitud(string SpName, int idDocumento, int idFlujo ,int destinoId, int estatus = 1)
        {
            List<DataAprobacion> dataList = new List<DataAprobacion>();
            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand(SpName, Conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@idDocumento", idDocumento);
                command.Parameters.AddWithValue("@idFlujo", idFlujo);
                command.Parameters.AddWithValue("@estatus", estatus);
                command.Parameters.AddWithValue("@destinoId", destinoId);
                SqlDataReader lector = command.ExecuteReader();
                int n = 0;
                while (lector.Read())
                {
                    DataAprobacion data = new DataAprobacion();
                    data.NivelAprobador = lector.GetString(0);
                    data.DescripcionAprobador = lector.GetString(1);
                    data.Orden = lector.GetInt32(2);
                    data.Descripcion = lector.GetString(3);
                    data.Id = lector.GetInt32(4);
                    data.MontoMinimo = lector.GetInt64(5);
                    data.MontoMaximo = lector.GetInt64(6);
                    data.Destino = lector.GetString(7);
                    data.DestinoId = lector.GetInt32(8);
                    data.TipoSolicitud = lector.GetString(9);
                    data.IdTipoSolicitud = lector.GetInt32(10);
                    data.NivelSuplenteUno= lector.GetString(11);
                    data.DescripcionSuplenteUno = lector.GetString(12);
                    data.NivelSuplenteDos = lector.GetString(13);
                    data.DescripcionSuplenteDos = lector.GetString(14);
                    dataList.Insert(n, data);
                    n++;
                }
                lector.Close();
                Conexion.Close();
            }
            return dataList;
        }

        public List<Legalizaciones.Web.Models.DataAprobacion> AprobadoresFlujoSolicitud(string SpName, Legalizaciones.Web.Models.DataAprobacion model)
        {
            List<DataAprobacion> dataList = new List<DataAprobacion>();
            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand(SpName, Conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Update", model.Update);
                command.Parameters.AddWithValue("@TipoSolicitud", model.TipoSolicitud);
                command.Parameters.AddWithValue("@Estatus", model.Estatus);
                command.Parameters.AddWithValue("@FechaCreacion", model.FechaCreacion);
                command.Parameters.AddWithValue("@Descripcion", model.Descripcion);
                command.Parameters.AddWithValue("@NivelAprobador", model.NivelAprobador);
                command.Parameters.AddWithValue("@DescripcionAprobador", model.DescripcionAprobador);
                command.Parameters.AddWithValue("@Orden", model.Orden);
                command.Parameters.AddWithValue("@DestinoId", model.DestinoId);
                command.Parameters.AddWithValue("@MontoMaximo", model.MontoMaximo);
                command.Parameters.AddWithValue("@MontoMinimo", model.MontoMinimo);
                string stringempty = " ";
                if (model.NivelSuplenteDos == null) model.NivelSuplenteDos = stringempty;
                command.Parameters.AddWithValue("@NivelSuplenteDos", model.NivelSuplenteDos);
                if (model.NivelSuplenteUno == null) model.NivelSuplenteUno = stringempty;
                command.Parameters.AddWithValue("@NivelSuplenteUno", model.NivelSuplenteUno);
                if (model.DescripcionSuplenteDos == null) model.DescripcionSuplenteDos = stringempty;
                command.Parameters.AddWithValue("@DescripcionSuplenteDos", model.DescripcionSuplenteDos);
                if (model.DescripcionSuplenteUno == null) model.DescripcionSuplenteUno = stringempty;
                command.Parameters.AddWithValue("@DescripcionSuplenteUno", model.DescripcionSuplenteUno);


                SqlDataReader lector = command.ExecuteReader();
                int n = 0;
                while (lector.Read())
                {
                    Legalizaciones.Web.Models.DataAprobacion data = new Legalizaciones.Web.Models.DataAprobacion();
                    data.NivelAprobador = lector.GetString(0);
                    data.DescripcionAprobador = lector.GetString(1);
                    data.Orden = lector.GetInt32(2);
                    data.Descripcion = lector.GetString(3);
                    data.Id = lector.GetInt32(4);
                    data.MontoMinimo = lector.GetInt64(5);
                    data.MontoMaximo = lector.GetInt64(6);
                    data.Destino = lector.GetString(7);
                    data.DestinoId = lector.GetInt32(8);
                    data.TipoSolicitud = lector.GetString(9);
                    data.IdTipoSolicitud = lector.GetInt32(10);
                    data.NivelSuplenteUno = lector.GetString(11);
                    data.DescripcionSuplenteUno = lector.GetString(12);
                    data.NivelSuplenteDos = lector.GetString(13);
                    data.DescripcionSuplenteDos = lector.GetString(14);
                    dataList.Insert(n, data);
                    n++;
                }
                lector.Close();
                Conexion.Close();
            }
            return dataList;
        }




        public bool UpdatePasoFlujoAprobacion(string SpName, int id, string descripcion, string nivelAprobador, string descripcionAprobador, int orden = 0,
            string nivelSuplente1 = "", string descripcionSuplente1 = "", string nivelSuplente2 = "", string descripcionSuplente2 = "")
        {
            bool resultado = false;
            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand(SpName, Conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Descripcion", descripcion);
                command.Parameters.AddWithValue("@NivelAprobador", nivelAprobador);
                command.Parameters.AddWithValue("@DescripcionAprobador", descripcionAprobador);
                command.Parameters.AddWithValue("@Orden", orden);
                string stringempty = " ";
                if (nivelSuplente2 == null) nivelSuplente2 = stringempty;
                command.Parameters.AddWithValue("@NivelSuplenteDos", nivelSuplente2);
                if (nivelSuplente1 == null) nivelSuplente1 = stringempty;
                command.Parameters.AddWithValue("@NivelSuplenteUno", nivelSuplente1);
                if (descripcionSuplente2 == null) descripcionSuplente2 = stringempty;
                command.Parameters.AddWithValue("@DescripcionSuplenteDos", descripcionSuplente2);
                if (descripcionSuplente1 == null) descripcionSuplente1 = stringempty;
                command.Parameters.AddWithValue("@DescripcionSuplenteUno", descripcionSuplente1);
                command.ExecuteNonQuery();
                resultado = true;
                Conexion.Close();
            }
            return resultado;
        }

        public bool UpdatePasoFlujoAprobacion(string SpName, DataTable dt)
        {
            SqlConnection cnx = new SqlConnection(EngineDb.DefaultConnection);
            SqlCommand command = new SqlCommand(SpName, cnx);
            bool resultado = false;
            using (cnx)
            {
                cnx.Open();
                foreach (DataRow R in dt.Rows)
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Id", R["Id"]);
                    command.Parameters.AddWithValue("@Descripcion", R["Descripcion"]);
                    command.Parameters.AddWithValue("@NivelAprobador", R["NivelAprobador"]);
                    command.Parameters.AddWithValue("@DescripcionAprobador", R["DescripcionAprobador"]);
                    command.Parameters.AddWithValue("@Orden", R["Orden"]);
                    command.Parameters.AddWithValue("@NivelSuplenteDos", R["NivelSuplenteDos"]);
                    command.Parameters.AddWithValue("@NivelSuplenteUno", R["NivelSuplenteUno"]);
                    command.Parameters.AddWithValue("@DescripcionSuplenteDos", R["DescripcionSuplenteDos"]);
                    command.Parameters.AddWithValue("@DescripcionSuplenteUno", R["DescripcionSuplenteUno"]);
                    command.ExecuteNonQuery();
                }
                cnx.Close();
                resultado = true;
            }
            return resultado;
        }

        public bool DeletePasoFlujoAprobacion(string SpName, int id)
        {
            bool resultado = false;
            SqlConnection cnx = new SqlConnection(EngineDb.DefaultConnection);
            using (cnx)
            {
                cnx.Open();
                SqlCommand command = new SqlCommand(SpName, cnx);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
                resultado = true;
                cnx.Close();
            }
            return resultado;
        }

        public int CountDocAsociado(string SpName, int id, string tipoDocumento)
        {
            int resultado = -1;
            object obj = new object();
            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand(SpName, Conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@tipoDoc", tipoDocumento);
                command.Parameters.AddWithValue("@Id", id);
                obj = command.ExecuteScalar();
                Conexion.Close();
                if (obj != null)
                    resultado = Convert.ToInt32(obj);
              
            }
            return resultado;
        }

        public DataTable GetPasoFlujoAprobacion(string SpName, string tipoDocumento)
        {
            DataTable dataTabla = new DataTable();
            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand(SpName, Conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@tipoDocumento", tipoDocumento);
                SqlDataAdapter dataAdaptador = new SqlDataAdapter(command);
                dataAdaptador.Fill(dataTabla);
                Conexion.Close();
            }
            return dataTabla;
        }

        public DataTable GetPasoFlujoAprobacion(string SpName, int flujoSolicitudId)
        {
            DataTable dataTabla = new DataTable();
            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand(SpName, Conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@flujoSolicitudId", flujoSolicitudId);
                SqlDataAdapter dataAdaptador = new SqlDataAdapter(command);
                dataAdaptador.Fill(dataTabla);
                Conexion.Close();
            }
            return dataTabla;
        }

        public int ExistePasoFlujoAprobacion(string SpName, int orden, string tipoDocumento,float montoMinimo,float montoMaximo)
        {
            int resultado = 0;
            object obj = new object();
            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand(SpName, Conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Orden", orden);
                command.Parameters.AddWithValue("@TipoDocumento", tipoDocumento);
                command.Parameters.AddWithValue("@MontoMinimo", montoMinimo);
                command.Parameters.AddWithValue("@MontoMaximo", montoMaximo);
                obj = command.ExecuteScalar();
                if (obj != null)
                    resultado = Convert.ToInt32(obj);
                Conexion.Close();
            }
            return resultado;

        }

        public int ExisteRangoAprobacion(string SpName, int destinoId ,float montoMinimo, float montoMaximo,int idDocumento)
        {
            int resultado = 0;
            object obj = new object();
            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand(SpName, Conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@IdDocumento", idDocumento);
                command.Parameters.AddWithValue("@DestinoId", destinoId);
                command.Parameters.AddWithValue("@MontoMaximo", montoMaximo);
                command.Parameters.AddWithValue("@MontoMinimo", montoMinimo);
                obj = command.ExecuteScalar();
                if (obj != null)
                    resultado = Convert.ToInt32(obj);
                Conexion.Close();
            }
            return resultado;

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

        public Aprobacion ObtenerSolicitudesPorAprobar(string Cargo)
        {
            Aprobacion Aprobacion = new Aprobacion();
            DataSet dataSet = new DataSet();
            
            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand("Sp_GetSolicitudesAprobacion", Conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Cargo", Cargo);
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

        public bool InsertKactusEmpleados(string SpName, List<KactusIntegration.Empleado> model)
        {
            bool resultado = false;
            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand(SpName, Conexion);
                command.CommandType = CommandType.StoredProcedure;
                int nueva = 0;
                foreach (KactusIntegration.Empleado m in model)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Nueva", nueva);
                    command.Parameters.AddWithValue("@Cargo", m.Cargo);
                    command.Parameters.AddWithValue("@CargoEmpleado", m.CargoEmpleado);
                    command.Parameters.AddWithValue("@Celular", m.Celular);
                    command.Parameters.AddWithValue("@CentroCosto", m.CentroCosto);
                    command.Parameters.AddWithValue("@CodCiudadExpedicion", m.CodCiudadExpedicion);
                    command.Parameters.AddWithValue("@CodCiudadResidencia", m.CodCiudadResidencia);
                    command.Parameters.AddWithValue("@CodDeptoExpedicion", m.CodDeptoExpedicion);
                    command.Parameters.AddWithValue("@CodDeptoResidencia", m.CodDeptoResidencia);
                    command.Parameters.AddWithValue("@CodTipoEspPersonaPreliq", m.CodTipoEspPersonaPreliq);
                    command.Parameters.AddWithValue("@CodTipoPersona", m.CodTipoPersona);
                    command.Parameters.AddWithValue("@CodUbicacion", m.CodUbicacion);
                    command.Parameters.AddWithValue("@CodigoArea", m.CodigoArea);
                    command.Parameters.AddWithValue("@CodigoEmpresa", m.CodigoEmpresa);
                    command.Parameters.AddWithValue("@CodigoGrupo", m.CodigoGrupo);
                    command.Parameters.AddWithValue("@CodigoNivel", m.CodigoNivel);
                    command.Parameters.AddWithValue("@CodigoNivel1", m.CodigoNivel1);
                    command.Parameters.AddWithValue("@CodigoNivel2", m.CodigoNivel2);
                    command.Parameters.AddWithValue("@CodigoNivel3", m.CodigoNivel3);
                    command.Parameters.AddWithValue("@CodigoNivel4", m.CodigoNivel4);
                    command.Parameters.AddWithValue("@CodigoNivel5", m.CodigoNivel5);
                    command.Parameters.AddWithValue("@CodigoNivel6", m.CodigoNivel6);
                    command.Parameters.AddWithValue("@CodigoNivel7", m.CodigoNivel);
                    command.Parameters.AddWithValue("@Direccion", m.Direccion);
                    command.Parameters.AddWithValue("@Email", m.Email);
                    command.Parameters.AddWithValue("@Eps", m.Eps);
                    command.Parameters.AddWithValue("@EscalaSalarial", m.EscalaSalarial);
                    command.Parameters.AddWithValue("@EstadoEmpleado", m.EstadoEmpleado);
                    command.Parameters.AddWithValue("@ExtensionCompania", m.ExtensionCompania);
                    command.Parameters.AddWithValue("@FecActCargo", m.FecActCargo);
                    command.Parameters.AddWithValue("@FecActContr", m.FecActContr);
                    command.Parameters.AddWithValue("@FechaInicioContrato", m.FechaInicioContrato);
                    command.Parameters.AddWithValue("@FechaNacimiento", m.FechaNacimiento);
                    command.Parameters.AddWithValue("@GanaExtras", m.GanaExtras);
                    command.Parameters.AddWithValue("@IdentificacionExterna", m.IdentificacionExterna);
                    command.Parameters.AddWithValue("@NombreNivel", m.NombreNivel);
                    command.Parameters.AddWithValue("@NombreNivel1", m.NombreNivel1);
                    command.Parameters.AddWithValue("@NombreNivel2", m.NombreNivel2);
                    command.Parameters.AddWithValue("@NombreNivel3", m.NombreNivel3);
                    command.Parameters.AddWithValue("@NombreNivel4", m.NombreNivel4);
                    command.Parameters.AddWithValue("@NombreNivel5", m.NombreNivel5);
                    command.Parameters.AddWithValue("@NombreNivel6", m.NombreNivel6);
                    command.Parameters.AddWithValue("@NombreNivel7", m.NombreNivel7);
                    command.Parameters.AddWithValue("@NumeroContrato", m.NumeroContrato);
                    command.Parameters.AddWithValue("@NumeroDeIdentificacion", m.NumeroDeIdentificacion);
                    command.Parameters.AddWithValue("@PrimerApellido", m.PrimerApellido);
                    command.Parameters.AddWithValue("@PrimerNombre", m.PrimerNombre);
                    command.Parameters.AddWithValue("@PuedeSerVisitado", m.PuedeSerVisitado);
                    command.Parameters.AddWithValue("@Rh", m.Rh);
                    command.Parameters.AddWithValue("@SegundoApellido", m.SegundoApellido);
                    command.Parameters.AddWithValue("@SegundoNombre", m.SegundoNombre);
                    command.Parameters.AddWithValue("@Telefono", m.Telefono);
                    command.Parameters.AddWithValue("@TipoContratista", m.TipoContratista);
                    command.Parameters.AddWithValue("@TipoDeIdentificacion", m.TipoDeIdentificacion);
                    command.Parameters.AddWithValue("@TipoDeSangre", m.TipoDeSangre);
                    command.Parameters.AddWithValue("@Titular", m.Titular);
                    command.Parameters.AddWithValue("@ValidoParaLiqNomina", m.ValidoParaLiqNomina);
                    command.Parameters.AddWithValue("@VencimientoAccion", m.VencimientoAccion);
                    command.ExecuteNonQuery();
                    nueva++;
                }
                Conexion.Close();
                resultado = true;
            }
            return resultado;
        }


        public bool TriggerActualizacionSolicitud(int SolicitudId)
        {
            bool result = false;
            DataTable dataTable = new DataTable();

            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand("Sp_UpdateTriggerSolicitud", Conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@SolicitudId", SolicitudId);
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


        public bool TriggerActualizacionLegalizacion(int LegalizacionId)
        {
            bool result = false;
            DataTable dataTable = new DataTable();

            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand("Sp_UpdateTriggerLegalizacion", Conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@LegalizacionId", LegalizacionId);
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

        public List<Flujo> ObtenerFlujoLegalizacion(int LegalizacionId)
        {
            List<Flujo> FlujoLegalizacion = new List<Flujo>();
            DataTable dataTabla = new DataTable();
            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand("Sp_GetFlujoLegalizacion", Conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@LegalizacionId", LegalizacionId);
                SqlDataAdapter dataAdaptador = new SqlDataAdapter(command);
                dataAdaptador.Fill(dataTabla);
                Conexion.Close();
            }
            EngineStf Funcion = new EngineStf();
            if (dataTabla.Rows.Count > 0)
            {
                FlujoLegalizacion = Funcion.ConvertirToListFlujo(dataTabla);
            }
            else
            {
                return null;
            }

            return FlujoLegalizacion;
        }

        public bool InsertUnoeeProveedores(string SpName, ListSuppliers model)
        {
            bool resultado = false;
            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand(SpName, Conexion);
                command.CommandType = CommandType.StoredProcedure;
                int nueva = 0;
                foreach (var m in model.Resultado)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Nueva", nueva);
                    if (m.IdProveedor == null) m.IdProveedor = string.Empty;
                    command.Parameters.AddWithValue("@IdProveedor", m.IdProveedor);
                    if (m.Nombre == null) m.Nombre = string.Empty;
                    command.Parameters.AddWithValue("@Nombre", m.Nombre);
                    if (m.Telefono == null) m.Telefono = string.Empty;
                    command.Parameters.AddWithValue("@Telefono", m.Telefono);
                    if (m.Celular == null) m.Celular = string.Empty;
                    command.Parameters.AddWithValue("@Celular", m.Celular);
                    if (m.FechaReg == null) m.FechaReg = string.Empty;
                    command.Parameters.AddWithValue("@FechaReg", m.FechaReg);
                    if (m.Sucursal == null) m.Sucursal = string.Empty;
                    command.Parameters.AddWithValue("@Sucursal", m.Sucursal);
                    if (m.Des_Sucursal == null) m.Des_Sucursal = string.Empty;
                    command.Parameters.AddWithValue("@Des_Sucursal", m.Des_Sucursal);
                    if (m.Pais == null) m.Pais = string.Empty;
                    command.Parameters.AddWithValue("@Pais", m.Pais);
                    if (m.Depto == null) m.Depto = string.Empty;
                    command.Parameters.AddWithValue("@Depto", m.Depto);
                    if (m.Ciudad == null) m.Ciudad = string.Empty;
                    command.Parameters.AddWithValue("@Ciudad", m.Ciudad);
                    if (m.Direccion == null) m.Direccion = string.Empty;
                    command.Parameters.AddWithValue("@Direccion", m.Direccion);
                    if (m.Email == null) m.Email = string.Empty;
                    command.Parameters.AddWithValue("@Email", m.Email);
                    command.ExecuteNonQuery();
                    nueva++;
                }
                Conexion.Close();
                resultado = true;
            }
            return resultado;
        }

        public List<Suppliers> GetUnoeeProveedores()
        {
            List<Suppliers> dataList = new List<Suppliers>();
            using (Conexion)
            {
                Conexion.Open();
                SqlCommand command = new SqlCommand("Sp_GetUnoeeProveedores", Conexion);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader lector = command.ExecuteReader();
                int n = 0;
                while (lector.Read())
                {
                    Suppliers data = new Suppliers();
                    data.IdTable = lector.GetInt32(0);
                    data.IdProveedor = lector.GetString(1);
                    data.Nombre = lector.GetString(2);
                    data.Telefono = lector.GetString(3);
                    data.Celular = lector.GetString(4);
                    data.FechaReg = lector.GetString(5);
                    data.Sucursal = lector.GetString(6);
                    data.Des_Sucursal = lector.GetString(7);
                    data.Pais = lector.GetString(8);
                    data.Depto = lector.GetString(9);
                    data.Ciudad = lector.GetString(10);
                    data.Direccion = lector.GetString(11);
                    data.Email = lector.GetString(12);
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