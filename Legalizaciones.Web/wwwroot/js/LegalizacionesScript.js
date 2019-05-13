
//funcion que me muestra la pantalla modal con los gastos
function AnadirGastos(boton) {
        $('#gastosModal').modal('show');

}

function EnviarReporte(boton) {
    $('#EnviarReporteModal').modal('show');

}

function ImprimirReporte(boton) {
    alert("Imprimir reporte");
}

//Funcion cuando selecciona un item del listado pantalla modal
function Seleccionar(Id) {
    //var wFecha = new Date(date.getFullYear(), date.getMonth(), date.getDate());

    $.ajax({
        type: "GET",
        url: "/Localidad/SolicitudGastos",
        datatype: "Json",
        data: { wId: Id },
        success: function (data) {
            console.log(data);
            if (data !== null) {
                $("#GastosId").val(Id);
                $("#MontoGasto").val(data.monto);
                $("#Origen").val(data.origen);
                $("#Destino").val(data.destino);
                $("#ConceptoGasto").val(data.concepto);
            }
            
        }
    });

    //aqui llenare todos los controles del formulario de legalizaciones gastos
    
    //$("#FechaGasto").val(wFecha);

    //CargarPais();
    //CargarCiudad(PaisId);

    funcion_Visible($('#RegistroDatos'));

}

function AddNuevoGasto() {
    funcion_Visible($('#RegistroDatos'));
}

function getDescPais(wID) {

}

function getDescCiudad(wID) {

}

function AgregarFilaDatagrid() {

    var FechaGasto = $("#FechaGasto").val();
    var Id = $("#GastosId").val();

    var PaisId = $("#PaisId").val();
    var Pais = "Colombia";
    var CiudadId = $("#CiudadId").val();
    var Ciudad = "Cali";
    var Monto = $("#MontoGasto").val();

    var ServicioId = $("#TiposervicioId").val();
    var Servicio = "";
    if (ServicioId == 1) {
        Servicio = "Comida";
    } else {
        Servicio = "Transporte";
    }
    var MotivoId = $("#MotivoId").val();
    var Motivo = "";
    if (MotivoId == 1) {
        Motivo = "Motivo Uno";
    } else {
        Motivo = "Motivo Dos";
    }
    var ConceptoGasto = $("#ConceptoGasto").val();

    var ProveedorId = $("#ProveedorId").val();
    var Proveedor = "";
    if (ProveedorId == 1) {
        Proveedor = "Proveedor Uno";
    } else {
        Proveedor = "Proveedor Dos";
    }

    //quite 7 colummnas xq se muestran vacias y no me cabe en la pantalla

    var row = `<tr>
                    <td class="display-none">${FechaGasto}</td> 
                    <td class="display-none">1</td>
                    <td class="display-none">${PaisId}</td> 
                    <td class="display-none">${CiudadId}</td> 
                    <td class="display-none">${ServicioId}</td> 
                    <td class="display-none">1</td> 
                    <td class="display-none">1</td> 
                    <td class="display-none">1</td>
                                  
                    <td class="Id">${Id}</td>
                    <td class="FechaGasto">${FechaGasto}</td>
                    <td>Centro de Operacion</td>
                    <td>Unidad de Negocio</td>
                    <td>Centro de Costo</td>
                    <td>${Motivo}</td>
                    <td class="PaisId">${Pais}</td>
                    <td class="CiudadId">${Ciudad}</td>
                    <td class="ServicioId">${Servicio}</td>
                    <td>${Proveedor}</td>
                    <td>${ConceptoGasto}</td>
                    <td class="Monto">${Monto}</td>
                
                    <td>
                        <a class="btn btn-danger btn-sm btnDelete">
                            <span class="glyphicon glyphicon-trash"></span>
                        </a>
                    </td>
                </tr>`;

    $('#Items').append(row);

    //Suma de Montos de Gastos legalizados
    var sum = 0;
    $('td.Monto').each(function () {
        sum += parseFloat(this.innerHTML);
    });
    $("#txMontoT").text(sum);
}



function GuardarGastos() {
    AgregarFilaDatagrid();
}


function funcion_Visible(obj) {
    obj.removeAttr("class = hide");
}

function funcion_InVisible(obj) {
    obj.addClass("hide");
}


//Funcion para cargar los datos
function cargarGastos() {
    var table = $('.tableGastos').tableToJSON({
        ignoreColumns: [16]
    });

    if (table.length > 0) {
        $("#mensajeRegistro").text("");
        $('#mensajeValidacionRegistro').hide("slow");
        $('#hdfGastosSolicitud').val(JSON.stringify(table));
        return true;
    } else {
        $("#mensajeRegistro").text("Debe añadir al menos un Gasto.");
        $('#mensajeValidacionRegistro').show("slow");
        return false;
    }

}

$("#PaisId").change(function () {
    valorPais = $("#PaisId").val();
    CargarCiudad(valorPais);
});

//-+-+-+-+-++- +-+-+-+-funciones ajax para carga de combos - +-+-+-+-+-+-+-+-


function CargarPais() {
    $.ajax({
        type: "GET",
        url: "/Localidad/Paises",
        datatype: "Json",
        success: function (data) {
            $("#PaisId").empty();
            $('#PaisId').append('<option selected value="">Seleccione...</option>');
            $.each(data, function (index, value) {
                $('#PaisId').append('<option value="' + value.id + '">' + value.nombre + '</option>');
            });
        }
    });
}

function CargarCiudad(valor) {
    $.ajax({
        type: "GET",
        url: "/Localidad/CiudadesPais",
        datatype: "Json",
        data: { paisID: valor },
        success: function (data) {
            $("#CiudadId").empty();
            $('#CiudadId').append('<option selected value="">Seleccione...</option>');
            $.each(data, function (index, value) {
                $('#CiudadId').append('<option value="' + value.id + '">' + value.nombre + '</option>');
            });
        }
    });
}


function CargarTipoServicio() {
    $('#TiposervicioId').append('<option value="1">Comida</option>');
    $('#TiposervicioId').append('<option value="2">transporte</option>');

}


function CargarMotivos() {
 $('#MotivoId').append('<option value="1">Motivo Uno</option>');
    $('#MotivoId').append('<option value="2">Motivo Dos</option>');

}


function CargarProveedor() {
    $('#ProveedorId').append('<option value="1">Proveedor Uno</option>');
    $('#ProveedorId').append('<option value="2">Proveedor Dos</option>');

}



//Cuando carga la pantalla
window.onload = function () {

    CargarTipoServicio();
    CargarProveedor();
    CargarMotivos();
    CargarPais();
}

$('.datepicker').datepicker({
    format: 'dd/mm/yyyy',
    todayHighlight: true,
    autoclose: true,
    orientation: 'bottom auto'
});

$(".datepicker").datepicker("update", new Date());
