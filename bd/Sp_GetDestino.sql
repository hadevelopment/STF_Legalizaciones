USE [DB_A486E4_Legalizaciones]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetDestino]    Script Date: 30/05/2019 10:13:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_GetDestino] 
AS
BEGIN
SET NOCOUNT ON;
SELECT Id, Nombre FROM Destino WHERE Nombre <> '' OR Nombre <> null AND Estatus = 1 ORDER BY ID ASC;
END ;

