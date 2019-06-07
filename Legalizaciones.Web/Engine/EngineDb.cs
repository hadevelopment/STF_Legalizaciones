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
            try
            {

                DataTable dataTabla = new DataTable();
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

            catch(Exception ex)
            {
                throw;
            }
           
            return InfoLegalizacion;
        }

        public List<InfoLegalizacion> SolicitudesAntPendientesLegalizacionFiltrar(string SpName, 
                                        DateTime fechaDesde, DateTime fechaHasta)
        {
            List<InfoLegalizacion> InfoLegalizacion = new List<InfoLegalizacion>();
            try
            {

                DataTable dataTabla = new DataTable();
                using (Conexion)
                {
                    Conexion.Open();
                    SqlCommand command = new SqlCommand(SpName, Conexion);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.Add(new SqlParameter("@fechaDesde", SqlDbType.DateTime));
                    command.Parameters.Add(new SqlParameter("@fechaHasta", SqlDbType.DateTime));
                    command.Parameters[0].Value = fechaDesde;
                    command.Parameters[1].Value = fechaHasta;

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
                    data.CedulaAprobador = lector.GetString(0);
                    data.NombreAprobador = lector.GetString(1);
                    data.EmailAprobador = lector.GetString(2);
                    data.Orden = lector.GetInt32(3);
                    data.Descripcion = lector.GetString(4);
                    data.Id = lector.GetInt32(5);
                    data.MontoMinimo = lector.GetFloat(6);
                    data.MontoMaximo = lector.GetFloat(7);
                    data.Destino = lector.GetString(8);
                    data.DestinoId = lector.GetInt32(9);
                    data.TipoSolicitud = lector.GetString(10);
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
                command.Parameters.AddWithValue("@CedulaAprobador", model.CedulaAprobador);
                command.Parameters.AddWithValue("@NombreAprobador", model.NombreAprobador);
                command.Parameters.AddWithValue("@EmailAprobador", model.EmailAprobador);
                command.Parameters.AddWithValue("@Orden", model.Orden);
                command.Parameters.AddWithValue("@DestinoId", model.DestinoId);
                command.Parameters.AddWithValue("@MontoMaximo", model.MontoMaximo);
                command.Parameters.AddWithValue("@MontoMinimo", model.MontoMinimo);
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
                    data.Id = lector.GetInt32(5);
                    data.MontoMinimo = lector.GetFloat(6);
                    data.MontoMaximo = lector.GetFloat(7);
                    data.Destino = lector.GetString(8);
                    data.DestinoId = lector.GetInt32(9);
                    data.TipoSolicitud = lector.GetString(10);
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
                    data.CedulaAprobador = lector.GetString(0);
                    data.NombreAprobador = lector.GetString(1);
                    data.EmailAprobador = lector.GetString(2);
                    data.Orden = lector.GetInt32(3);
                    data.Descripcion = lector.GetString(4);
                    data.Id = lector.GetInt32(5);
                    data.MontoMinimo = lector.GetFloat(6);
                    data.MontoMaximo = lector.GetFloat(7);
                    data.Destino = lector.GetString(8);
                    data.DestinoId = lector.GetInt32(9);
                    data.TipoSolicitud = lector.GetString(10);
                    data.IdTipoSolicitud = lector.GetInt32(11);
                    data.CedulaSuplenteUno = lector.GetString(12);
                    data.NombreSuplenteUno = lector.GetString(13);
                    data.EmailSuplenteUno = lector.GetString(14);
                    data.CedulaSuplenteDos = lector.GetString(15);
                    data.NombreSuplenteDos = lector.GetString(16);
                    data.EmailSuplenteDos = lector.GetString(17);
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
                command.Parameters.AddWithValue("@CedulaAprobador", model.CedulaAprobador);
                command.Parameters.AddWithValue("@NombreAprobador", model.NombreAprobador);
                command.Parameters.AddWithValue("@EmailAprobador", model.EmailAprobador);
                command.Parameters.AddWithValue("@Orden", model.Orden);
                command.Parameters.AddWithValue("@DestinoId", model.DestinoId);
                command.Parameters.AddWithValue("@MontoMaximo", model.MontoMaximo);
                command.Parameters.AddWithValue("@MontoMinimo", model.MontoMinimo);
                if (model.CedulaSuplenteDos == null) model.CedulaSuplenteDos = " ";
                command.Parameters.AddWithValue("@CedulaSuplenteDos", model.CedulaSuplenteDos);
                if (model.CedulaSuplenteUno == null) model.CedulaSuplenteUno = " ";
                command.Parameters.AddWithValue("@CedulaSuplenteUno", model.CedulaSuplenteUno);
                if (model.EmailSuplenteDos == null) model.EmailSuplenteDos = " ";
                command.Parameters.AddWithValue("@EmailSuplenteDos", model.EmailSuplenteDos);
                if (model.EmailSuplenteUno == null) model.EmailSuplenteUno = " ";
                command.Parameters.AddWithValue("@EmailSuplenteUno", model.EmailSuplenteUno);
                if (model.NombreSuplenteDos == null) model.NombreSuplenteDos = " ";
                command.Parameters.AddWithValue("@NombreSuplenteDos", model.NombreSuplenteDos);
                if (model.NombreSuplenteUno == null) model.NombreSuplenteUno = " ";
                command.Parameters.AddWithValue("@NombreSuplenteUno", model.NombreSuplenteUno);


                SqlDataReader lector = command.ExecuteReader();
                int n = 0;
                while (lector.Read())
                {
                    Legalizaciones.Web.Models.DataAprobacion data = new Legalizaciones.Web.Models.DataAprobacion();
                    data.CedulaAprobador = lector.GetString(0);
                    data.NombreAprobador = lector.GetString(1);
                    data.EmailAprobador = lector.GetString(2);
                    data.Orden = lector.GetInt32(3);
                    data.Descripcion = lector.GetString(4);
                    data.Id = lector.GetInt32(5);
                    data.MontoMinimo = lector.GetFloat(6);
                    data.MontoMaximo = lector.GetFloat(7);
                    data.Destino = lector.GetString(8);
                    data.DestinoId = lector.GetInt32(9);
                    data.TipoSolicitud = lector.GetString(10);
                    data.IdTipoSolicitud = lector.GetInt32(11);
                    data.CedulaSuplenteUno = lector.GetString(12);
                    data.NombreSuplenteUno = lector.GetString(13);
                    data.EmailSuplenteUno = lector.GetString(14);
                    data.CedulaSuplenteDos = lector.GetString(15);
                    data.NombreSuplenteDos = lector.GetString(16);
                    data.EmailSuplenteDos = lector.GetString(17);
                    dataList.Insert(n, data);
                    n++;
                }
                lector.Close();
                Conexion.Close();
            }
            return dataList;
        }




        public bool UpdatePasoFlujoAprobacion(string SpName, int id, string descripcion, string cedulaAprobador, string nombreAprobador, string emailAprobador, int orden = 0, string aprobadorSuplente1 = "", string cedulaSuplente1 = "",
                                               string emailSuplente1 = "", string aprobadorSuplente2 = "", string cedulaSuplente2 = "", string emailSuplente2 = "")
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
                command.Parameters.AddWithValue("@CedulaAprobador", cedulaAprobador);
                command.Parameters.AddWithValue("@NombreAprobador", nombreAprobador);
                command.Parameters.AddWithValue("@EmailAprobador", emailAprobador);
                command.Parameters.AddWithValue("@Orden", orden);
                if (cedulaSuplente2 == null) cedulaSuplente2 = " ";
                command.Parameters.AddWithValue("@CedulaSuplenteDos",cedulaSuplente2);
                if (cedulaSuplente1 == null) cedulaSuplente1 = " ";
                command.Parameters.AddWithValue("@CedulaSuplenteUno",cedulaSuplente1);
                if (emailSuplente2 == null) emailSuplente2 = " ";
                command.Parameters.AddWithValue("@EmailSuplenteDos",emailSuplente2);
                if (emailSuplente1 == null) emailSuplente1 = " ";
                command.Parameters.AddWithValue("@EmailSuplenteUno",emailSuplente1);
                if (aprobadorSuplente2 == null) aprobadorSuplente2 = " ";
                command.Parameters.AddWithValue("@NombreSuplenteDos",aprobadorSuplente2);
                if (aprobadorSuplente1 == null) aprobadorSuplente1 = " ";
                command.Parameters.AddWithValue("@NombreSuplenteUno",aprobadorSuplente1);
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
                    command.Parameters.AddWithValue("@CedulaAprobador", R["CedulaAprobador"]);
                    command.Parameters.AddWithValue("@NombreAprobador", R["NombreAprobador"]);
                    command.Parameters.AddWithValue("@EmailAprobador", R["EmailAprobador"]);
                    command.Parameters.AddWithValue("@Orden", R["Orden"]);
                    command.Parameters.AddWithValue("@CedulaSuplenteDos", R["CedulaSuplenteDos"]);
                    command.Parameters.AddWithValue("@CedulaSuplenteUno", R["CedulaSuplenteUno"]);
                    command.Parameters.AddWithValue("@EmailSuplenteDos", R["EmailSuplenteDos"]);
                    command.Parameters.AddWithValue("@EmailSuplenteUno", R["EmailSuplenteUno"]);
                    command.Parameters.AddWithValue("@NombreSuplenteDos", R["NombreSuplenteDos"]);
                    command.Parameters.AddWithValue("@NombreSuplenteUno", R["NombreSuplenteUno"]);
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