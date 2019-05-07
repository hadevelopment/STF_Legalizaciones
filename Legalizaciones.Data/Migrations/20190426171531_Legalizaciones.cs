using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Legalizaciones.Data.Migrations
{
    public partial class Legalizaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cargo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    Descripcion = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    Codigo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    Telefono = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Concepto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    Codigo = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    Activo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concepto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Destino",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destino", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Moneda",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    Simbolo = table.Column<byte>(nullable: false),
                    Abreviatura = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moneda", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pais",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    Abreviatura = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proveedor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Solicitud",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    NumeroSolicitud = table.Column<string>(nullable: true),
                    TipoSolicitudID = table.Column<int>(nullable: false),
                    Concepto = table.Column<string>(nullable: false),
                    DestinoID = table.Column<int>(nullable: false),
                    ZonaID = table.Column<int>(nullable: false),
                    CentroOperacionId = table.Column<int>(nullable: false),
                    UnidadNegocioId = table.Column<int>(nullable: false),
                    CentroCostoId = table.Column<int>(nullable: false),
                    FechaDesde = table.Column<DateTime>(nullable: false),
                    FechaHasta = table.Column<DateTime>(nullable: false),
                    MonedaId = table.Column<int>(nullable: false),
                    EmpleadoCedula = table.Column<string>(nullable: true),
                    Monto = table.Column<float>(nullable: false),
                    FechaSolicitud = table.Column<DateTime>(nullable: false),
                    FechaVencimiento = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitud", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoDocumento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDocumento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoServicio",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoServicio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoSolicitud",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    DiasHabiles = table.Column<int>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoSolicitud", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    Precio = table.Column<float>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    CategoriaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Producto_Categoria_CategoriaID",
                        column: x => x.CategoriaID,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inventario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    Total = table.Column<float>(nullable: false),
                    Activo = table.Column<string>(nullable: true),
                    Anticipo = table.Column<float>(nullable: false),
                    ChangeAmount = table.Column<float>(nullable: false),
                    ClienteID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventario_Cliente_ClienteID",
                        column: x => x.ClienteID,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zona",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    DestinoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zona", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zona_Destino_DestinoID",
                        column: x => x.DestinoID,
                        principalTable: "Destino",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ciudad",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    PaisID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ciudad_Pais_PaisID",
                        column: x => x.PaisID,
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Compania",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    RazonSocial = table.Column<string>(nullable: false),
                    PaisId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compania", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Compania_Pais_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudActiva",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    SolicitudID = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    IDEmpleado = table.Column<int>(nullable: false),
                    IDFlujo = table.Column<int>(nullable: false),
                    CantidadPasos = table.Column<int>(nullable: false),
                    PosicionActual = table.Column<int>(nullable: false),
                    Estatus = table.Column<int>(nullable: false),
                    IDSolicitud = table.Column<int>(nullable: false),
                    UltimoAprobador = table.Column<string>(nullable: true),
                    AprobadorSiguiente = table.Column<string>(nullable: true),
                    MotivoRechazo = table.Column<string>(nullable: true),
                    Observacion = table.Column<string>(nullable: true),
                    DocERP = table.Column<string>(nullable: true),
                    ColorEstatus = table.Column<string>(nullable: true),
                    FechaERP = table.Column<DateTime>(nullable: false),
                    Estado = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudActiva", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitudActiva_Solicitud_SolicitudID",
                        column: x => x.SolicitudID,
                        principalTable: "Solicitud",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudGastos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaGasto = table.Column<string>(nullable: true),
                    PaisId = table.Column<int>(nullable: false),
                    Pais = table.Column<string>(nullable: true),
                    ServicioId = table.Column<int>(nullable: true),
                    Servicio = table.Column<string>(nullable: true),
                    CiudadId = table.Column<int>(nullable: false),
                    Ciudad = table.Column<string>(nullable: true),
                    Origen = table.Column<string>(nullable: true),
                    Destino = table.Column<string>(nullable: true),
                    Monto = table.Column<float>(nullable: false),
                    IVA = table.Column<string>(nullable: true),
                    ReteIVA = table.Column<string>(nullable: true),
                    ReteServicio = table.Column<string>(nullable: true),
                    ICA = table.Column<string>(nullable: true),
                    Neto = table.Column<string>(nullable: true),
                    IVATeorico = table.Column<string>(nullable: true),
                    SolicitudId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudGastos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitudGastos_Solicitud_SolicitudId",
                        column: x => x.SolicitudId,
                        principalTable: "Solicitud",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlujoAprobacion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    IDDocumento = table.Column<string>(nullable: true),
                    IDDestino = table.Column<string>(nullable: true),
                    MontoDesde = table.Column<float>(nullable: false),
                    MontoHasta = table.Column<float>(nullable: false),
                    IDMoneda = table.Column<string>(nullable: true),
                    IDMAprobacion = table.Column<string>(nullable: true),
                    TipoDocumentoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlujoAprobacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlujoAprobacion_TipoDocumento_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalTable: "TipoDocumento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventarioGastos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    Cantidad = table.Column<int>(nullable: false),
                    Total = table.Column<float>(nullable: false),
                    Precio = table.Column<float>(nullable: false),
                    ProductoID = table.Column<int>(nullable: false),
                    InventarioID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventarioGastos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventarioGastos_Inventario_InventarioID",
                        column: x => x.InventarioID,
                        principalTable: "Inventario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventarioGastos_Producto_ProductoID",
                        column: x => x.ProductoID,
                        principalTable: "Producto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicioDetalle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    PaisID = table.Column<int>(nullable: false),
                    CargoId = table.Column<int>(nullable: false),
                    Monto = table.Column<float>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    TipoServicioID = table.Column<int>(nullable: false),
                    ZonaOrigenId = table.Column<int>(nullable: false),
                    OrigenId = table.Column<int>(nullable: true),
                    ZonaDestinoId = table.Column<int>(nullable: false),
                    DestinoId = table.Column<int>(nullable: true),
                    ProveedorID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicioDetalle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicioDetalle_Cargo_CargoId",
                        column: x => x.CargoId,
                        principalTable: "Cargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicioDetalle_Zona_DestinoId",
                        column: x => x.DestinoId,
                        principalTable: "Zona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServicioDetalle_Zona_OrigenId",
                        column: x => x.OrigenId,
                        principalTable: "Zona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServicioDetalle_Pais_PaisID",
                        column: x => x.PaisID,
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicioDetalle_Proveedor_ProveedorID",
                        column: x => x.ProveedorID,
                        principalTable: "Proveedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicioDetalle_TipoServicio_TipoServicioID",
                        column: x => x.TipoServicioID,
                        principalTable: "TipoServicio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnidadNegocio",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    CompaniaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadNegocio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnidadNegocio_Compania_CompaniaId",
                        column: x => x.CompaniaId,
                        principalTable: "Compania",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatrizAprobacion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    FlujoAprobacionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatrizAprobacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatrizAprobacion_FlujoAprobacion_FlujoAprobacionID",
                        column: x => x.FlujoAprobacionID,
                        principalTable: "FlujoAprobacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gerencia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    UnidadNegocioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gerencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gerencia_UnidadNegocio_UnidadNegocioId",
                        column: x => x.UnidadNegocioId,
                        principalTable: "UnidadNegocio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Supervisor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    UnidadNegocioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supervisor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supervisor_UnidadNegocio_UnidadNegocioId",
                        column: x => x.UnidadNegocioId,
                        principalTable: "UnidadNegocio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatrizDetalle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    IDMatrizAprobacion = table.Column<string>(nullable: true),
                    IDAprobador = table.Column<string>(nullable: true),
                    IDSuplente = table.Column<string>(nullable: true),
                    Orden = table.Column<int>(nullable: false),
                    MatrizAprobacionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatrizDetalle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatrizDetalle_MatrizAprobacion_MatrizAprobacionId",
                        column: x => x.MatrizAprobacionId,
                        principalTable: "MatrizAprobacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estatus = table.Column<int>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    Cedula = table.Column<string>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Apellido = table.Column<string>(nullable: true),
                    CargoId = table.Column<int>(nullable: true),
                    Area = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    Ciudad = table.Column<string>(nullable: true),
                    NombreSupervisor = table.Column<string>(nullable: true),
                    SupervisorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empleado_Supervisor_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Supervisor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ciudad_PaisID",
                table: "Ciudad",
                column: "PaisID");

            migrationBuilder.CreateIndex(
                name: "IX_Compania_PaisId",
                table: "Compania",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_SupervisorId",
                table: "Empleado",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_FlujoAprobacion_TipoDocumentoId",
                table: "FlujoAprobacion",
                column: "TipoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Gerencia_UnidadNegocioId",
                table: "Gerencia",
                column: "UnidadNegocioId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_ClienteID",
                table: "Inventario",
                column: "ClienteID");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioGastos_InventarioID",
                table: "InventarioGastos",
                column: "InventarioID");

            migrationBuilder.CreateIndex(
                name: "IX_InventarioGastos_ProductoID",
                table: "InventarioGastos",
                column: "ProductoID");

            migrationBuilder.CreateIndex(
                name: "IX_MatrizAprobacion_FlujoAprobacionID",
                table: "MatrizAprobacion",
                column: "FlujoAprobacionID");

            migrationBuilder.CreateIndex(
                name: "IX_MatrizDetalle_MatrizAprobacionId",
                table: "MatrizDetalle",
                column: "MatrizAprobacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_CategoriaID",
                table: "Producto",
                column: "CategoriaID");

            migrationBuilder.CreateIndex(
                name: "IX_ServicioDetalle_CargoId",
                table: "ServicioDetalle",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicioDetalle_DestinoId",
                table: "ServicioDetalle",
                column: "DestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicioDetalle_OrigenId",
                table: "ServicioDetalle",
                column: "OrigenId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicioDetalle_PaisID",
                table: "ServicioDetalle",
                column: "PaisID");

            migrationBuilder.CreateIndex(
                name: "IX_ServicioDetalle_ProveedorID",
                table: "ServicioDetalle",
                column: "ProveedorID");

            migrationBuilder.CreateIndex(
                name: "IX_ServicioDetalle_TipoServicioID",
                table: "ServicioDetalle",
                column: "TipoServicioID");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudActiva_SolicitudID",
                table: "SolicitudActiva",
                column: "SolicitudID");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudGastos_SolicitudId",
                table: "SolicitudGastos",
                column: "SolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisor_UnidadNegocioId",
                table: "Supervisor",
                column: "UnidadNegocioId");

            migrationBuilder.CreateIndex(
                name: "IX_UnidadNegocio_CompaniaId",
                table: "UnidadNegocio",
                column: "CompaniaId");

            migrationBuilder.CreateIndex(
                name: "IX_Zona_DestinoID",
                table: "Zona",
                column: "DestinoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ciudad");

            migrationBuilder.DropTable(
                name: "Concepto");

            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "Gerencia");

            migrationBuilder.DropTable(
                name: "InventarioGastos");

            migrationBuilder.DropTable(
                name: "MatrizDetalle");

            migrationBuilder.DropTable(
                name: "Moneda");

            migrationBuilder.DropTable(
                name: "ServicioDetalle");

            migrationBuilder.DropTable(
                name: "SolicitudActiva");

            migrationBuilder.DropTable(
                name: "SolicitudGastos");

            migrationBuilder.DropTable(
                name: "TipoSolicitud");

            migrationBuilder.DropTable(
                name: "Supervisor");

            migrationBuilder.DropTable(
                name: "Inventario");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "MatrizAprobacion");

            migrationBuilder.DropTable(
                name: "Cargo");

            migrationBuilder.DropTable(
                name: "Zona");

            migrationBuilder.DropTable(
                name: "Proveedor");

            migrationBuilder.DropTable(
                name: "TipoServicio");

            migrationBuilder.DropTable(
                name: "Solicitud");

            migrationBuilder.DropTable(
                name: "UnidadNegocio");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "FlujoAprobacion");

            migrationBuilder.DropTable(
                name: "Destino");

            migrationBuilder.DropTable(
                name: "Compania");

            migrationBuilder.DropTable(
                name: "TipoDocumento");

            migrationBuilder.DropTable(
                name: "Pais");
        }
    }
}
