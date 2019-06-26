
$(document).ready(function () {
    $("#TiposervicioId").change(function () {
        var idServicio = $('#TiposervicioId  option:selected').val();
        var nombreServicio = $('#TiposervicioId  option:selected').text();

        console.log(nombreServicio);
        if (nombreServicio === "Comida") {
            $('#divFechaGasto').addClass('display-none');
            $('#divOrigenDestino').addClass('display-none');
            $('#FechaGasto').val('N/A');
            $('#Origen').val('N/A');
            $('#Destino').val('N/A');
            $('#mensajeComida').removeClass('display-none');
        } else {
            $('#mensajeComida').addClass('display-none');
            $('#divFechaGasto').removeClass('display-none');
            $('#FechaGasto').val('');
            $('#Origen').val('');
            $('#Destino').val('');

            if (nombreServicio === "Transporte" || nombreServicio === "Movilidad") {
                $('#divOrigenDestino').removeClass('display-none');
            } else {
                $('#Origen').val('N/A');
                $('#Destino').val('N/A');
                $('#divOrigenDestino').addClass('display-none');
            }
        }
    });

    $('.maskMoney').focus();
   
});


//funcion que me muestra la pantalla modal con los gastos
function AnadirGastos(boton) {
    $("#mensajeRegistro").text("");
    $('#mensajeValidacionRegistro').hide("slow");
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
    $.ajax({
        type: "GET",
        url: "/Localidad/SolicitudGastos",
        datatype: "Json",
        data: { wId: Id },
        success: function (data) {
            console.log(data);
            if (data !== null) {

                if (data.servicio === "Comida") {
                    $('#divFechaGasto').addClass('display-none');
                    $('#divOrigenDestino').addClass('display-none');
                    $('#mensajeComida').removeClass('display-none');

                } else {
                    $('#divFechaGasto').removeClass('display-none');
                    $('#divOrigenDestino').removeClass('display-none');
                    $('#mensajeComida').addClass('display-none');

                }

                $("#MontoGasto").val(data.monto);
                $("#MontoGasto").focus();
                $("#Origen").val(data.origen);
                $("#Destino").val(data.destino);
                $("#FechaGasto").val(data.fechaGasto);

                var wPais = data.paisId;
                var wCiudad = data.ciudadId;
                var wServicio = data.servicioId;

                $('#PaisId').val(wPais);
                CargarCiudad(wPais);
                $('#CiudadId').val(wCiudad);
                $('#TiposervicioId').val(wServicio);
            }
        }
    });

    funcion_Visible($('#RegistroDatos'));

}

function AddNuevoGasto() {
    $('#PaisId').val('');
    $('#CiudadId').val('');
    $('#TiposervicioId').val('');
    $('#ProveedorId').val('');
    $('#ConceptoGasto').val('');
    $('#MontoGasto').val('');

    $('#divFechaGasto').removeClass('display-none');
    $('#divOrigenDestino').removeClass('display-none');
    $('#mensajeComida').addClass('display-none');
    $('#FechaGasto').val('');
    $('#Origen').val('');
    $('#Destino').val('');

    $('#gastosModal').modal('hide');
    funcion_Visible($('#RegistroDatos'));
}

function getDescPais(wID) {

}

function getDescCiudad(wID) {

}


