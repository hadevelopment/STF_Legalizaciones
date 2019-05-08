USE [DB_A486E4_Legalizaciones]
GO

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
	   Moneda.Nombre AS Moneda,
	   Tasa.Valor AS Tasa,
	   Solicitud.EmpleadoCedula,
	   (CASE 
	   WHEN (Solicitud.FechaVencimiento > GETDATE()) THEN 'Vencido'
	   WHEN (Solicitud.FechaVencimiento <= GETDATE()) THEN 'Vigente'
	   END) AS Estado,
	   '02589' AS IdDocErp,
	   Solicitud.FechaSolicitud,
	   Solicitud.FechaVencimiento,
	   DATEDIFF(DAY,Solicitud.FechaCreacion,GETDATE()) AS DiasTranscurridos 
	FROM Solicitud 
    inner join Moneda ON Solicitud.MonedaId = Moneda.Id
	inner join Tasa ON Solicitud.MonedaId = Tasa.Id
	WHERE Solicitud.EmpleadoCedula = @EmpleadoCedula;

END 
