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

    $('#Empleado').on('change', function (e) {
        var email = $("#Empleado option:selected").data('email');
        document.getElementById("addMail").value = email;
    }); 
});

function TipoSolicitudCambio() {
    const obj = document.getElementById('solicitud');
    var tipo = obj.options[obj.selectedIndex].text;
    document.getElementById("tipoSolicitud").value = tipo;
    document.getElementById("tipoDocumento").value = tipo;
}


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
        $("#msjClienteModal").modal('show');
        document.getElementById("msjClient").innerHTML = 'Debe elejir tipo -> Solicitud Anticipo/Legalizacion';
    }
}

function AddAprobador() {
    const sel = document.getElementById('Empleado');
    var tip = sel.options[sel.selectedIndex].text;
    document.getElementById("addAprobador").value = tip;

}

function ClearAll() {
    $('#tipoDocumento').val('');
    $('#addAprobador').val('');
    $('#addMail').val('');
    $("#tblFlujo tbody tr").remove();
    var clear = 'Clear';
    $.ajax({
        type: "Get",
        url: "Index",
        data: { clear: clear }
    });
}