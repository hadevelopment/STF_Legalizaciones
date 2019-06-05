$(document).ready(function () {

    $("#solicitud").select2({
        multiple: false,
        placeholder: 'Seleccione...'
    });

    $("#flujos").select2({
        multiple: false,
        placeholder: 'Seleccione...'
    });


    var arraySolicitud = [];
    $.ajax({
        type: "GET",
        url: "/WorkFlow/GetDocumentType",
        dataType: "json",
        beforeSend: function () {
            
        },
        success: function (data) {
            console.log(data);
            data.length > 0 ? $('#triggerModal').removeAttr('disabled') : $('#triggerModal').attr('disabled', 'disabled');
            $("#solicitud").empty();
            $('#solicitud').append('<option selected disabled value="-1">Seleccione...</option>');
            $.each(data, function (index, value) {
                arraySolicitud.push(value);
                $('#solicitud').append('<option  value="' + value.id + '">' + value.documento + '</option>');
            });
        },
        complete: function () {
          
        }
    });

    /* $("#Moneda").select2({
         multiple: false,
         placeholder: 'Seleccione...'
     });*/


    EmpleadoAprobador();
    EmpleadoAprobador2();
    Destinos();

    $('#Empleado2').on('change', function (e) {
        var email = $("#Empleado2 option:selected").data('email');
        document.getElementById("email").value = email;
        var tipo = this.options[this.selectedIndex].text;
        document.getElementById("nombre").value = tipo;
        var cedula = $("#Empleado2 option:selected").val();
        document.getElementById("cedula").value = cedula;
    }); 

    $('#Moneda').hide();
    $('#Currency').hide();
});

function EmpleadoAprobador() {

    $("#Empleado").select2({
        multiple: false,
        placeholder: 'Seleccione...'
    });

    var arrayEmpleadoPermiso = [];
    $.ajax({
        type: "GET",
        url: "/UNOEE/Empleados?filtroCedula=true",
        datatype: "Json",
        beforeSend: function () {
         
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
            
        }
    });
}


function EmpleadoAprobador2() {

    $("#Empleado2").select2({
        multiple: false,
        placeholder: 'Seleccione...'
    });

    var arrayEmpleadoPermiso2 = [];
    $.ajax({
        type: "GET",
        url: "/UNOEE/Empleados?filtroCedula=true",
        datatype: "Json",
        beforeSend: function () {
         
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
            
        }
    });
}

function Destinos() {

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
           
        }
    });}



$('#solicitud').on('change', function (e) {
 
    const obj = document.getElementById('solicitud');
    var tipo = obj.options[obj.selectedIndex].text;
    var indice = document.getElementById("solicitud").selectedIndex;
    document.getElementById("tipoSolicitud").value = tipo;
    document.getElementById("tipoDocumento").value = tipo;
    document.getElementById("indiceSolicitud").value = indice;
    document.getElementById("tipoDocumentoe").value = obj.value;
    $("#tblAprobadores thead tr").remove();
    $("#tblAprobadores tbody tr").remove();
    $("#addPasoFlow").remove();
    FlujosAprobacion(indice);
});

$('#flujos').on('change', function (e) {

    const obj = document.getElementById('flujos');
    var tipo = obj.options[obj.selectedIndex].text;
    var indice = document.getElementById("flujos").selectedIndex;
    document.getElementById("rango").value = tipo;
    document.getElementById("desRange").value = tipo;
    document.getElementById("rangeId").value = document.getElementById('flujos').value;
    $("#tblAprobadores thead tr").remove();
    $("#tblAprobadores tbody tr").remove();
    $("#addPasoFlow").remove();
});

