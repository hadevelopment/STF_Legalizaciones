using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Legalizaciones.Model;
using Legalizaciones.Model.ItemSolicitud;
using Legalizaciones.Model.Workflow;
using Legalizaciones.Web.Helpers;
using Legalizaciones.Web.Models;


namespace Legalizaciones.Web.Engine
{
    public class EngineStf
    {

        public List<InfoLegalizacion> ConvertirToListSolicitud(DataTable dt)
        {
            UNOEE erp = new UNOEE();
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
                    item.Monto = Convert.ToDouble(row[4]).ToString("N2");
                if (row[5] != DBNull.Value)
                    item.Moneda = row[5].ToString();
                if (row[6] != DBNull.Value)
                    item.Tasa = Convert.ToDouble(row[6]).ToString("N2"); ;
                if (row[7] != DBNull.Value)
                    item.EmpleadoCedula = row[7].ToString();
                if (row[8] != DBNull.Value)
                    item.FechaEntrega = row[8].ToString().Substring(0, 10);
                if (row[9] != DBNull.Value)
                    item.FechaVencimiento = row[9].ToString().Substring(0, 10);
                if (row[10] != DBNull.Value)
                    item.DiasTrascurridos = Convert.ToInt32(row[10]);
                if (row[11] != DBNull.Value)
                    item.EstadoId = Convert.ToInt32(row[11]);
                if (row[12] != DBNull.Value)
                    item.Estado = row[12].ToString();
                if (row[13] != DBNull.Value)
                    item.legalizacionID = Convert.ToInt32(row[13]);

                var empleado = erp.getEmpleadoCedula(item.EmpleadoCedula);

                item.IdDocErp = Aleatorio(n);
                item.ConsecutivoErp = Aleatorio(n + 2);
                item.Beneficiario = empleado.Nombre;
                list.Insert(n, item);
                n++;
            }
            return list;
        }


        public List<Flujo> ConvertirToListFlujo(DataTable dt)
        {
            UNOEE erp = new UNOEE();
            List<Flujo> list = new List<Flujo>();
            int n = 0;
            foreach (DataRow row in dt.Rows)
            {
                Flujo item = new Flujo();
                if (row["Id"] != DBNull.Value)
                    item.Id = Convert.ToInt32(row["Id"]);
                if (row["Descripcion"] != DBNull.Value)
                    item.Descripcion = row["Descripcion"].ToString();
                if (row["CedulaAprobador"] != DBNull.Value)
                    item.CedulaAprobador = row["CedulaAprobador"].ToString();
                if (row["NombreAprobador"] != DBNull.Value)
                    item.NombreAprobador = row["NombreAprobador"].ToString();
                if (row["EmailAprobador"] != DBNull.Value)
                    item.EmailAprobador = row["EmailAprobador"].ToString();
                if (row["Orden"] != DBNull.Value)
                    item.Orden = Convert.ToInt32(row["Orden"]);
                if (row["TipoSolicitud"] != DBNull.Value)
                    item.TipoSolicitud = row["TipoSolicitud"].ToString();
                if (row["Motivo"] != DBNull.Value)
                    item.Motivo = row["Motivo"].ToString();
                if (row["Procesado"] != DBNull.Value)
                    item.Procesado = Convert.ToInt32(row["Procesado"]);

                list.Insert(n, item);
                n++;
            }

            var rechazado = list.Where(m => m.Motivo.Contains("Rechazado")).FirstOrDefault();
            if (rechazado != null)
            {
                rechazado.Procesado = 3;
                list = list.Where(m => m.Orden != rechazado.Orden).ToList();
                list.Add(rechazado);
                list = list.OrderBy(m => m.Orden).ToList();
            }

            var aprobado = list.Where(m => m.Motivo.Contains("Aprobado")).FirstOrDefault();
            if(aprobado != null)
            {
                aprobado.Procesado = 1;
                list = list.Where(m => m.Orden != aprobado.Orden).ToList();
                list.Add(aprobado);
                list = list.OrderBy(m => m.Orden).ToList();
            }

            return list;
        }

        public List<PasoFlujoSolicitud> ConvertirToListPasos(DataTable dt)
        {
            UNOEE erp = new UNOEE();
            List<PasoFlujoSolicitud> list = new List<PasoFlujoSolicitud>();
            foreach (DataRow row in dt.Rows)
            {
                PasoFlujoSolicitud item = new PasoFlujoSolicitud();
                if (row["Id"] != DBNull.Value)
                    item.Id = Convert.ToInt32(row["Id"]);
                if (row["Estatus"] != DBNull.Value)
                    item.Estatus = Convert.ToInt32(row["Estatus"]);
                if (row["FechaCreacion"] != DBNull.Value)
                    item.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                if (row["Descripcion"] != DBNull.Value)
                    item.Descripcion = row["Descripcion"].ToString();
                if (row["Orden"] != DBNull.Value)
                    item.Orden = Convert.ToInt32(row["Orden"]);

                list.Add(item);
            }
            return list;
        }

