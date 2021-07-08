USE [DB_A486E4_Legalizaciones1]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetSolicitudesAnticiposPendientesLegalizacion]    Script Date: 5/24/2019 7:42:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_GetSolicitudesAnticiposPendientesLegalizacion] 
(
  @EmpleadoCedula VARCHAR(50) = '',
  @fechaDesde DATETIME = '',
  @fechaHasta DATETIME = ''
)
AS
BEGIN

SET NOCOUNT ON;

  SELECT
       Solicitud.Id, 
       Solicitud.FechaCreacion ,
	   Solicitud.NumeroSolicitud,
	   Solicitud.Concepto,
	   Solicitud.Monto,
	   Moneda.Abreviatura AS Moneda,
	   Tasa.Valor AS Tasa,
	   Solicitud.EmpleadoCedula,
	   Solicitud.FechaSolicitud,
	   Solicitud.FechaVencimiento,
	   DATEDIFF(DAY,Solicitud.FechaCreacion,GETDATE()) AS DiasTranscurridos ,
	   Solicitud.EstadoId,
	   EstadoSolicitud.Descripcion as Estado,
	   Legalizacion.Id legalizacionID
	FROM Solicitud 
    inner join Moneda ON Solicitud.MonedaId = Moneda.Id
	inner join Tasa ON Solicitud.MonedaId = Tasa.Id
	left join Legalizacion ON Solicitud.Id = Legalizacion.SolicitudID
	inner join EstadoSolicitud ON EstadoSolicitud.Id = Solicitud.EstadoId
	WHERE (EmpleadoCedula = @EmpleadoCedula OR @EmpleadoCedula = '') AND 
		  (Solicitud.FechaSolicitud>=@fechaDesde OR @fechaDesde = '')  
			AND (Solicitud.FechaSolicitud<=@fechaHasta  OR @fechaHasta = '')
	ORDER BY Solicitud.Id DESC;

END ;