var rowIndex = 0;
function AgregarFilaDatagrid() {

    $('#tbGastos tbody tr').each(function () {
        $projectName = $(this).find('td:eq(0)').text();
        if ($projectName === Id) {
            alert('Este gasto ya esta agregado');
            return;
        }
    });

    var Monto = $("#MontoGasto").maskMoney('unmasked')[0];
    var FechaGasto = $("#FechaGasto").val();

    var PaisId = $("#PaisId option:selected").val();
    var Pais = $("#PaisId option:selected").text();

    var CiudadId = $("#CiudadId option:selected").val();
    var Ciudad = $("#CiudadId option:selected").text();

    var ServicioId = $("#TiposervicioId option:selected").val();
    var Servicio = $("#TiposervicioId option:selected").text();


    var CentroOperacionId = $("#CentroOperacion option:selected").val();
    var CentroOperacion = $("#CentroOperacion option:selected").text();

    var UnidadNegocioId = $("#UnidadNegocio option:selected").val();
    var UnidadNegocio = $("#UnidadNegocio option:selected").text();

    var CentroCostoId = $("#CentroCosto option:selected").val();
    var CentroCosto = $("#CentroCosto option:selected").text();

    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();

    today = dd + '/' + mm + '/' + yyyy;

    var ConceptoGasto = $("#ConceptoGasto").val();

    var ProveedorId = $("#ProveedorId").val();
    var Proveedor = "";
    if (ProveedorId === 1) {
        Proveedor = "Proveedor Uno";
    } else {
        Proveedor = "Proveedor Dos";
    }

    //if (Servicio === "Comida") {
    //    Monto = CalcularGastoComidaLegalizacion();
    //}
    rowIndex = rowIndex + 1;
    GetImpuesto(ServicioId, rowIndex);

    //las primeras son para el mapeo
    //las demas son para mostrar
    var row = `<tr class="rowIndex-${rowIndex}">  
                    <td class="display-none">${PaisId}</td> 
                    <td class="display-none">${CiudadId}</td> 
                    <td class="display-none">${ServicioId}</td> 
                    <td class="display-none">${ProveedorId}</td>
                    <td class="display-none">${ConceptoGasto}</td>
                    <td class="display-none">${CentroOperacionId}</td>
                    <td class="display-none">${UnidadNegocioId}</td>
                    <td class="display-none">${CentroCostoId}</td>
                    <td>${today}</td>
                    <td class="display-none">${CentroOperacion}</td>
                    <td class="display-none">${UnidadNegocio}</td>
                    <td class="display-none">${CentroCosto}</td>
                    <td>${FechaGasto}</td>
                    <td>${Pais}</td>
                    <td>${Ciudad}</td>
                    <td>${Servicio}</td>
                    <td>${Proveedor}</td>
                    <td>${ConceptoGasto}</td>
                    <td id="tax${rowIndex}"></td>
                    <td class="monto text-right"><input type="text" class="txtLabel maskMoney" readonly="readonly" id="monto-${rowIndex}" value="${Monto}"/></td>
                    <td>
                        <a class="btn btn-danger btn-sm btnDelete" onclick='remove(this)'>
                            <span class="glyphicon glyphicon-trash"></span>
                        </a>
                    </td>
                </tr>`;

    $('#Items').append(row);

    //Se aplica Formato Moneda al monto
    $('#monto-' + rowIndex).maskMoney({ thousands: ',', decimal: '.', allowZero: true, suffix: '' });
    $('#monto-' + rowIndex).focus();


    CalcularMontos();
}



function GetImpuesto(idServicio,i) {
    return $.ajax({
        type: "GET",
        url: "/UNOEE/GetTipoImpuesto",
        data: { id: idServicio },
        datatype: "Json",
        success: function (data) {
            var obj = '#tax' + i;
            if (data.Valor != 0 && data.valor != null) {
                var value = data.valor.replace('.', ',');
                $(obj).text(value);
            }
            else {
                $(obj).text('0,00');
            }
        }
    });
}


function GuardarGastos() {
    if (ValidarGastos()) {
        AgregarFilaDatagrid();
    }
}

