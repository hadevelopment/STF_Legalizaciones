USE [DB_A486E4_Legalizaciones]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetDestino]    Script Date: 30/05/2019 9:54:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_CreateRangoMontoAprobacion] 
(
@Estatus INT,
@FechaCreacion DATETIME,
@TipoDocumento VARCHAR (50),
@DestinoId INT,
@MontoMaximo FLOAT,
@MontoMinimo FLOAT
)
AS
BEGIN
SET NOCOUNT ON;
 INSERT INTO FlujoSolicitud (Estatus,	
                             FechaCreacion,	
							 TipoSolicitudId,
						     DestinoId,	
							 MontoMaximo,	
							 MontoMinimo) VALUES
							 (@Estatus,	
                             @FechaCreacion,	
							 (SELECT FlujoSolicitudId FROM TipoSolicitud WHERE Nombre = @TipoDocumento),
						     @DestinoId,	
							 @MontoMaximo,	
							 @MontoMinimo) 

END ;

