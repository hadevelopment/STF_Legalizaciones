USE [DB_A486E4_Legalizaciones]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetFlujoSolicitud]    Script Date: 27/05/2019 13:49:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Author:		<Daniel Sanchez>
-- Create date: <21 Mayo 2019>
-- Description:	<Procedimiento almacenado Para obtener el flujo de una solicitud>
-- =============================================
ALTER PROCEDURE [dbo].[Sp_GetFlujoSolicitud] 
(
  @SolicitudId int
)
AS
BEGIN

DECLARE @PasoActual INT;
SET @PasoActual = (SELECT PasoFlujoSolicitudId FROM Solicitud WHERE Id = 31)


	--//PROCESADO
	--//1 = Listo, 2 = En Espera, 0 = No Iniciado

	SELECT
			A.Id,
			A.Descripcion,
			A.CedulaAprobador,
			A.NombreAprobador,
			A.EmailAprobador,
			A.Orden,
			D.Descripcion AS TipoSolicitud,
			B.Descripcion AS Motivo,
			CASE B.PasoFlujoSolicitudId WHEN @PasoActual THEN 2 ELSE 1 END AS Procesado
		FROM PasoFlujoSolicitud A 
		INNER JOIN HistoricoSolicitud B ON A.Id = B.PasoFlujoSolicitudId 
		INNER JOIN FlujoSolicitud C ON C.Id = A.FlujoSolicitudId
		INNER JOIN TipoSolicitud D ON D.Id = C.TipoSolicitudId
		WHERE B.SolicitudId = 31
	UNION
	SELECT
			A.Id,
			A.Descripcion,
			A.CedulaAprobador,
			A.NombreAprobador,
			A.EmailAprobador,
			A.Orden,
			D.Descripcion AS TipoSolicitud,
			'' AS Motivo,
			0 AS Procesado
		FROM PasoFlujoSolicitud A 
		INNER JOIN FlujoSolicitud C ON C.Id = A.FlujoSolicitudId
		INNER JOIN TipoSolicitud D ON D.Id = C.TipoSolicitudId
		WHERE Not A.Id In (Select B.PasoFlujoSolicitudId From HistoricoSolicitud B Where B.SolicitudId = 31)

END;
