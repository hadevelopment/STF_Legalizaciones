USE [DB_A486E4_Legalizaciones]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CreateFlujoAprobadoresSolicitud]    Script Date: 30/05/2019 11:07:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Sp_CreateFlujoAprobadoresSolicitud] 
(
            @Update INT ,
			@TipoSolicitud VARCHAR(50),
            @Estatus INT,
		    @FechaCreacion DATETIME,
			@Descripcion VARCHAR(MAX),
			@CedulaAprobador VARCHAR(50),
			@NombreAprobador  VARCHAR(50),
			@EmailAprobador VARCHAR(50),
			@Orden INT,
			@DestinoId INT,
            @MontoMaximo FLOAT,
            @MontoMinimo FLOAT
)
AS
BEGIN
SET NOCOUNT ON;

              SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
              BEGIN TRAN [PERMISOSYNC]

             DECLARE @FlujoSolicitudId  INT;
        IF(@Update = 1)
		BEGIN
		    INSERT INTO FlujoSolicitud (
			                  Estatus,	
                              FechaCreacion,	
							  TipoSolicitudId,
						      DestinoId,	
							  MontoMaximo,	
							  MontoMinimo) VALUES
							 (@Estatus,	
                              @FechaCreacion,	
							 (SELECT FlujoSolicitudId FROM TipoSolicitud WHERE Nombre = @TipoSolicitud),
						      @DestinoId,	
							  @MontoMaximo,	
							  @MontoMinimo) 

	       	SET @FlujoSolicitudId = (SELECT SCOPE_IDENTITY());

		   INSERT INTO PasoFlujoSolicitud 
		   (Estatus,
		    FechaCreacion,
			Descripcion,
			CedulaAprobador,
			NombreAprobador,
			EmailAprobador,
			FlujoSolicitudId,
			Orden) 
			VALUES 
			(@Estatus,
		    @FechaCreacion,
			@Descripcion,
			@CedulaAprobador,
			@NombreAprobador,
			@EmailAprobador,
			@FlujoSolicitudId,
			@Orden);
END;
 ELSE
 BEGIN
  INSERT INTO PasoFlujoSolicitud 
		   (Estatus,
		    FechaCreacion,
			Descripcion,
			CedulaAprobador,
			NombreAprobador,
			EmailAprobador,
			FlujoSolicitudId,
			Orden) 
			VALUES 
			(@Estatus,
		    @FechaCreacion,
			@Descripcion,
			@CedulaAprobador,
			@NombreAprobador,
			@EmailAprobador,
			(SELECT MAX(Id) FROM FlujoSolicitud ),
			@Orden);
 END;

          SELECT
          A.CedulaAprobador,
          A.NombreAprobador,
          A.EmailAprobador,
          A.Orden,
          A.Descripcion,
		  B.MontoMinimo,
		  B.MontoMaximo,
		  C.Nombre AS Destino,
		  C.Id As DestinoId
          FROM PasoFlujoSolicitud AS A
		  INNER JOIN FlujoSolicitud AS B ON  A.FlujoSolicitudId = B.Id
		  INNER JOIN Destino As C ON  B.DestinoId = C.Id
          WHERE B.MontoMinimo = @MontoMinimo AND B.MontoMaximo = @MontoMaximo
          AND A.Estatus = @Estatus
          ORDER BY A.Orden ASC;

          COMMIT TRAN [PERMISOSYNC];

END ;