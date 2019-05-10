USE [DB_A486E4_Legalizaciones]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetSolicitudesAnticiposPendientesLegalizacion]    Script Date: 10/05/2019 11:48:56 ******/
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
	   PasoFlujoSolicitud.Descripcion as Estado,
	   CASE 
		(SELECT COUNT(*) FROM Solicitud INNER JOIN Legalizacion ON Legalizacion.SolicitudId = Solicitud.Id)
	   WHEN 0 THEN 'Legalizar'
	   ELSE ''
	   END AS Accion
	FROM Solicitud 
    inner join Moneda ON Solicitud.MonedaId = Moneda.Id
	inner join Tasa ON Solicitud.MonedaId = Tasa.Id
	left join Legalizacion ON Solicitud.Id = Legalizacion.SolicitudID
	inner join TipoSolicitud on TipoSolicitud.Id = Solicitud.TipoSolicitudId
	inner join FlujoSolicitud ON FlujoSolicitud.Id = TipoSolicitud.FlujoSolicitudId
	inner join PasoFlujoSolicitud ON PasoFlujoSolicitud.Id = FlujoSolicitud.PasoFlujoSolicitudId  
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
	   PasoFlujoSolicitud.Descripcion as Estado,
	   CASE 
		(SELECT COUNT(*) FROM Solicitud INNER JOIN Legalizacion ON Legalizacion.SolicitudId = Solicitud.Id)
	   WHEN 0 THEN 'Legalizar'
	   ELSE ''
	   END AS Accion
	FROM Solicitud 
    inner join Moneda ON Solicitud.MonedaId = Moneda.Id
	inner join Tasa ON Solicitud.MonedaId = Tasa.Id
	left join Legalizacion ON Solicitud.Id = Legalizacion.SolicitudID
	inner join TipoSolicitud on TipoSolicitud.Id = Solicitud.TipoSolicitudId
	inner join FlujoSolicitud ON FlujoSolicitud.Id = TipoSolicitud.FlujoSolicitudId
	inner join PasoFlujoSolicitud ON PasoFlujoSolicitud.Id = FlujoSolicitud.PasoFlujoSolicitudId 
	WHERE Solicitud.EmpleadoCedula = @EmpleadoCedula 
	ORDER BY Solicitud.Id DESC;

END ;
