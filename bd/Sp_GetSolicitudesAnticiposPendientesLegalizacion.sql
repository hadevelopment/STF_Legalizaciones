USE [DB_A486E4_Legalizaciones]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetSolicitudesAnticiposPendientesLegalizacion]    Script Date: 27/05/2019 13:51:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_GetSolicitudesAnticiposPendientesLegalizacion] 
(
  @EmpleadoCedula VARCHAR(50)
)
AS
BEGIN

SET NOCOUNT ON;

IF(@EmpleadoCedula = '')
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
	ORDER BY Solicitud.Id DESC;

ELSE

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
	WHERE Solicitud.EmpleadoCedula = @EmpleadoCedula 
	ORDER BY Solicitud.Id DESC;

END ;

