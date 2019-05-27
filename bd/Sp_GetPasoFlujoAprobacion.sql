USE [DB_A486E4_Legalizaciones]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetPasoFlujoAprobacion]    Script Date: 27/05/2019 13:50:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_GetPasoFlujoAprobacion] 
(
  @TipoDocumento VARCHAR(50)
)
AS
BEGIN
SET NOCOUNT ON;
       DECLARE @FlujoSolicitudId INT;
	   SET @FlujoSolicitudId  =(SELECT FlujoSolicitudId FROM TipoSolicitud WHERE Nombre = @TipoDocumento);

               SELECT Id,    
	                  Descripcion,
					  CedulaAprobador, 
					  NombreAprobador, 
					  EmailAprobador,
					  Orden FROM PasoFlujoSolicitud WHERE FlujoSolicitudId = @FlujoSolicitudID AND Estatus = 1 ORDER BY Orden ASC;
END ;
