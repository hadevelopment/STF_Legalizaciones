USE [DB_A486E4_Legalizaciones]
GO
/****** Object:  StoredProcedure [dbo].[Sp_DeletePasoAprobacion]    Script Date: 27/05/2019 13:42:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_DeletePasoAprobacion] 
(
  @Id INT
)
AS
BEGIN
SET NOCOUNT ON;

DELETE PasoFlujoSolicitud WHERE ID = @Id;

END ;

