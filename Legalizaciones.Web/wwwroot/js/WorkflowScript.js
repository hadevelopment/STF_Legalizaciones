$(document).ready(function () {

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
    document.getElementById("tipoSolicitud").value = tipo;
    document.getElementById("tipoDocumento").value = tipo;
    document.getElementById("indiceSolicitud").value = document.getElementById("solicitud").selectedIndex;
});


function SetNuevoFlujo()
{
    const obj = document.getElementById('solicitud');
    var tipo = obj.options[obj.selectedIndex].text;
    if (tipo === 'Seleccione...') {
        $("#nuevoFlujoModal").modal('hide');
        $("#msjClienteModal").modal('show');
        document.getElementById("msjClient").innerHTML = 'Debe elejir tipo -> Solicitud Anticipo/Legalizacion';
    }
    else {
        $("#nuevoFlujoModal").modal('show');
        document.getElementById("msjNuevoFlujo").innerHTML = 'Nuevo flujo de aprobacion para ' + tipo + ', el anterior flujo sera modificado';
    }

}

function VerFlujo() {
    const obj = document.getElementById('solicitud');
    var tipo = obj.options[obj.selectedIndex].text;
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
}

function ClearAll() {
    $('#tipoDocumento').val('');
    $('#addAprobador').val('');
    $('#addMail').val('');
    $("#tblFlujo tbody tr").remove();
    CloseNewFlujo();
   var clear = 'Clear';
   $.ajax({
        type: "Get",
        url: "Index",
        data: { clear: clear }
    });
}

function OpenNewFlujo() {
    $('#nuevoFlujoModal').modal('show');
}

function CloseNewFlujo() {
    $('#nuevoFlujoModal').modal('hide');
}


function GetDataAprobador(id, orden, descripcion, nombre, email, cedula, tipoSolicitud,proceso) {
  
    document.getElementById("msjDataUpdate").innerHTML = 'Actualizacion de datos para el flujo de aprobacion';
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
    OpenQuestionModal(proceso);
}

function GetDataAprobador2(id,tipoSolicitud, proceso) {

    document.getElementById("msjDataEliminar").innerHTML = 'Desea eliminar los siguientes datos?  SI para continuar , No para cancelar';

    $('#ide').val(id);
    $('#typee').val(tipoSolicitud);
    $('#procesoe').val(proceso);
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
    $("#preguntaModal").modal('hide');
    $('#dataModal').modal('show');
}

function CloseDataModal(){
    $("#dataModal").modal('hide');
}


function CloseDataModal2() {
    $("#dataModal2").modal('hide');
}

function OpenMensajeClienteModal() {
    document.getElementById("msjClient").innerHTML = 'Existen documentos Asociados a este flujo, No se puede eliminar';
    $('#cerrar').show();
    $("#msjClienteModal").modal('show');
}

function CloseMensajeClienteModal() {
    $("#msjClienteModal").modal('hide');
}