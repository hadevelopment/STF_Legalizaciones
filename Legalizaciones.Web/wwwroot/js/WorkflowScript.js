﻿$(document).ready(function () {
  

    var arrayEmpleadoPermiso = [];
    $.ajax({
        type: "GET",
        url: "/UNOEE/Empleados?filtroCedula=true",
        datatype: "Json",
        beforeSend: function () {
            // console.log("Before Send Request");
        },
        success: function (data) {
            data.length > 0 ? $('#triggerModal').removeAttr('disabled') : $('#triggerModal').attr('disabled', 'disabled');
            $("#Empleado").empty();
            $('#Empleado').append('<option selected disabled value="-1">Seleccione...</option>');
            $.each(data, function (index, value) {
                arrayEmpleadoPermiso.push(value);
                $('#Empleado').append('<option data-email="' + value.correo + '" value="' + value.cedula + '">' + value.nombre + '</option>');
            });
        },
        complete: function () {
            // console.log(arrayEmpleadoPermiso);
        }
    });

    //Propiedades del dropdown destinos
    $("#Empleado").select2({
        multiple: false,
        placeholder: 'Seleccione...'
    });


    var arrayEmpleadoPermiso2 = [];
    $.ajax({
        type: "GET",
        url: "/UNOEE/Empleados?filtroCedula=true",
        datatype: "Json",
        beforeSend: function () {
            // console.log("Before Send Request");
        },
        success: function (data) {
            data.length > 0 ? $('#triggerModal').removeAttr('disabled') : $('#triggerModal').attr('disabled', 'disabled');
            $("#Empleado2").empty();
            $('#Empleado2').append('<option selected disabled value="-1">Seleccione...</option>');
            $.each(data, function (index, value) {
                arrayEmpleadoPermiso2.push(value);
                $('#Empleado2').append('<option data-email="' + value.correo + '" value="' + value.cedula + '">' + value.nombre + '</option>');
            });
        },
        complete: function () {
            // console.log(arrayEmpleadoPermiso);
        }
    });


    //Propiedades del dropdown destinos
    $("#Empleado2").select2({
        multiple: false,
        placeholder: 'Seleccione...'
    });

    $('#Empleado2').on('change', function (e) {
        var email = $("#Empleado2 option:selected").data('email');
        document.getElementById("email").value = email;
        var tipo = this.options[this.selectedIndex].text;
        document.getElementById("nombre").value = tipo;
        var cedula = $("#Empleado2 option:selected").val();
        document.getElementById("cedula").value = cedula;
    }); 


    $("#Destino").select2({
        multiple: false,
        placeholder: 'Seleccione...'
    });
    var arrayDestino = [];
    $.ajax({
        type: "GET",
        url: "/WorkFlow/GetDestino",
        dataType: "json",
        beforeSend: function () {
            // console.log("Before Send Request");
        },
        success: function (data) {
            console.log(data);
            data.length > 0 ? $('#triggerModal').removeAttr('disabled') : $('#triggerModal').attr('disabled', 'disabled');
            $("#Destino").empty();
            $('#Destino').append('<option selected disabled value="-1">Seleccione...</option>');
            $.each(data, function (index, value) {
                arrayDestino.push(value);
                $('#Destino').append('<option  value="' + value.id + '">' + value.destino + '</option>');
            });
        },
        complete: function () {
            // console.log(arrayEmpleadoPermiso);
        }
    });

   /* $("#Moneda").select2({
        multiple: false,
        placeholder: 'Seleccione...'
    });*/

    $('#Moneda').hide();
    $('#Currency').hide();
});



 /*function TipoSolicitudCambio() {
    const obj = document.getElementById('solicitud');
    var tipo = obj.options[obj.selectedIndex].text;
    document.getElementById("tipoSolicitud").value = tipo;
    document.getElementById("tipoDocumento").value = tipo;
    document.getElementById("indiceSolicitud").value = $('#solicitud').index; 
}*/

$('#solicitud').on('change', function (e) {
 
    const obj = document.getElementById('solicitud');
    var tipo = obj.options[obj.selectedIndex].text;
    var indice = document.getElementById("solicitud").selectedIndex;
    document.getElementById("tipoSolicitud").value = tipo;
    document.getElementById("tipoDocumento").value = tipo;
    document.getElementById("indiceSolicitud").value = indice;
    $('#flow').css('visibility', 'hidden'); 
});

$('#Destino').on('change', function (e) {
 
    const obj = document.getElementById('Destino');
    var destino = obj.options[obj.selectedIndex].text;
    var indice = document.getElementById("Destino").selectedIndex;
    $('#destinoId').val(indice);
    /*var arrayMoneda = [];
    $.ajax({
        type: "GET",
        url: "/WorkFlow/GetMoneda",
        data: { id: indice},
        dataType: "json",
        beforeSend: function () {
            // console.log("Before Send Request");
        },
        success: function (data) {
            console.log(data);
            data.length > 0 ? $('#triggerModal').removeAttr('disabled') : $('#triggerModal').attr('disabled', 'disabled');
            $("#Moneda").empty();
            $('#Moneda').append('<option selected disabled value="-1">Seleccione...</option>');
            $.each(data, function (index, value) {
                arrayMoneda.push(value);
                $('#Moneda').append('<option  value="' + value.id + '">' + value.moneda + '</option>');
            });
        },
        complete: function () {
            // console.log(arrayEmpleadoPermiso);
        }
    });*/ 
});


