USE [DB_A486E4_Legalizaciones]
GO
/****** Object:  StoredProcedure [dbo].[Sp_UpdatePasoAprobacion]    Script Date: 27/05/2019 14:14:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_UpdatePasoAprobacion] 
(
  @Id INT,
  @Descripcion VARCHAR(50),
  @CedulaAprobador VARCHAR(50),
  @NombreAprobador VARCHAR(50),
  @EmailAprobador VARCHAR(50),
  @Orden INT
)
AS
BEGIN
SET NOCOUNT ON;
      IF(@Orden > 0)
	       BEGIN
                UPDATE PasoFlujoSolicitud SET 
                      FechaCreacion = GetDate() , 
                      Descripcion = @Descripcion,
					  CedulaAprobador = @CedulaAprobador,
					  NombreAprobador = @NombreAprobador,
					  EmailAprobador = @EmailAprobador,
					  Orden = @Orden  
                 WHERE ID = @Id;
		    END ;
		ELSE
		  BEGIN
                UPDATE PasoFlujoSolicitud SET 
                      FechaCreacion = GetDate() , 
                      Descripcion = @Descripcion,
					  CedulaAprobador = @CedulaAprobador,
					  NombreAprobador = @NombreAprobador,
					  EmailAprobador = @EmailAprobador
                 WHERE ID = @Id;
		    END ;

END ;

