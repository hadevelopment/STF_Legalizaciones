USE [DB_A486E4_Legalizaciones]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetMoneda]    Script Date: 30/05/2019 10:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_GetMoneda]
(
@id INT
) 
AS
BEGIN
SET NOCOUNT ON;
IF (@id = 1)
BEGIN
SELECT Id ,  Abreviatura FROM Moneda WHERE id = @id AND Abreviatura <> '' OR Abreviatura<> null AND Estatus = 1 ORDER BY ID ASC;
END
ELSE
BEGIN
SELECT Id ,  Abreviatura FROM Moneda WHERE id >= @id AND Abreviatura <> '' OR Abreviatura<> null AND Estatus = 1 ORDER BY ID ASC;
END
END ;