$('#Destino').on('change', function (e) {
 
    const obj = document.getElementById('Destino');
    var destino = obj.options[obj.selectedIndex].text;
    var indice = document.getElementById("Destino").selectedIndex;
    document.getElementById("destino").value = destino;
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


/*$('#nuevoFlujoModal').on('hidden.bs.modal', function () {
    var lastChild = $("#flujos option:last-child").val();
    $("#flujos").val(lastChild);
    $("#flujos").trigger('change');
});*/

function FlujosAprobacion(indice) {

    var arrayFlujos = [];
    $.ajax({
        type: "GET",
        url: "/WorkFlow/GetFlujos",
        data: { id: indice },
        dataType: "json",
        beforeSend: function () {
           
        },
        success: function (data) {
            console.log(data);
            data.length > 0 ? $('#triggerModal').removeAttr('disabled') : $('#triggerModal').attr('disabled', 'disabled');
            $("#flujos").empty();
            $('#flujos').append('<option selected disabled value="-1">Seleccione...</option>');
            $.each(data, function (index, value) {
                arrayFlujos.push(value);
                $('#flujos').append('<option  value="' + value.id + '">' + value.descripcion + '</option>');
            });
        },
        complete: function () {
            
        }
    });
}


function SetNuevoFlujo()
{
    $("#tblFlujo thead tr").remove();
    $("#tblFlujo tbody tr").remove();
    EmpleadoAprobador();
    Destinos();
    $('#Destino').css('visibility', 'show'); 
    $("#Destino").prop("disabled", false);
    $("#Empleado").prop("disabled", true);
    $("#Destino").val("0");
    document.getElementById('paso').value = '1';
    document.getElementById('destinoId').value = '0';
    document.getElementById('tipoDocumento').value = '';
    document.getElementById('addAprobador').value = '';
    document.getElementById('addMail').value = '';
    document.getElementById('descripcion').value = '';
    document.getElementById('descripcion').readOnly = true;
    document.getElementById('montoMaximo').readOnly = false;
    document.getElementById('montoMinimo').readOnly = false;
    document.getElementById('paso').value = 1;
    document.getElementById('descripcion').value = '';
    document.getElementById('montoMaximo').placeholder = '0,00';
    document.getElementById('montoMinimo').placeholder = '0,00';
    const obj = document.getElementById('solicitud');
    var tipo = obj.options[obj.selectedIndex].text;
    document.getElementById('tipoDocumento').value = tipo;
    document.getElementById('idDocument').value = $('#solicitud').val();
    document.getElementById("msjNuevoFlujo").innerHTML = 'Nuevo Flujo de aprobacion para ' + tipo;
    var paso = $('#paso').val();
    if (paso == 1) {
        $("#Empleado").prop("disabled", true);
    }
    else {
        $("#Empleado").prop("disabled", false);
        document.getElementById('descripcion').readOnly = false;
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
    EmpleadoAprobador();
    Destinos();
    $("#Empleado").prop("disabled", false);
    const documento = document.getElementById('solicitud');
    const descripcion = document.getElementById('flujos');
    var documentoId = documento.value;
    var flujoId = descripcion.value;
    var tipo = documento.options[documento.selectedIndex].text;
    var txtFlujo = descripcion.options[descripcion.selectedIndex].text;
    var [min,max,des] = txtFlujo.split("-");
    min = min.trim();
    max = max.trim();
    des = des.trim();
    document.getElementById('montoMinimo').value = min;
    document.getElementById('montoMaximo').value = max;
    document.getElementById('descripcion').readOnly = false;
    var indiceDestino = 0;
    if (des == 'Nacional') { indiceDestino = 1; }
    else if (des == 'Internacional') { indiceDestino = 2; }
    document.getElementById("Destino").selectedIndex = indiceDestino;
    $("#Destino").val(indiceDestino);
    $("#Destino").prop("disabled", true);
    document.getElementById("msjNuevoFlujo").innerHTML = 'Agregar paso para el flujo de aprobacion ' + tipo + ' - ' + des;
    document.getElementById('destinoId').value = indiceDestino;
    document.getElementById('tipoDocumento').value = tipo;
    document.getElementById('idDocument').value = documentoId;
    var paso = document.getElementById("tblAprobadores").rows.length; 
    document.getElementById('paso').value = paso;
    $("#nuevoFlujoModal").modal('show');
}


function EnviarDataAprobador()
{
    var paso = document.getElementById('paso').value;
    var tipoDocumento = document.getElementById('tipoDocumento').value;
    var idDocumento = document.getElementById('idDocument').value;
    var montoMinimo = document.getElementById('montoMinimo').value;
    var montoMaximo = document.getElementById('montoMaximo').value;
    var destino = document.getElementById('destino').value;
    var destinoId = document.getElementById('destinoId').value;
    if (paso > 1)
    {
        var aprobador = document.getElementById('addAprobador').value;
        var empleado = document.getElementById('Empleado').value;
        var mail = document.getElementById('addMail').value;
        var descripcion = document.getElementById('descripcion').value;
        if (aprobador == '') {
            alert('El nombre del aprobador es un campo requerido');
            return false;
        }
        if (descripcion == '' ) {
            alert('La descripcion es un campo requerido');
            return false;
        }
    }

    if (paso == 1) {

        if (destinoId == 0 || montoMaximo == 0 || montoMinimo == 0) {
            alert('Debe elegir destino y especificar montos mayores a cero.');
            return false;
        }
        $.ajax({
            url: "/WorkFlow/ExisteRangoAprobacion",
            data: { destinoId: destinoId, montoMaximo: montoMaximo, montoMinimo: montoMinimo, idDocumento: idDocumento },
            dataType: 'json',
            type: 'GET',
            cache: false,
            success: function (data) {
                console.log(data.id);
                if (data.id > 0) {
                    alert('Ya existe un flujo de aprobacion destino ' + destino + ' con montos entre ' + montoMinimo + ' y ' + montoMaximo + ', debe elegir montos diferentes.' );
                    return false;
                }
                else {

                    $.ajax({
                        url: "/WorkFlow/CreateFlujoDocumento",
                        data: { paso: paso, tipoDocumento: tipoDocumento, idDocumento: idDocumento, aprobador: aprobador, empleado: empleado, descripcion: descripcion, mail: mail, destino: destino, destinoId: destinoId, montoMaximo: montoMaximo, montoMinimo: montoMinimo },
                        dataType: 'json',
                        type: 'POST',
                        cache: false,
                        success: function (data) {
                            paso = parseInt(paso) + 1;
                            EmpleadoAprobador();
                            document.getElementById('paso').value = paso;
                            document.getElementById('descripcion').value = '';
                            document.getElementById('montoMaximo').readOnly = true;
                            document.getElementById('montoMinimo').readOnly = true;
                            $("#Empleado").prop("disabled", false);
                            $("#Destino").prop("disabled", true);
                            document.getElementById('descripcion').readOnly = false;
                            $("#addPasoFlow").remove();
                            var indice = document.getElementById("solicitud").selectedIndex;
                            FlujosAprobacion2(indice);

                            /*var f = document.getElementById('flujos').length;
                            var lastChild = $("#flujos option:last-child").val();
                            $("#flujos").val(lastChild);
                            $("#flujos").trigger('change');
                            $("#flujos").val(f - 1);*/

                            CreateTablaAprobadores(data);
                            CreateTablaFlujoAprobadores(data);
                        },
                        error: function (d) {
                            alert("ERROR INESPERADO.");
                        }
                    });
                }
            },
            error: function (d) {
                alert("ERROR INESPERADO.");
            }
        });
    }
    else {
        if (aprobador == ''|| descripcion == '') {
            alert('Debe especificar los campos aprobador y descripcion');
            return false;
        }
        $.ajax({
            url: "/WorkFlow/CreateFlujoDocumento",
            data: { paso: paso, tipoDocumento: tipoDocumento, idDocumento: idDocumento, aprobador: aprobador, empleado: empleado, descripcion: descripcion, mail: mail, destino: destino, destinoId: destinoId, montoMaximo: montoMaximo, montoMinimo: montoMinimo },
            dataType: 'json',
            type: 'POST',
            cache: false,
            success: function (data) {
                paso = parseInt(paso) + 1;
                EmpleadoAprobador();
                document.getElementById('paso').value = paso;
                document.getElementById('descripcion').value = '';
                document.getElementById('montoMaximo').readOnly = true;
                document.getElementById('montoMinimo').readOnly = true;
                $("#Empleado").prop("disabled", false);
                $("#Destino").prop("disabled", true);
                document.getElementById('descripcion').readOnly = false;
                $("#addPasoFlow").remove();
                CreateTablaAprobadores(data);
                CreateTablaFlujoAprobadores(data);
            },
            error: function (d) {
                alert("ERROR INESPERADO.");
            }
        });

    }

}

function VerFlujo() {
    $("#addPasoFlow").remove();
    const tipoSolicitud = document.getElementById('solicitud');
    const tipoFlujo = document.getElementById('flujos');
    var idDocumento = tipoSolicitud.selectedIndex;
    const obj = document.getElementById('flujos');
    var idFlujo = tipoFlujo.value;
    var rango = obj.options[obj.selectedIndex].text;
    if (idDocumento <= 0 || idFlujo <= 0) {
        alert('Seleccione tipo de documento y rango del flujo');
        return false;
    }
    $.ajax({
        url: "/WorkFlow/GetAprobadores/",
        data: { idDocumento: idDocumento, idFlujo: idFlujo , rango: rango },
        dataType: 'json',
        type: 'GET',
        cache: false,
        success: function (data) {
            CreateTablaAprobadores(data);
        },
        error: function (d) {
            alert("ERROR INESPERADO.");
        }
    });
}

function CreateTablaAprobadores(infoAprobadores) {
    $("#tblAprobadores thead tr").remove();
    $("#tblAprobadores tbody tr").remove();
    if (infoAprobadores !== null) {
        let title = `<tr>
                        <th> Orden de Aprobacion </th>
                        <th> Descripcion </th>
                        <th> Nombre Aprobador </th>
                        <th> E-Mail Aprobador</th>
                        <th> Actualizar </th>
                        <th> Eliminar </th>
                                      </tr>`;
        $("#tblAprobadores thead").append(title); //Se agrega a la nueva tabla

        $("#tblAprobadores tbody tr").remove();
        $.each(infoAprobadores, function (index, item)
        {
            console.log(item);
            let tr = `  <tr>
                            <td> ${item.orden}</td>
                            <td> ${item.descripcion}</td>
                            <td> ${item.nombreAprobador}r</td>
                            <td> ${item.emailAprobador}</td>
                            <td> <input type="button" class="btn btn-primary" onclick="GetDataAprobador('${item.id}','${item.orden}','${item.descripcion}','${item.nombreAprobador}','${item.emailAprobador}','${item.cedulaAprobador}','${item.tipoSolicitud}','${item.idTipoSolicitud}','${item.montoMinimo}','${item.montoMaximo}','${item.destinoId}', '${item.flujoSolicitudId}','actualizar');" value="Actualizar"> </td>
                            <td> <input type="button" class="btn btn-primary" onclick="GetDataAprobador2('${item.id}','${item.orden}','${item.tipoSolicitud}','${item.idTipoSolicitud}','${item.montoMinimo}','${item.montoMaximo}','${item.destinoId}','${item.flujoSolicitudId}','eliminar');" value="Eliminar"> </td>
                       
                                    </tr>`;
            $("#tblAprobadores tbody").append(tr);
        });
        if (infoAprobadores !== null) {
            let btn = '<button type="button" class="btn btn-primary" style="float:right;" onclick="SetAgregarPasoFlujo();" id="addPasoFlow" name="addPasoFlow">Agregar Paso de Aprobacion</button>';
            $("#btnAddPaso").append(btn);
        }
    }
    else {
        $("#tblAprobadores thead").append('<tr><th><h2 class="noFound">No existe flujo de aprobacion creado</h2></th></tr>');
    }
}

function CreateTablaFlujoAprobadores(infoAprobadores) {
    $("#tblFlujo thead tr").remove();
    $("#tblFlujo tbody tr").remove();
    if (infoAprobadores !== null) {
        let title = `<tr>
                        <th> Orden de Aprobacion </th>
                        <th> Descripcion </th>
                        <th> Nombre Aprobador </th>
                        <th> E-Mail Aprobador</th>
                      
                                      </tr>`;
        $("#tblFlujo thead").append(title); //Se agrega a la nueva tabla

        $("#tblFlujo tbody tr").remove();
        $.each(infoAprobadores, function (index, item) {
            console.log(item);
            let tr = `  <tr>
                            <td> ${item.orden}</td>
                            <td> ${item.descripcion}</td>
                            <td> ${item.nombreAprobador}r</td>
                            <td> ${item.emailAprobador}</td>
                       
                                    </tr>`;
            $("#tblFlujo tbody").append(tr);
        });
    }
    else {
        $("#tblFlujo thead").append('<tr><th><h2 class="noFound">No existe flujo de aprobacion creado</h2></th></tr>');
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
    $('#paso').val('1');
    $('#destinoId').val('');
    $('#descripcion').val('');
    $('#tipoDocumento').val('');
    $('#addAprobador').val('');
    $('#addMail').val('');
    $("#tblFlujo thead tr").remove();
    $("#tblFlujo tbody tr").remove();
    CloseNewFlujo();
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


function GetDataAprobador(id, orden, descripcion, nombre, email, cedula, tipoDocumento, idTipoDocumento, montoMinimo, montoMaximo, destinoId, flujoSolicitudId, proceso) {
    EmpleadoAprobador();
    document.getElementById("msjPreguntaModal").innerHTML = 'Desea actualizar los siguientes datos?  SI para continuar , No para cancelar';
    $("#modificar").val('Actualizar');
    $('#descripcionT').val(descripcion);
    $('#nombre').val(nombre);
    $('#email').val(email);
    $('#cedula').val(cedula);
    $('#idPasoFlujoSolicitud').val(id);
    $('#tipoDocumentoe').val(tipoDocumento);
    $('#idTipoDocumente').val(idTipoDocumento);
    $('#pasoe').val(orden);
    OpenQuestionModal(proceso);
}

function Actualizar()
{
    var descripcion = $('#descripcionT').val();
    var cedula = $('#cedula').val();
    var nombre = $('#nombre').val();
    var email = $('#email').val();
    var idPasoFlujoSolicitud = $('#idPasoFlujoSolicitud').val();
    var idDocumento = $('#idTipoDocumente').val();
    var idFlujo = $('#rangeId').val();
    var rango = $('#range').val();

    if (descripcion == '' || cedula == '' || nombre == '' || email == '' || idPasoFlujoSolicitud == '' || idDocumento == '' || idFlujo == '' || rango == '') {
        alert('Todos los campos son requeridos.');
        return false;
    }
    
    $.ajax({
        url: "/WorkFlow/UpdatePasoFlujo",
        data: {descripcion: descripcion,  cedula: cedula , nombre: nombre, email: email, idPasoFlujoSolicitud: idPasoFlujoSolicitud , idDocumento: idDocumento , idFlujo: idFlujo , rango: rango},
        dataType: 'json',
        type: 'POST',
        cache: false,
        success: function (data) {
            $("#addPasoFlow").remove();
            CreateTablaAprobadores(data);    
        },
        error: function (d) {
            alert("ERROR INESPERADO.");
        }
    });
    CloseDataModal();
}

function GetDataAprobador2(id, orden, tipoDocumento, idTipoDocumento, montoMinimo, montoMaximo, destinoId, flujoSolicitudId, proceso)
{

    document.getElementById("msjDataEliminar").innerHTML = 'Desea eliminar los siguientes datos?  SI para continuar , No para cancelar';
    $('#ide').val(id);
    $('#orden').val(orden);
    $('#typee').val(tipoDocumento);
    $('#idTypee').val(idTipoDocumento);
    OpenQuestionModal(proceso);
}


function Eliminar() {
    var idPasoFlujoSolicitud = $('#ide').val();
    var tipoDocumento = $('#typee').val();
    var idFlujo = document.getElementById('flujos').value;
    var rango = $('#desRange').val();
    var idDocumento = $('#idTypee').val();

    if (tipoDocumento == '' || idPasoFlujoSolicitud == '' || idDocumento == '' || idFlujo == '' || rango == '') {
        alert('Todos los campos son requeridos.');
        return false;
    }

    $.ajax({
        url: "/WorkFlow/ExisteDocumentosAsociados",
        data: { idPasoFlujoSolicitud: idPasoFlujoSolicitud, tipoDocumento: tipoDocumento },
        dataType: 'json',
        type: 'GET',
        cache: false,
        success: function (data) {
            console.log(data.id);
            if (data.id > 0) {
                alert('Existen documentos asociados a este flujo no se puede eliminar');
                CloseQuestionModal();
                return false;
            }
            else {
                $.ajax({
                    url: "/WorkFlow/EliminarPasoFlujo",
                    data: { idPasoFlujoSolicitud: idPasoFlujoSolicitud, tipoDocumento: tipoDocumento , idDocumento: idDocumento, idFlujo: idFlujo, rango: rango },
                    dataType: 'json',
                    type: 'POST',
                    cache: false,
                    success: function (data) {
                        if (data.length > 0) {
                            $("#addPasoFlow").remove();
                            CreateTablaAprobadores(data);
                        } else {
                            $("#tblAprobadores thead tr").remove();
                            $("#tblAprobadores tbody tr").remove();
                            $("#addPasoFlow").remove();
                        }
                    },
                    error: function (d) {
                        alert("ERROR INESPERADO.");

                    }
                });
            }
        }
    });
    var paso = document.getElementById("tblAprobadores").rows.length; 
    if (paso == 2)
    {
        var indice = document.getElementById("solicitud").selectedIndex; 
        FlujosAprobacion(indice);
    }
    CloseQuestionModal();
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
    const documento = document.getElementById('solicitud');
    const descripcion = document.getElementById('flujos');
    var tipo = documento.options[documento.selectedIndex].text;
    var txtFlujo = descripcion.options[descripcion.selectedIndex].text;
    document.getElementById("msjDataUpdate").innerHTML = 'Actualizacion de datos para el flujo de aprobacion ' + txtFlujo + ' y tipo de documento ' + tipo;
    $('#range').val(txtFlujo);
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

function FlujosAprobacion2(indice) {

    var arrayFlujos = [];
    $.ajax({
        type: "GET",
        url: "/WorkFlow/GetFlujos",
        data: { id: indice },
        dataType: "json",
        beforeSend: function () {
           
        },
        success: function (data) {
            console.log(data);
            data.length > 0 ? $('#triggerModal').removeAttr('disabled') : $('#triggerModal').attr('disabled', 'disabled');
            $("#flujos").empty();
            $('#flujos').append('<option selected disabled value="-1">Seleccione...</option>');
            $.each(data, function (index, value) {
                arrayFlujos.push(value);
                $('#flujos').append('<option  value="' + value.id + '">' + value.descripcion + '</option>');
            });

            var f = document.getElementById('flujos');
            f.children[f.children.length - 1].setAttribute('selected', '');
            //$("#flujos").trigger('change');
        },
        complete: function () {
        
        }
    });
}

