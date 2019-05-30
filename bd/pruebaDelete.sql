SELECT * FROM PasoFlujoSolicitud 
SELECT * FROM FLUJOSOLICITUD


Delete PasoFlujoSolicitud Where Orden = 1 AND FlujoSolicitudId = (Select Id FROM Flujosolicitud WHERE TipoSolicitudId = 2 AND MontoMinimo = 4 AND MontoMaximo = 2) 
Delete FlujoSolicitud Where id = 7