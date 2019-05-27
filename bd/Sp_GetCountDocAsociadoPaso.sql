USE [DB_A486E4_Legalizaciones]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetCountDocAsociadoPaso]    Script Date: 27/05/2019 13:46:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_GetCountDocAsociadoPaso] 
(
  @TipoDoc VARCHAR(50),
  @Id INT
)
AS
BEGIN
SET NOCOUNT ON;
       DECLARE @FlujoSolicitudId INT;
	   SET @FlujoSolicitudId  =(SELECT FlujoSolicitudId FROM TipoSolicitud WHERE Nombre = @TipoDoc);

      IF(@FlujoSolicitudId = 1)
	       BEGIN
                SELECT COUNT(*) As Numero FROM Solicitud WHERE PasoFlujoSolicitudId = @Id;
		    END ;
		ELSE IF (@FlujoSolicitudId = 2)
		  BEGIN
               SELECT COUNT(*) As Numero FROM Legalizacion WHERE PasoFlujoSolicitudId = @Id;
		   END ;
END ;

