USE [DB_A486E4_Legalizaciones]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetDestino]    Script Date: 26/06/2019 15:52:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_GetUnoeeProveedores] 
AS
BEGIN
SET NOCOUNT ON;
SELECT * FROM UnoeeProveedores ORDER BY Nombre ASC;
END ;

