USE [DB_A486E4_Legalizaciones]
GO
/****** Object:  StoredProcedure [dbo].[Sp_CreateFlujoAprobadoresSolicitud]    Script Date: 22/05/2019 18:59:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_CreateFlujoAprobadoresSolicitud] 
(
            @Update INT ,
			@TipoSolicitud VARCHAR(50),
            @Estatus INT,
		    @FechaCreacion DATETIME,
			@Descripcion VARCHAR(MAX),
			@CedulaAprobador VARCHAR(50),
			@NombreAprobador  VARCHAR(50),
			@EmailAprobador VARCHAR(50),
			@Orden INT
)
AS
BEGIN
SET NOCOUNT ON;

              SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
              BEGIN TRAN [PERMISOSYNC]

              DECLARE @FlujoSolicitudId  INT;
		      SET @FlujoSolicitudId = (SELECT FlujoSolicitudId FROM TipoSolicitud WHERE Nombre = @TipoSolicitud);

		
                       
           IF (@Update = 1)
           Begin
		        Update PasoFlujoSolicitud SET Estatus = 0 WHERE FlujoSolicitudId = @FlujoSolicitudId;
		   End;

		
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


          SELECT
          CedulaAprobador,
          NombreAprobador,
          EmailAprobador,
          Orden,
          Descripcion   
          FROM PasoFlujoSolicitud 
          WHERE FlujoSolicitudId = @FlujoSolicitudId
          AND Estatus = @Estatus
          ORDER BY Orden ASC;

          COMMIT TRAN [PERMISOSYNC];

END ;