USE [DB_A486E4_Legalizaciones]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetTiposSolicitud]    Script Date: 27/05/2019 13:52:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_GetTiposSolicitud] 
AS
BEGIN
SET NOCOUNT ON;
SELECT Nombre FROM TipoSolicitud WHERE Nombre <> '' OR Nombre <> null  ;
END ;

