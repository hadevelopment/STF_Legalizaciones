USE [DB_A486E4_Legalizaciones]
GO
/****** Object:  StoredProcedure [dbo].[Sp_InsertKactusEmpleado]    Script Date: 26/06/2019 14:30:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sp_InsertUnoeeProveedores] 
(                   @Nueva INT,
                  	@IdProveedor varchar(50) NULL,
	                @Nombre varchar(50) NULL,
	                @Telefono varchar(50) NULL,
	                @Celular varchar(50) NULL,
	                @FechaReg varchar(50) NULL,
	                @Sucursal varchar(50) NULL,
	                @Des_Sucursal varchar(50) NULL,
	                @Pais varchar(50) NULL,
	                @Depto varchar(50) NULL,
	                @Ciudad varchar(50) NULL,
	                @Direccion varchar(50) NULL,
	                @Email varchar(50) NULL
)
AS
BEGIN
SET NOCOUNT ON;

     If (@Nueva = 0)
	  Begin
       TRUNCATE TABLE UnoeeProveedores;
	   End ;

        INSERT INTO UnoeeProveedores (
		        	IdProveedor,
	                Nombre,
	                Telefono,
	                Celular,
	                FechaReg,
	                Sucursal,
	                Des_Sucursal,
	                Pais,
	                Depto,
	                Ciudad,
	                Direccion,
	                Email 
		         ) 
					VALUES 
					(
					@IdProveedor,
	                @Nombre,
	                @Telefono,
	                @Celular,
	                @FechaReg,
	                @Sucursal,
	                @Des_Sucursal,
	                @Pais,
	                @Depto,
	                @Ciudad,
	                @Direccion,
	                @Email 
					);


END ;