BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.UnoeeProveedores
	(
	Id int NOT NULL IDENTITY (1, 1),
	IdProveedor varchar(50) NULL,
	Nombre varchar(50) NULL,
	Telefono varchar(50) NULL,
	Celular varchar(50) NULL,
	FechaReg varchar(50) NULL,
	Sucursal varchar(50) NULL,
	Des_Sucursal varchar(50) NULL,
	Pais varchar(50) NULL,
	Depto varchar(50) NULL,
	Ciudad varchar(50) NULL,
	Direccion varchar(50) NULL,
	Email varchar(50) NULL
	
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.UnoeeProvedores ADD CONSTRAINT
	PK_UnoeeProvedores PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.KactusEmpleado SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.UnoeeProvedores', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.UnoeeProvedores', 'Object', 'CONTROL') as Contr_Per 