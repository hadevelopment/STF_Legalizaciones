USE [DB_A486E4_Legalizaciones]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetSolicitudesAprobacion]    Script Date: 27/05/2019 13:51:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Author:		<Daniel Sanchez>
-- Create date: <21 Mayo 2019>
-- Description:	<Procedimiento almacenado Para obtener los anticipos y legalizaciones asignadas al usuario>
-- =============================================
ALTER PROCEDURE [dbo].[Sp_GetSolicitudesAprobacion] 
(
  @Cedula VARCHAR(MAX)
)
AS
BEGIN

--PASOS ASIGNADOS PARA APROBAR ANTICIPOS--
	SELECT * FROM PasoFlujoSolicitud WHERE FlujoSolicitudId = 1 AND CedulaAprobador = @Cedula AND Estatus = 1

--PASOS ASIGNADOS PARA APROBAR ANTICIPOS--
	SELECT * FROM PasoFlujoSolicitud WHERE FlujoSolicitudId = 2 AND CedulaAprobador = @Cedula AND Estatus = 1

--ANTICIPOS--
	SELECT A.*, C.Descripcion AS EstadoDescripcion, B.Descripcion AS PasoDescripcion 
	FROM solicitud A  
	INNER JOIN PasoFlujoSolicitud B ON A.PasoFlujoSolicitudId = B.Id 
	INNER JOIN EstadoSolicitud C ON A.EstadoId= C.Id 
	WHERE A.PasoFlujoSolicitudId IN (SELECT id FROM PasoFlujoSolicitud WHERE CedulaAprobador = @cedula and FlujoSolicitudId = 1) 

--LEGALIZACIONES--
	SELECT A.*, C.Descripcion AS EstadoDescripcion, B.Descripcion AS PasoDescripcion 
	FROM Legalizacion A  
	INNER JOIN PasoFlujoSolicitud B ON A.PasoFlujoSolicitudId = B.Id 
	INNER JOIN EstadoLegalizacion C ON A.EstadoId= C.Id 
	WHERE PasoFlujoSolicitudId in (SELECT id FROM PasoFlujoSolicitud WHERE CedulaAprobador = @cedula and FlujoSolicitudId = 2) 

END;
