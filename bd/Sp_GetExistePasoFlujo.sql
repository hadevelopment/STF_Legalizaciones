USE [DB_A486E4_Legalizaciones]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_GetExistePasoFlujo] 
(
 @Orden INT,
 @TipoDocumento VARCHAR (50)
)
AS
BEGIN
SET NOCOUNT ON;
     SELECT ID FROM PasoFlujoSolicitud
	 WHERE FlujoSolicitudId = (SELECT FlujoSolicitudId FROM TipoSolicitud WHERE Nombre = @TipoDocumento) 
	 AND Orden = @Orden;
END ;

