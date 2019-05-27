USE [DB_A486E4_Legalizaciones]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetFlujoAprobadoresSolicitud]    Script Date: 27/05/2019 13:47:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_GetFlujoAprobadoresSolicitud] 
(
 @tipoSolicitud VARCHAR(50),
 @estatus INT
)
AS
BEGIN
SET NOCOUNT ON;

SELECT
CedulaAprobador,
NombreAprobador,
EmailAprobador,
Orden,
Descripcion,
Id,
(@tipoSolicitud) AS TipoSolicitud
FROM PasoFlujoSolicitud 
WHERE FlujoSolicitudId = (SELECT FlujoSolicitudId FROM TipoSolicitud WHERE Nombre = @tipoSolicitud) 
AND Estatus = @estatus
ORDER BY Orden ASC;

END ;

