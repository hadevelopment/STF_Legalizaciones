/*
   martes, 18 de junio de 201918:23:30
   Usuario: sa
   Servidor: EMCSERVERI7
   Base de datos: MvcPractice
   Aplicación: 
*/

/* Para evitar posibles problemas de pérdida de datos, debe revisar este script detalladamente antes de ejecutarlo fuera del contexto del diseñador de base de datos.*/
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
CREATE TABLE dbo.KactusEmpleado
	(
	Id int NOT NULL IDENTITY (1, 1),
	Cargo varchar(50) NULL,
	CargoEmpleado varchar(50) NULL,
	Celular varchar(50) NULL,
	CentroCosto varchar(50) NULL,
	CodCiudadExpedicion varchar(50) NULL,
	CodCiudadResidencia varchar(50) NULL,
	CodDeptoExpedicion varchar(50) NULL,
	CodDeptoResidencia varchar(50) NULL,
	CodTipoEspPersonaPreliq varchar(50) NULL,
	CodTipoPersona varchar(50) NULL,
	CodUbicacion varchar(50) NULL,
	CodigoArea varchar(50) NULL,
	CodigoEmpresa varchar(50) NULL,
	CodigoGrupo varchar(50) NULL,
	CodigoNivel varchar(50) NULL,
	CodigoNivel1 varchar(50) NULL,
	CodigoNivel2 varchar(50) NULL,
	CodigoNivel3 varchar(50) NULL,
	CodigoNivel4 varchar(50) NULL,
	CodigoNivel5 varchar(50) NULL,
	CodigoNivel6 varchar(50) NULL,
	CodigoNivel7 varchar(50) NULL,
	Direccion varchar(50) NULL,
	Email varchar(50) NULL,
	Eps varchar(50) NULL,
	EscalaSalarial varchar(50) NULL,
	EstadoEmpleado varchar(50) NULL,
	ExtensionCompania varchar(50) NULL,
	FechaActCargo varchar(50) NULL,
	FechaActContr varchar(50) NULL,
	FechaInicioContrato varchar(50) NULL,
	FechaNacimiento varchar(50) NULL,
	GanaExtras varchar(50) NULL,
	IdentificacionExterna varchar(50) NULL,
	NombreNivel varchar(50) NULL,
	NombreNivel1 varchar(50) NULL,
	NombreNivel2 varchar(50) NULL,
	NombreNivel3 varchar(50) NULL,
	NombreNivel4 varchar(50) NULL,
	NombreNivel5 varchar(50) NULL,
	NombreNivel6 varchar(50) NULL,
	NombreNivel7 varchar(50) NULL,
	NumeroContrato varchar(50) NULL,
	NumeroIdentificacion varchar(50) NULL,
	PrimerApellido varchar(50) NULL,
	PrimerNombre varchar(50) NULL,
	PuedeSerVisitado varchar(50) NULL,
	Rh varchar(50) NULL,
	SegundoApellido varchar(50) NULL,
	SegundoNombre varchar(50) NULL,
	Telefono varchar(50) NULL,
	TipoContratista varchar(50) NULL,
	TipoDeIdentificacion varchar(50) NULL,
	TipoDeSangre varchar(50) NULL,
	Titular varchar(50) NULL,
	ValidoParaLiqNomina varchar(50) NULL,
	VencimientoAccion varchar(50) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.KactusEmpleado ADD CONSTRAINT
	PK_KactusEmpleado PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.KactusEmpleado SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.KactusEmpleado', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.KactusEmpleado', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.KactusEmpleado', 'Object', 'CONTROL') as Contr_Per 