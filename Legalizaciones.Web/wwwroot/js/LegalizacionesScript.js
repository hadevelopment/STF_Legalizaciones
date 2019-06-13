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


    
});


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

                $("#GastosId").val(Id);
                $("#MontoGasto").val(data.monto);
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
    $('#GastosId').val('');
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

function AgregarFilaDatagrid() {

    var Id = $("#GastosId").val();

    $('#tbGastos tbody tr').each(function () {
        $projectName = $(this).find('td:eq(0)').text();
        if ($projectName === Id) {
            alert('Este gasto ya esta agregado');
            return;
        }
    });

    var Monto = $("#MontoGasto").val();
    var FechaGasto = $("#FechaGasto").val();

    var PaisId = $("#PaisId option:selected").val();
    var Pais = $("#PaisId option:selected").text();

    var CiudadId = $("#CiudadId option:selected").val();
    var Ciudad = $("#CiudadId option:selected").text();

    //var Monto = $("#MontoGasto").val();

    var ServicioId = $("#TiposervicioId option:selected").val();
    var Servicio = $("#TiposervicioId option:selected").text();

    var CentroOperacion = $("#CentroOperacion option:selected").val();
    var UnidadNegocio = $("#UnidadNegocio option:selected").val();
    var CentroCosto = $("#CentroCosto option:selected").val();

    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();

    today = mm + '/' + dd + '/' + yyyy;

    var ConceptoGasto = $("#ConceptoGasto").val();

    var ProveedorId = $("#ProveedorId").val();
    var Proveedor = "";
    if (ProveedorId === 1) {
        Proveedor = "Proveedor Uno";
    } else {
        Proveedor = "Proveedor Dos";
    }

    if (Servicio === "Comida") {
        Monto = CalcularGastoComidaLegalizacion();
    }

    //las primeras son para el mapeo
    //las demas son para mostrar
    var row = `<tr>  
                    <td class="display-none">${PaisId}</td> 
                    <td class="display-none">${CiudadId}</td> 
                    <td class="display-none">${ServicioId}</td> 
                    <td class="display-none">${ProveedorId}</td>
                    <td class="display-none">${ConceptoGasto}</td>
                    <td>${Id}</td>
                    <td>${today}</td>
                    <td>${CentroOperacion}</td>
                    <td>${UnidadNegocio}</td>
                    <td>${CentroCosto}</td>
                    <td>${FechaGasto}</td>
                    <td>${Pais}</td>
                    <td>${Ciudad}</td>
                    <td>${Servicio}</td>
                    <td>${Proveedor}</td>
                    <td>${ConceptoGasto}</td>
                    <td class="Monto">${Monto}</td>                
                    <td>
                        <a class="btn btn-danger btn-sm btnDelete" onclick='remove(this)'>
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

    //$("#txMontoT").text(sum);
    
    var wSaldo = parseFloat($('#txSaldo').val());
    var wDiferencia = wSaldo - sum; 

    if (wDiferencia > 0) {
        funcion_InVisible($("#txMontoSobrante"));
        funcion_Visible($("#txMontoFaltante"));
        $('#txMontoFaltante').text(sum);
    } else {
        funcion_InVisible($("#txMontoFaltante"));
        funcion_Visible($("#txMontoSobrante"));
        $('#txMontoSobrante').text(sum);
    }
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
    var Valor = $('#MontoGasto').val();

    var Origen = $('#Origen').val();
    var Destino = $('#Destino').val();

    if (CentroOperacion !== "" && UnidadNegocio !== "" && CentroCosto !== "" && Pais !== "" && Ciudad !== "" && Servicio !== "" && Concepto !== "" && Valor !== "") {
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
        url: "/UNOEE/Servicios",
        datatype: "Json",
        success: function (data) {
            $("#TiposervicioId").empty();
            $('#TiposervicioId').append('<option selected value="">Seleccione...</option>');
            $.each(data, function (index, value) {
                $('#TiposervicioId').append('<option value="' + value.id + '">' + value.nombre + '</option>');
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

    CargarProveedor();
    CargarMotivos ();
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

    CargarListas();
    //CargarMotivos();
    //CargarPais();

    var w = $('#txSaldo').val();
    w = w.replace(",", ".");
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
    var sum = 0;
    $('td.monto').each(function () {
        sum += parseFloat(this.innerHTML);
    });


    $("#txMontoSobrante").val(sum);

    var wSaldo = parseFloat($('#txSaldo').val());
    var wDiferencia = wSaldo - sum;

    if (wDiferencia > 0) {
        funcion_InVisible($("#txMontoSobrante"));
        funcion_Visible($("#txMontoFaltante"));
        $('#txMontoFaltante').text(sum);
    } else {
        funcion_InVisible($("#txMontoFaltante"));
        funcion_Visible($("#txMontoSobrante"));
        $('#txMontoSobrante').text(sum);
    }
    return false;
}


function CalcularGastoComidaLegalizacion() {
    var wServicio = $('#TiposervicioId option:selected').text();
    var wMonto = $('#MontoGasto').val();

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

    //var wServicio = $('#TiposervicioId option:selected').text();
    //var wMonto = $('#MontoGasto').val();

    //if (wServicio === "Comida") {
    //    var FechaDesde = $("#FechaDesde").val();
    //    var FDdia = FechaDesde.substr(0, 2);
    //    var FDMes = FechaDesde.substr(3, 2);
    //    var FDAnno = FechaDesde.substr(6, 4);
    //    var wFDFormato = FDAnno + "-" + FDMes + "-" + FDdia;

    //    var FechaHasta = $("#FechaHasta").val();
    //    var FHdia = FechaHasta.substr(0, 2);
    //    var FHMes = FechaHasta.substr(3, 2);
    //    var FHAnno = FechaHasta.substr(6, 4);
    //    var wFHFormato = FHAnno + "-" + FHMes + "-" + FHdia;

    //    var fecha1 = moment(wFDFormato);
    //    var fecha2 = moment(wFHFormato);

    //    var wDias = fecha2.diff(fecha1, 'days');

    //    if (wDias > 1) {
    //        var wMontoTotal = wMonto * parseInt(wDias);

    //        return wMontoTotal;
    //    }


    //}

    //return wMonto;

}
