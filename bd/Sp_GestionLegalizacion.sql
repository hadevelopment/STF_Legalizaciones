USE [DB_A486E4_Legalizaciones]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GestionLegalizacion]    Script Date: 27/05/2019 13:45:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Author:		<Daniel Sanchez>
-- Create date: <24 Mayo 2019>
-- Description:	<Procedimiento almacenado para controlar las actualizaciones de estados y flujo de Legalizacion>
-- =============================================
ALTER PROCEDURE [dbo].[Sp_GestionLegalizacion] 
(
  @Id INT,
  @TipoAccion INT,
  @Usuario VARCHAR(MAX),
  @Motivo VARCHAR(MAX)
)
AS
BEGIN

	--Se obtiene el flujo al que pertenece la Solicitud
	DECLARE @Flujo INT;
	SET @Flujo = (SELECT TOP 1 A.Id FROM FlujoSolicitud A WHERE A.TipoSolicitudId= 2)

	DECLARE @PasoActual INT;
	DECLARE @PasoSiguiente INT;
	DECLARE @PasoInicial INT;
	DECLARE @PasoFinal INT;
	DECLARE @MensajeAccion VARCHAR(MAX);
	DECLARE @Error INT;

	BEGIN TRAN

	--**INICIO**--

		--PASO ACTUAL
		SET @PasoActual = (SELECT A.PasoFlujoSolicitudId FROM Legalizacion A WHERE A.Id= @Id)

		--PASO SIGUIENTE
		SET @PasoSiguiente = (SELECT TOP 1 A.Id FROM PasoFlujoSolicitud A INNER JOIN FlujoSolicitud B ON A.FlujoSolicitudId = B.Id WHERE B.TipoSolicitudId = 2 AND A.Id > @PasoActual AND A.Estatus = 1)

		--PASO FINAL
		SET @PasoFinal = (SELECT TOP 1 A.Id FROM PasoFlujoSolicitud A INNER JOIN FlujoSolicitud B ON A.FlujoSolicitudId = B.Id WHERE B.TipoSolicitudId = 2 AND A.Estatus = 1 ORDER BY A.Orden DESC)

		--SI EXISTE UN PASO SIGUIENTE
		IF(@PasoSiguiente IS NOT NULL) BEGIN
			--DESCRIPCION DE LA ACCION
			SET @MensajeAccion = (SELECT CASE @TipoAccion WHEN 1 THEN 'Aprobado la legalización.' WHEN 2 THEN 'Rechazado la legalización con Motivo:' END)

			--INSERT DETALLE TRANSACCION
			INSERT INTO [dbo].[HistoricoLegalizacion]
			   ([Estatus]
			   ,[FechaCreacion]
			   ,[LegalizacionId]
			   ,[FlujoSolicitudId]
			   ,[PasoFlujoSolicitudId]
			   ,[Descripcion])
			 VALUES
				   (1
				   ,GETDATE()
				   ,@Id
				   ,@Flujo
				   ,@PasoSiguiente
				   ,'El usuario [' + @Usuario + '] ha ' + @MensajeAccion + ' ' + @Motivo)


			--SI ES APROBAR--
			IF(@TipoAccion = 1) 
			BEGIN
				--SE ACTUALIZA EL PASO AL SIGUIENTE
				UPDATE Legalizacion SET PasoFlujoSolicitudId = @PasoSiguiente WHERE Id = @Id;

				--ESTADO EN PROCESO
				UPDATE Legalizacion SET EstadoId = (SELECT Id FROM EstadoLegalizacion WHERE Descripcion like '%En Proc%') WHERE Id = @Id
			END
			--SI ES RECHAZAR
			ELSE IF(@TipoAccion = 2)
			BEGIN
				--SE TOMA EL PASO INICIAL
				SET @PasoInicial = (SELECT TOP 1 A.Id FROM PasoFlujoSolicitud A INNER JOIN FlujoSolicitud B ON A.FlujoSolicitudId = B.Id WHERE B.TipoSolicitudId = 2 AND A.Estatus = 1 ORDER BY A.Orden ASC)

				--SE ACTUALIZA EL PASO AL INICIAL (REESTABLECIENDO EL FLUJO)
				UPDATE Legalizacion SET PasoFlujoSolicitudId = @PasoInicial WHERE Id = @Id;

				--ESTADO RECHAZADO
				UPDATE Legalizacion SET EstadoId = (SELECT Id FROM EstadoLegalizacion WHERE Descripcion like '%Recha%') WHERE Id = @Id
			END;
			
			SET @Error = @@ERROR;
			IF (@Error<>0) GOTO TratarError
		END
		ELSE
			--SI ES EL ULTIMO PASO A APROBAR SE FINALIZA--
			IF(@PasoActual = @PasoFinal AND @TipoAccion = 1 ) BEGIN
				SET @PasoSiguiente = @PasoActual
				SET @Motivo = ''

				--Se obtiene la descripcion de la accion que se esta realizando
				SET @MensajeAccion = 'aprobado la legalización y la ha finalizado.'

				--Se inserta la Transaccion
				INSERT INTO [dbo].[HistoricoLegalizacion]
				   ([Estatus]
				   ,[FechaCreacion]
				   ,[LegalizacionId]
				   ,[FlujoSolicitudId]
				   ,[PasoFlujoSolicitudId]
				   ,[Descripcion])
				 VALUES
					   (1
					   ,GETDATE()
					   ,@Id
					   ,@Flujo
					   ,@PasoSiguiente
					   ,'El usuario [' + @Usuario + '] ha ' + @MensajeAccion + ' ' + @Motivo)


				--SE MANTIENE EL MISMO ESTADO
				UPDATE Legalizacion SET PasoFlujoSolicitudId = @PasoSiguiente WHERE Id = @Id;

				--ESTADO FINALIZADO
				UPDATE Legalizacion SET EstadoId = (SELECT Id FROM EstadoLegalizacion WHERE Descripcion like '%Finaliza%') WHERE Id = @Id

				SET @Error = @@ERROR;
				IF (@Error<>0) GOTO TratarError
			END
			--SI ES EL ULTIMO PASO A RECHAZAR SE REESTABLECE--
			ELSE IF (@PasoActual = @PasoFinal AND @TipoAccion = 2 ) BEGIN
				--SE TOMA EL PASO INICIAL
				SET @PasoInicial = (SELECT TOP 1 A.Id FROM PasoFlujoSolicitud A INNER JOIN FlujoSolicitud B ON A.FlujoSolicitudId = B.Id WHERE B.TipoSolicitudId = 2 AND A.Estatus = 1 ORDER BY A.Orden ASC)
				SET @PasoSiguiente = @PasoInicial

				--Se obtiene la descripcion de la accion que se esta realizando
				SET @MensajeAccion = 'rechazado la legalizacion con motivo:'

				--Se inserta la Transaccion
				INSERT INTO [dbo].[HistoricoLegalizacion]
				   ([Estatus]
				   ,[FechaCreacion]
				   ,[LegalizacionId]
				   ,[FlujoSolicitudId]
				   ,[PasoFlujoSolicitudId]
				   ,[Descripcion])
				 VALUES
					   (1
					   ,GETDATE()
					   ,@Id
					   ,@Flujo
					   ,@PasoSiguiente
					   ,'El usuario [' + @Usuario + '] ha ' + @MensajeAccion + ' ' + @Motivo)

				--SE REESTABLECE EL FLUJO
				UPDATE Legalizacion SET PasoFlujoSolicitudId = @PasoInicial WHERE Id = @Id;

				--ESTADO RECHAZADO
				UPDATE Legalizacion SET EstadoId = (SELECT Id FROM EstadoLegalizacion WHERE Descripcion like '%Rechaza%') WHERE Id = @Id

				SET @Error = @@ERROR;
				IF (@Error<>0) GOTO TratarError
			END

	--**FIN**--

	SELECT 'Success' AS Respuesta;
	COMMIT TRAN


	TratarError:
	--Si ha ocurrido algún error llegamos hasta aquí
	If @@Error<>0 BEGIN
		SELECT 'Error: Ha Ocurrido un error durante la transaccion' AS Respuesta;
		--Se lo comunicamos al usuario y deshacemos la transacción
		--todo volverá a estar como si nada hubiera ocurrido
		ROLLBACK TRAN
	END

END;
