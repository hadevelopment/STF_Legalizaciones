USE [DB_A486E4_Legalizaciones]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetSolicitudesAnticiposPendientesLegalizacion]    Script Date: 09/05/2019 11:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_GetSolicitudesAnticiposPendientesLegalizacion] 
(
  @EmpleadoCedula VARCHAR(50)
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
	   (EstadoSolicitud.Descripcion) AS Estado,
	   Solicitud.FechaSolicitud,
	   Solicitud.FechaVencimiento,
	   DATEDIFF(DAY,Solicitud.FechaCreacion,GETDATE()) AS DiasTranscurridos 
	FROM Solicitud 
    inner join Moneda ON Solicitud.MonedaId = Moneda.Id
	inner join Tasa ON Solicitud.MonedaId = Tasa.Id
	inner join EstadoSolicitud ON Solicitud.Estatus = EstadoSolicitud.Estatus
	WHERE Solicitud.EmpleadoCedula = @EmpleadoCedula
	ORDER BY Solicitud.Id DESC;

END 