function ValidarGastos() {
    var FechaGasto = $('#FechaGasto').val();
    var CentroOperacion = $('#CentroOperacion').val();
    var UnidadNegocio = $('#UnidadNegocio').val();
    var CentroCosto = $('#CentroCosto').val();
    var Pais = $('#PaisId').val();
    var Ciudad = $('#CiudadId').val();
    var ServicioId = $('#TiposervicioId  option:selected').val();
    var Servicio = $('#TiposervicioId  option:selected').text();
    var Proveedor = $('#ProveedorId').val();
    var Concepto = $('#ConceptoGasto').val();
    var Valor = $('#MontoGasto').maskMoney('unmasked')[0];

    var Origen = $('#Origen').val();
    var Destino = $('#Destino').val();

    if (CentroOperacion !== "" && UnidadNegocio !== "" && CentroCosto !== "" && Pais !== "" && Ciudad !== "" && Servicio !== "" && Concepto !== "" && Valor !== "" & Valor > 0) {
        //Validacion de Transporte
        if (Servicio === "Transporte" || Servicio === "Movilidad") {
            if (Origen === "" || Destino === "") {
                $("#mensajeGastos").text("Debe específicar la ruta del transporte.");
                $('#mensajeValidacionGastos').show("slow");
                return false;
            } else if (FechaGasto === "") {
                $("#mensajeGastos").text("Debe específicar la fecha del gasto.");
                $('#mensajeValidacionGastos').show("slow");
                return false;
            } else {
                $("#mensajeGastos").text("");
                $('#mensajeValidacionGastos').hide("slow");
                return true;
            }
        }

        $("#mensajeGastos").text("");
        $('#mensajeValidacionGastos').hide("slow");
        return true;
    } else {
        $("#mensajeGastos").text("Faltan datos por especificar.");
        $('#mensajeValidacionGastos').show("slow");
        return false;
    } 
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
        $('#hdfGastosLegalizacion').val(JSON.stringify(table));
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


function CargarListas() {

    $.ajax({
        type: "GET",
        url: "/UNOEE/GetUnoeeProveedores",
        datatype: "Json",
        success: function (data) {
            $("#ProveedorId").empty();
            $('#ProveedorId').append('<option selected value="">Seleccione...</option>');
            $.each(data.resultado, function (index, value) {
                $('#ProveedorId').append('<option value="' + value.idProveedor + '">' + value.nombre + '</option>');
            });
        }
    });

    $.ajax({
        type: "GET",
        url: "/UNOEE/GetServicios",
        datatype: "Json",
        success: function (data) {  
            $("#TiposervicioId").empty();
            $('#TiposervicioId').append('<option selected value="">Seleccione...</option>');
            $.each(data.resultado, function (index, value) {
                $('#TiposervicioId').append('<option value="' + value.idTipoServicio.trim() + '">' + value.nombre + '</option>');
            });
        }
    });


    $.ajax({
        type: "GET",
        url: "/UNOEE/GetCentroOperaciones",
        datatype: "Json",
        success: function (data) {
            $("#CentroOperacion").empty();
            $('#CentroOperacion').append('<option selected value="">Seleccione...</option>');
            $.each(data.resultado, function (index, value) {
                $('#CentroOperacion').append('<option value="' + value.idCentroOperacion + '">' + value.nombre + '</option>');
            });
        }
    });
 
    $.ajax({
        type: "GET",
        url: "/Localidad/Paises",
        datatype: "Json",
        success: function (data) {
            $("#PaisId").empty();
            $.each(data, function (index, value) {
                $('#PaisId').append('<option value="' + value.id + '">' + value.nombre + '</option>');
            });
        }
    });

    $.ajax({
        type: "GET",
        url: "/Localidad/CiudadesPais",
        datatype: "Json",
        data: { paisID: "" },
        success: function (data) {
            $("#CiudadId").empty();
            $.each(data, function (index, value) {
                $('#CiudadId').append('<option value="' + value.id + '">' + value.nombre + '</option>');
            });
        }
    });

 

    CargarMotivos ();
}


function CargarMotivos() {
 $('#MotivoId').append('<option value="1">Motivo Uno</option>');
    $('#MotivoId').append('<option value="2">Motivo Dos</option>');

}



//Cuando carga la pantalla
window.onload = function () {

    CargarListas();
    //CargarMotivos();
    //CargarPais();

    var w = $('#txSaldo').val();
    $('#txSaldo').val(w); 
}


$('.datepicker').datepicker({
    language: 'es',
    format: 'yyyy-mm-dd',
    todayHighlight: true,
    autoclose: true,
    orientation: 'bottom auto'
}).on("changeDate", function (dateText, inst) {
    var id = $(this).attr('id');
    console.log(id);
    });

$(".datepicker").datepicker("update", new Date());

function remove(tr) {
    $(tr).parent().parent().remove();
    CalcularMontos();
    return false;
}


function CalcularGastoComidaLegalizacion() {
    var wServicio = $('#TiposervicioId option:selected').text();
    var wMonto = $('#MontoGasto').maskMoney('unmasked')[0];

    if (wServicio === "Comida") {
        var FechaDesde = $("#FechaDesde").val();
        var FechaHasta = $("#FechaHasta").val();

        var fecha1 = moment(FechaDesde);
        var fecha2 = moment(FechaHasta);

        var wDias = fecha2.diff(fecha1, 'days');
        console.log('dias ' + wDias);
        wDias = wDias + 1;

        var wMontoTotal = wMonto * parseInt(wDias);

        return wMontoTotal;
    }

    return wMonto;

}

function CalcularMontos() {
    //Suma de Montos de Gastos legalizados
    var montoGastos = 0;
    $('td.monto input').each(function () {
        montoGastos += parseFloat($(this).maskMoney('unmasked')[0]);
    });

    $('#txMontoFaltante').val(montoGastos);
    $('#txMontoFaltante').focus();

    var montoAnticipo = $('#txMontoAnticipo').maskMoney('unmasked')[0];

    if (montoGastos > montoAnticipo) {
        $('#txSaldo').removeClass('fontGreen');
        $('#txSaldo').addClass('fontRed');
        $('#mensajeSaldo').text('Saldo a Favor del Empleado');
    } else if (montoGastos < montoAnticipo && montoGastos > 0) {
        $('#txSaldo').removeClass('fontRed');
        $('#txSaldo').addClass('fontGreen');
        $('#mensajeSaldo').text('Saldo a Favor de la Empresa');
    } else if (montoGastos === 0){
        $('#txSaldo').removeClass('fontRed');
        $('#txSaldo').removeClass('fontGreen');
        $('#mensajeSaldo').text('');
    }

    var saldo = montoAnticipo - montoGastos;

    $('#txSaldo').val(saldo);
    $('#txSaldo').focus();
}