function SetNuevoFlujo()
{
    document.getElementById('montoMaximo').readOnly = false;
    document.getElementById('montoMinimo').readOnly = false;
    const obj = document.getElementById('solicitud');
    var tipo = obj.options[obj.selectedIndex].text;
    document.getElementById('tipoDocumento').value = tipo;
    document.getElementById("msjNuevoFlujo").innerHTML = 'Agregar paso para el flujo de aprobacion ' + tipo;
    var paso = $('#paso').val();
    if ( paso == '' || paso == 0) {
        $("#Empleado").prop("disabled", true);
    }
    else {
        $("#Empleado").prop("disabled", false);
    }
    if (tipo === 'Seleccione...') {
        $("#nuevoFlujoModal").modal('hide');
        $("#msjClienteModal").modal('show');
        document.getElementById("msjClient").innerHTML = 'Debe elejir tipo -> Solicitud Anticipo/Legalizacion';
    }
    else {    
        $("#nuevoFlujoModal").modal('show');
    }
}

function SetAgregarPasoFlujo()
{
    $('#Destino').select2(false);
    const obj = document.getElementById('solicitud');
    var tipo = obj.options[obj.selectedIndex].text;
    document.getElementById('tipoDocumento').value = tipo;
    document.getElementById("msjNuevoFlujo").innerHTML = 'Agregar paso para el flujo de aprobacion ' + tipo;

    if (tipo === 'Seleccione...') {
        $("#nuevoFlujoModal").modal('hide');
        $("#msjClienteModal").modal('show');
        document.getElementById("msjClient").innerHTML = 'Debe elejir tipo -> Solicitud Anticipo/Legalizacion';
    }
}

function VerFlujo() {
    const obj = document.getElementById('solicitud');
    var tipo = obj.options[obj.selectedIndex].text;
    document.getElementById("tipoSolicitud").value = tipo;
    document.getElementById("tipoDocumento").value = tipo;
    document.getElementById("indiceSolicitud").value = document.getElementById("solicitud").selectedIndex;
    if (tipo === 'Seleccione...') {
        $('#cerrar').hide();
        $("#msjClienteModal").modal('show');
        document.getElementById("msjClient").innerHTML = 'Debe elejir tipo -> Solicitud Anticipo/Legalizacion';
    }
}

function AddAprobador() {
    const sel = document.getElementById('Empleado');
    var tip = sel.options[sel.selectedIndex].text;
    document.getElementById("addAprobador").value = tip;
    var email = $("#Empleado option:selected").data('email');
    document.getElementById("addMail").value = email;
    console.log('AddAprobador');
}

function ClearAll() {
    $('#descripcion').val('');
    $('#tipoDocumento').val('');
    $('#addAprobador').val('');
    $('#addMail').val('');
    $("#tblFlujo tbody tr").remove();
    CloseNewFlujo();
   /*var clear = 'Clear';
   $.ajax({
        type: "Get",
        url: "Index",
        data: { clear: clear }
    });*/
}

function OpenNewFlujo() {
    const obj = document.getElementById('solicitud');
    var tipo = obj.options[obj.selectedIndex].text;
    document.getElementById("msjNuevoFlujo").innerHTML = 'Flujo de aprobacion ' + tipo;
    $('#nuevoFlujoModal').modal('show');
}

function CloseNewFlujo() {
    $('#nuevoFlujoModal').modal('hide');
}


function GetDataAprobador(id, orden, descripcion, nombre, email, cedula, tipoSolicitud,montoMinimo,montoMaximo,destinoId,flujoSolicitudId,proceso) {
    document.getElementById("msjPreguntaModal").innerHTML = 'Desea actualizar los siguientes datos?  SI para continuar , No para cancelar';
    $("#modificar").val('Actualizar');
  
    $('#id').val(id);
    $('#orden').val(orden);
    $('#descripcionT').val(descripcion);
    $('#nombre').val(nombre);
    $('#email').val(email);
    $('#cedula').val(cedula);
    $('#type').val(tipoSolicitud);
    $('#proceso').val(proceso);
    $('#minimo').val(montoMinimo);
    $('#maximo').val(montoMaximo);
    $('#destinoIde').val(destinoId);
    OpenQuestionModal(proceso);
}

function GetDataAprobador2(id, tipoSolicitud, montoMinimo, montoMaximo, destinoId, flujoSolicitudId, proceso) {

    document.getElementById("msjDataEliminar").innerHTML = 'Desea eliminar los siguientes datos?  SI para continuar , No para cancelar';
    $('#ide').val(id);
    $('#typee').val(tipoSolicitud);
    $('#procesoe').val(proceso);
    $('#minimoe').val(montoMinimo);
    $('#maximoe').val(montoMaximo);
    $('#destinoIdee').val(destinoId);
    OpenQuestionModal(proceso);
}

function OpenQuestionModal(proceso) {
    if(proceso === 'actualizar')
        $('#preguntaModal').modal('show');
    else if (proceso === 'eliminar')
        $('#dataModal2').modal('show');
}

function CloseQuestionModal(proceso) {
  
    $("#dataModal2").modal('hide');
    $("#preguntaModal").modal('hide');
}

function OpenDataModal() {
    var tipoFlujo = document.getElementById('tipoFlujo').value;
    document.getElementById("msjDataUpdate").innerHTML = 'Actualizacion de datos para el flujo de aprobacion ' + tipoFlujo;
    $("#preguntaModal").modal('hide');
    $('#dataModal').modal('show');
}

function CloseDataModal() {

    $("#dataModal").modal('hide');
}


function CloseDataModal2() {
    $("#dataModal2").modal('hide');
}

function OpenMensajeClienteModal(msj) {
    document.getElementById("msjClient").innerHTML = msj;
    $('#cerrar').show();
    $("#msjClienteModal").modal('show');
}

function CloseMensajeClienteModal() {
    $("#msjClienteModal").modal('hide');
}