USE [DB_A486E4_Legalizaciones]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetExistePasoFlujo]    Script Date: 27/05/2019 13:47:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_GetExistePasoFlujo] 
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