        public List<Solicitud> ConvertirToListAnticipos(DataTable dt)
        {
            UNOEE erp = new UNOEE();
            List<Solicitud> list = new List<Solicitud>();
            foreach (DataRow row in dt.Rows)
            {
                Solicitud item = new Solicitud();
                if (row["Id"] != DBNull.Value)
                    item.Id = Convert.ToInt32(row["Id"]);
                if (row["Estatus"] != DBNull.Value)
                    item.Estatus = Convert.ToInt32(row["Estatus"]);
                if (row["FechaCreacion"] != DBNull.Value)
                    item.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                if (row["NumeroSolicitud"] != DBNull.Value)
                    item.NumeroSolicitud = row["NumeroSolicitud"].ToString();
                if (row["Concepto"] != DBNull.Value)
                    item.Concepto = row["Concepto"].ToString();
                if (row["FechaDesde"] != DBNull.Value)
                    item.FechaDesde = Convert.ToDateTime(row["FechaDesde"]);
                if (row["FechaHasta"] != DBNull.Value)
                    item.FechaHasta = Convert.ToDateTime(row["FechaHasta"]);

                if (row["EmpleadoCedula"] != DBNull.Value)
                    item.EmpleadoCedula = row["EmpleadoCedula"].ToString();
                if (row["Monto"] != DBNull.Value)
                    item.Monto = Convert.ToDecimal(row["Monto"]);
                if (row["FechaCreacion"] != DBNull.Value)
                    item.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                if (row["FechaVencimiento"] != DBNull.Value)
                    item.FechaVencimiento = Convert.ToDateTime(row["FechaVencimiento"]);

                if (row["EstadoId"] != DBNull.Value)
                    item.EstadoId = Convert.ToInt32(row["EstadoId"]);

                if (row["EstadoDescripcion"] != DBNull.Value)
                    item.EstadoSolicitud = new EstadoSolicitud { Descripcion = row["EstadoDescripcion"].ToString() };

                if (row["PasoDescripcion"] != DBNull.Value)
                    item.PasoFlujoSolicitud = new PasoFlujoSolicitud { Descripcion = row["PasoDescripcion"].ToString() };

                list.Add(item);
            }
            return list;
        }


        public List<Legalizacion> ConvertirToListLegalizaciones(DataTable dt)
        {
            UNOEE erp = new UNOEE();
            List<Legalizacion> list = new List<Legalizacion>();
            foreach (DataRow row in dt.Rows)
            {
                Legalizacion item = new Legalizacion();
                if (row["Id"] != DBNull.Value)
                    item.Id = Convert.ToInt32(row["Id"]);
                if (row["Estatus"] != DBNull.Value)
                    item.Estatus = Convert.ToInt32(row["Estatus"]);
                if (row["FechaCreacion"] != DBNull.Value)
                    item.FechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                if (row["SolicitudId"] != DBNull.Value)
                    item.SolicitudID = Convert.ToInt32(row["SolicitudId"]);
                if (row["ReciboCaja"] != DBNull.Value)
                    item.ReciboCaja = Convert.ToInt64(row["ReciboCaja"]);
                if (row["Consignacion"] != DBNull.Value)
                    item.Consignacion = Convert.ToInt64(row["Consignacion"]);
                if (row["Valor"] != DBNull.Value)
                    item.Valor = row["Valor"].ToString();
                if (row["EmpleadoCedula"] != DBNull.Value)
                    item.EmpleadoCedula = row["EmpleadoCedula"].ToString();
                if (row["PasoFlujoSolicitudId"] != DBNull.Value)
                    item.PasoFlujoSolicitudId = Convert.ToInt32(row["PasoFlujoSolicitudId"]);
                if (row["PasoDescripcion"] != DBNull.Value)
                    item.PasoFlujoSolicitud = new PasoFlujoSolicitud { Descripcion = row["PasoDescripcion"].ToString() };

                list.Add(item);
            }
            return list;
        }

        public string Aleatorio(int s)
        {
            Random rnd = new Random(s);
            int n = rnd.Next(1, 999);
            return "000" + n.ToString();
        }

        public List<string> TiposDocumentos(DataTable dt)
        {
            List<string> documento = new List<string>();
            int n = 0;
            documento.Insert(n, "Seleccione...");
            n++;
            foreach (DataRow row in dt.Rows)
            {
                string doc = string.Empty;
                if (row[0] != DBNull.Value)
                    doc = row[0].ToString();
                documento.Insert(n, doc);
                n++;
            }
            return documento;
        }


        public AprobacionDocumento SetCreateAprobador(AprobacionDocumento model, string tipoDocumento, string addAprobador, string empleado, string descripcion, string mail, int update, int estatus, int paso)
        {
            DataAprobacion Item = new DataAprobacion()
            {
                Update = update,
                Estatus = estatus,
                TipoSolicitud = tipoDocumento,
                NombreAprobador = addAprobador,
                CedulaAprobador = empleado,
                EmailAprobador = mail,
                Descripcion = descripcion,
                Orden = paso
            };

            EngineDb Metodo = new EngineDb();
            model.FlujoAprobacion = Metodo.AprobadoresTipoSolicitud("Sp_CreateFlujoAprobadoresSolicitud", Item);
            return model;
        }

        public bool ReordenarFlujoAprobacion(string tipoDocumento)
        {
            bool resultado = false;
            EngineDb Metodo = new EngineDb();
            DataTable dt = new DataTable();
            dt = Metodo.GetPasoFlujoAprobacion("Sp_GetPasoFlujoAprobacion", tipoDocumento);
            if (dt.Rows.Count > 0)
                dt = ReordenarPaso(dt);
            resultado = Metodo.UpdatePasoFlujoAprobacion("Sp_UpdatePasoAprobacion", dt);
            return resultado;
        }

        private DataTable ReordenarPaso(DataTable dt)
        {
            int n = 1;
            foreach (DataRow r in dt.Rows)
            {
                r["Orden"] = n;
                n++;
            }
            return dt;
        }

    }
}