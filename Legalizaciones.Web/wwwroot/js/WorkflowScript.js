$(document).ready(function () {

    $("#solicitud").select2({
        multiple: false,
        placeholder: 'Seleccione...'
    });

    $("#flujos").select2({
        multiple: false,
        placeholder: 'Seleccione...'
    });

    GetTiposDocumentos();
    jQuery('#nuevoFlujoModal').css("overflow-y", "scroll");
    //Aprobadores('#Empleado');
    //Aprobadores('#Empleado2');
    //Aprobadores('#suplente1');
    //Aprobadores('#suplente2');
    //Destinos();
    //$('#Moneda').hide();
    //$('#Currency').hide();

/* $("#Moneda").select2({
   multiple: false,
   placeholder: 'Seleccione...'
});*/
});


function GetTiposDocumentos() {

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
}

function GetFlujosAprobacion(indice) {

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

function GetFlujosAprobacion2(indice) {

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
            $("#flujos").trigger('change');
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
    });
}

function GetVerFlujo() {
    $("#addPasoFlow").remove();
    var tipoDocumentoId = $('#tipoDocumentoId').val();
    var flujoId = $('#flujoId').val();
    var flujoDescripcion = $('#flujoDescripcion').val();
    if (tipoDocumentoId <= 0 || flujoId <= 0) {
        alert('Seleccione tipo de documento y rango del flujo');
        return false;
    }
    $.ajax({
        url: "/WorkFlow/GetAprobadores/",
        data: { idDocumento: tipoDocumentoId, idFlujo: flujoId, rango: flujoDescripcion},
        dataType: 'json',
        type: 'GET',
        cache: false,
        success: function (data) {
            CreateTablaAprobadores(data);
        },
        error: function (d) {
            alert("ERROR INESPERADO.OBTENIENDO FLUJO DE APROBACION");
        }
    });

    $('#paso').val(1);
}

$('#solicitud').on('change', function (e) {
    $("#tblAprobadores thead tr").remove();
    $("#tblAprobadores tbody tr").remove();
    $('#addPasoFlow').remove();
    $('#flujoId').val(0);
    $('#flujoDescripcion').val('');
    var tipoDocumentoId = $('#solicitud').val();
    var tipoDocumento = $("#solicitud :selected").text();
    $('#tipoDocumentoId').val(tipoDocumentoId);
    $('#tipoDocumento').val(tipoDocumento);
    GetFlujosAprobacion(tipoDocumentoId);
}); 

$('#flujos').on('change', function (e) {
    var flujoId = $('#flujos').val();
    var flujoDescripcion = $("#flujos :selected").text();
    $('#flujoId').val(flujoId);
    $('#flujoDescripcion').val(flujoDescripcion);
}); 

$('#Destino').on('change', function (e) {
    var destinoId = $('#Destino').val();
    var destino = $("#Destino :selected").text();
    $('#destinoId').val(destinoId);
    $('#destino').val(destino);
}); 

$('#Empleado').on('change', function (e) {
    var email = $("#Empleado option:selected").data('email');
    console.log(email);
    $('#emailAprobador').val(email);
    var nombre = this.options[this.selectedIndex].text;
    $('#aprobador').val(nombre);
    var cedula = $("#Empleado option:selected").val();
    $('#cedulaAprobador').val(cedula);
}); 

$('#Empleado2').on('change', function (e) {
    var email = $("#Empleado2 option:selected").data('email');
    $('#emailAprobador').val(email);
    var nombre = this.options[this.selectedIndex].text;
    $('#aprobador').val(nombre);
    $('#nombre').val(nombre);
    var cedula = $("#Empleado2 option:selected").val();
    $('#cedulaAprobador').val(cedula);
}); 

$('#nuevoSuplente1').on('change', function (e) {
    var email = $("#nuevoSuplente1 option:selected").data('email');
    $('#emailSuplente1').val(email);
    var nombre = this.options[this.selectedIndex].text;
    $('#aprobadorSuplente1').val(nombre);
    var cedula = $("#nuevoSuplente1 option:selected").val();
    $('#cedulaSuplente1').val(cedula);
}); 

$('#nuevoSuplente2').on('change', function (e) {
    var email = $("#nuevoSuplente2 option:selected").data('email');
    $('#emailSuplente2').val(email);
    var nombre = this.options[this.selectedIndex].text;
    $('#aprobadorSuplente2').val(nombre);
    var cedula = $("#nuevoSuplente2 option:selected").val();
    $('#cedulaSuplente2').val(cedula);
}); 

$('#suplente11').on('change', function (e) {
    var email = $("#suplente11 option:selected").data('email');
    $('#emailSuplente1').val(email);
    var nombre = this.options[this.selectedIndex].text;
    $('#aprobadorSuplente1').val(nombre);
    var cedula = $("#suplente11 option:selected").val();
    $('#cedulaSuplente1').val(cedula);
    $('#suplent1').val(nombre);
});

$('#suplente22').on('change', function (e) {
    var email = $("#suplente22 option:selected").data('email');
    $('#emailSuplente2').val(email);
    var nombre = this.options[this.selectedIndex].text;
    $('#aprobadorSuplente2').val(nombre);
    var cedula = $("#suplente22 option:selected").val();
    $('#cedulaSuplente2').val(cedula);
    $('#suplent2').val(nombre);
}); 


function SetNuevoFlujo() {
    $('#paso').val(1);
    var flag = $('#solicitud').val();
    console.log(flag);
    if (flag == null) {
        alert('Seleccione tipo de documento');
        return false;
    }
    $("#tblFlujo thead tr").remove();
    $("#tblFlujo tbody tr").remove();
    $('#addPasoFlow').remove();
    Aprobadores('#Empleado');
    Aprobadores('#nuevoSuplente1');
    Aprobadores('#nuevoSuplente2');
    Destinos();
    $("#Destino").prop("disabled", false);
    var paso = $('#paso').val();
    if (paso == 1) {
        $("#Empleado").prop("disabled", true);
        document.getElementById('descripcion').readOnly = true;
    }
    else {
        $("#Empleado").prop("disabled", false);
        document.getElementById('descripcion').readOnly = false;
    }
    document.getElementById('montoMaximo').readOnly = false;
    document.getElementById('montoMinimo').readOnly = false;
    document.getElementById('descripcion').value = '';
    document.getElementById('montoMinimo').value = '';
    document.getElementById('montoMaximo').value = '';
    document.getElementById('montoMinimo').placeholder = '0,00';
    document.getElementById('montoMaximo').placeholder = '0,00';
    var tipoDocumento = $('#tipoDocumento').val();
    document.getElementById("msjNuevoFlujo").innerHTML = 'Nuevo flujo de aprobacion para ' + tipoDocumento;
    $("#nuevoFlujoModal").modal('show'); 
}

function EnviarDataAprobador() {
    var paso = $('#paso').val();
    var tipoDocumento = document.getElementById('tipoDocumento').value;
    var idDocumento = document.getElementById('tipoDocumentoId').value;
    var montoMinimo = document.getElementById('montoMinimo').value;
    var montoMaximo = document.getElementById('montoMaximo').value;
    var destino = document.getElementById('destino').value;
    var destinoId = document.getElementById('destinoId').value;
    var aprobadorSuplente1 = document.getElementById('aprobadorSuplente1').value;
    var cedulaSuplente1 = document.getElementById('cedulaSuplente1').value;
    var emailSuplente1 = document.getElementById('emailSuplente1').value;
    var aprobadorSuplente2 = document.getElementById('aprobadorSuplente2').value;
    var cedulaSuplente2 = document.getElementById('cedulaSuplente2').value;
    var emailSuplente2 = document.getElementById('emailSuplente2').value;
    if (paso > 1) {
        var aprobador = document.getElementById('aprobador').value;
        var cedulaAprobador = document.getElementById('cedulaAprobador').value;
        var emailAprobador = document.getElementById('emailAprobador').value;
        var descripcion = document.getElementById('descripcion').value;
        if (aprobador == '' || descripcion == '') {
            alert('Complete todos los campos requeridos.');
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
                    alert('Ya existe un flujo de aprobacion destino ' + destino + ' con montos entre ' + montoMinimo + ' y ' + montoMaximo + ', debe elegir montos diferentes.');
                    return false;
                }
                else {

                    $.ajax({
                        url: "/WorkFlow/CreateFlujoDocumento",
                        data: {
                            paso: paso, tipoDocumento: tipoDocumento, idDocumento: idDocumento, aprobador: aprobador, empleado: cedulaAprobador,
                            descripcion: descripcion, mail: emailAprobador, destino: destino, destinoId: destinoId, montoMaximo: montoMaximo, montoMinimo: montoMinimo,
                            aprobadorSuplente1: aprobadorSuplente1, cedulaSuplente1: cedulaSuplente1, emailSuplente1: emailSuplente1, aprobadorSuplente2: aprobadorSuplente2, cedulaSuplente2: cedulaSuplente2, emailSuplente2: emailSuplente2
                        },
                        dataType: 'json',
                        type: 'POST',
                        cache: false,
                        success: function (data) {
                            paso = parseInt(paso) + 1;
                            Aprobadores('#Empleado');
                            Aprobadores('#nuevoSuplente1');
                            Aprobadores('#nuevoSuplente2');
                            document.getElementById('paso').value = paso;
                            document.getElementById('descripcion').value = '';
                            document.getElementById('montoMaximo').readOnly = true;
                            document.getElementById('montoMinimo').readOnly = true;
                            $("#Empleado").prop("disabled", false);
                            $("#Destino").prop("disabled", true);
                            document.getElementById('descripcion').readOnly = false;
                            $("#addPasoFlow").remove();
                            var indice = document.getElementById("solicitud").selectedIndex;
                            GetFlujosAprobacion2(indice);

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
        if (aprobador == '' || descripcion == '') {
            alert('Debe especificar los campos aprobador y descripcion');
            return false;
        }
        $.ajax({
            url: "/WorkFlow/CreateFlujoDocumento",
            data: {
                paso: paso, tipoDocumento: tipoDocumento, idDocumento: idDocumento, aprobador: aprobador, empleado: cedulaAprobador, descripcion: descripcion,
                mail: emailAprobador, destino: destino, destinoId: destinoId, montoMaximo: montoMaximo, montoMinimo: montoMinimo,
                aprobadorSuplente1: aprobadorSuplente1, cedulaSuplente1: cedulaSuplente1, emailSuplente1: emailSuplente1, aprobadorSuplente2: aprobadorSuplente2, cedulaSuplente2: cedulaSuplente2, emailSuplente2: emailSuplente2
            },
            dataType: 'json',
            type: 'POST',
            cache: false,
            success: function (data) {
                paso = parseInt(paso) + 1;
                Aprobadores('#Empleado');
                Aprobadores('#nuevoSuplente1');
                Aprobadores('#nuevoSuplente2');
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
    var numPaso = document.getElementById("tblAprobadores").rows.length;
    $('#paso').val(numPaso);
    LimpiarAprobadores();
}


function SetAgregarPasoFlujo() {
    Aprobadores('#Empleado');
    Aprobadores('#nuevoSuplente1');
    Aprobadores('#nuevoSuplente2');
    Destinos();
    $("#Empleado").prop("disabled", false);  
    var txtFlujo = $('#flujoDescripcion').val();
    var [min, max, des] = txtFlujo.split("-");
    min = min.trim();
    max = max.trim();
    des = des.trim();
    document.getElementById('montoMinimo').value = parseFloat(min).toFixed(2);
    document.getElementById('montoMaximo').value = parseFloat(max).toFixed(2);
    document.getElementById('descripcion').readOnly = false;
    var indiceDestino = 0;
    if (des == 'Nacional') { indiceDestino = 1; }
    else if (des == 'Internacional') { indiceDestino = 2; }
    document.getElementById("Destino").selectedIndex = indiceDestino;
    $("#Destino").val(indiceDestino);
    $("#Destino option[value=" + des + "]").attr("selected", true);
    $("#Destino").prop("disabled", true);
    document.getElementById("msjNuevoFlujo").innerHTML = 'Agregar paso para el flujo de aprobacion ' + $('#tipoDocumento').val()+ ' - ' + des;
    var paso = document.getElementById("tblAprobadores").rows.length;
    $('#paso').val(paso);
    $('#destino').val(des);
    $('#destinoId').val(indiceDestino);
    $("#nuevoFlujoModal").modal('show');
}


function GetDataAprobador(id, orden, descripcion, nombre, email, cedula, tipoDocumento, idTipoDocumento, montoMinimo, montoMaximo, destinoId, flujoSolicitudId,
                                               nombreSuplenteUno, nombreSuplenteDos, cedulaSuplenteUno, cedulaSuplenteDos, emailSuplenteUno, emailSuplenteDos,proceso) {
    Aprobadores('#Empleado2');
    Aprobadores('#suplente11');
    Aprobadores('#suplente22');
    document.getElementById("msjPreguntaModal").innerHTML = 'Desea actualizar los siguientes datos?  SI para continuar , No para cancelar';
    $("#modificar").val('Actualizar');

    $('#idx').val(id);
    $('#msjDataUpdate').val('Actualizacion de flujoaprobacion');
    $('#paso').val(orden);
    $('#nombre').val(nombre);
    $('#descripcionT').val(descripcion);
    $('#aprobador').val(nombre);
    $('#suplent1').val(nombreSuplenteUno);
    $('#suplent2').val(nombreSuplenteDos);
    $('#aprobadorSuplente1').val(nombreSuplenteUno);
    $('#aprobadorSuplente2').val(nombreSuplenteDos);
    $('#cedulaAprobador').val(cedula);
    $('#cedulaSuplente1').val(cedulaSuplenteUno);
    $('#cedulaSuplente2').val(cedulaSuplenteDos);
    $('#emailAprobador').val(email);
    $('#emailSuplente1').val(emailSuplenteUno);
    $('#emailSuplente2').val(emailSuplenteDos);
    OpenQuestionModal(proceso);
}

function Actualizar() {
    var id = $('#idx').val();
    var descripcion = $('#descripcionT').val();
    var cedula = $('#cedulaAprobador').val();
    var nombre = $('#aprobador').val();
    var email = $('#emailAprobador').val();
    var idPasoFlujoSolicitud = $('#idx').val();
    var tipoDocumento = document.getElementById('tipoDocumento').value;
    var idDocumento = $('#tipoDocumentoId').val();
    var idFlujo = $('#flujoId').val();
    var rango = $('#flujoDescripcion').val();
    var montoMinimo = document.getElementById('montoMinimo').value;
    var montoMaximo = document.getElementById('montoMaximo').value;
    var destino = document.getElementById('destino').value;
    var destinoId = document.getElementById('destinoId').value;
    var aprobadorSuplente1 = document.getElementById('aprobadorSuplente1').value;
    var cedulaSuplente1 = document.getElementById('cedulaSuplente1').value;
    var emailSuplente1 = document.getElementById('emailSuplente1').value;
    var aprobadorSuplente2 = document.getElementById('aprobadorSuplente2').value;
    var cedulaSuplente2 = document.getElementById('cedulaSuplente2').value;
    var emailSuplente2 = document.getElementById('emailSuplente2').value;

    if (idFlujo == '') { idFlujo = document.getElementById('flujos').value; }

    if (descripcion == '' || cedula == '' || nombre == '' || email == '' || idPasoFlujoSolicitud == '' || idDocumento == '' || idFlujo == '' || rango == '') {
        alert('Todos los campos son requeridos.');
        return false;
    }

    $.ajax({
        url: "/WorkFlow/UpdatePasoFlujo",
        data: {
            descripcion: descripcion, cedula: cedula, nombre: nombre, email: email, idPasoFlujoSolicitud: idPasoFlujoSolicitud, idDocumento: idDocumento, idFlujo: idFlujo, rango: rango,
            aprobadorSuplente1: aprobadorSuplente1, cedulaSuplente1: cedulaSuplente1, emailSuplente1: emailSuplente1, aprobadorSuplente2: aprobadorSuplente2, cedulaSuplente2: cedulaSuplente2, emailSuplente2: emailSuplente2
        },
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

function Aprobadores(objeto) {

    $(objeto).select2({
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
            $(objeto).empty();
            $(objeto).append('<option selected disabled value="-1">Seleccione...</option>');
            $.each(data, function (index, value) {
                arrayEmpleadoPermiso.push(value);
                $(objeto).append('<option data-email="' + value.correo + '" value="' + value.cedula + '">' + value.cargo + '</option>');
            });
        },
        complete: function () {

        }
    });
}


function GetDataAprobador2(id, orden, tipoDocumento, idTipoDocumento, montoMinimo, montoMaximo, destinoId, flujoSolicitudId, proceso) {

    document.getElementById("msjDataEliminar").innerHTML = 'Desea eliminar los siguientes datos?  SI para continuar , No para cancelar';
    $('#idx').val(id);
    $('#paso').val(orden);
    //$('#tipoDocumento').val(tipoDocumento);
    //$('#tipoDocumentoId').val(idTipoDocumento);
    OpenQuestionModal(proceso);
}


function Eliminar() {
    var idPasoFlujoSolicitud = $('#idx').val();
    var tipoDocumento = $('#tipoDocumento').val();
    var idFlujo = document.getElementById('flujoId').value;
    var rango = $('#flujoDescripcion').val();
    var idDocumento = $('#tipoDocumentoId').val();


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
                    data: { idPasoFlujoSolicitud: idPasoFlujoSolicitud, tipoDocumento: tipoDocumento, idDocumento: idDocumento, idFlujo: idFlujo, rango: rango },
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
        },
        complete: function () {
            var paso = document.getElementById("tblAprobadores").rows.length;
            if (paso == 2) {
                var indice = document.getElementById("solicitud").selectedIndex;
                setTimeout(FlujosAprobacion(indice), 2000);
            }
        }
    });
    var numPaso = document.getElementById("tblAprobadores").rows.length;
    $('#paso').val(numPaso);
    CloseQuestionModal();
}


function CreateTablaAprobadores(infoAprobadores) {
    $("#tblAprobadores thead tr").remove();
    $("#tblAprobadores tbody tr").remove();
    $('#addPasoFlow').remove();
    if (infoAprobadores !== null) {
        let title = `<tr>
                        <th> Orden de Aprobacion </th>
                        <th> Descripcion </th>
                        <th> Cargo del Aprobador </th>
                        <th> E-Mail Aprobador </th>
                        <th> Cargo Suplente 1 </th>
                        <th> Cargo Suplente 2 </th>
                        <th> Actualizar </th>
                        <th> Eliminar </th>
                                      </tr>`;
        $("#tblAprobadores thead").append(title); //Se agrega a la nueva tabla

        $("#tblAprobadores tbody tr").remove();
        $.each(infoAprobadores, function (index, item) {
            console.log(item);
            let tr = `  <tr>
                            <td> ${item.orden}</td>
                            <td> ${item.descripcion}</td>
                            <td> ${item.nombreAprobador}</td>
                            <td> ${item.emailAprobador}</td>
                            <td> ${item.nombreSuplenteUno}</td>
                            <td> ${item.nombreSuplenteDos}</td>
                            <td> <input type="button" class="btn btn-primary" onclick="GetDataAprobador('${item.id}','${item.orden}','${item.descripcion}','${item.nombreAprobador}','${item.emailAprobador}','${item.cedulaAprobador}',
                                                                                          '${item.tipoSolicitud}','${item.idTipoSolicitud}','${item.montoMinimo}','${item.montoMaximo}','${item.destinoId}', '${item.flujoSolicitudId}',
                                                                                   '${item.nombreSuplenteUno}','${item.nombreSuplenteDos}','${item.cedulaSuplenteUno}', '${item.cedulaSuplenteDos}', '${item.emailSuplenteUno}', '${item.emailSuplenteDos}',
                                                                                  'actualizar');" value="Actualizar"> </td>

                            <td> <input type="button" class="btn btn-primary" onclick="GetDataAprobador2('${item.id}','${item.orden}','${item.tipoSolicitud}','${item.idTipoSolicitud}','${item.montoMinimo}',
                                                                                                      '${item.montoMaximo}','${item.destinoId}','${item.flujoSolicitudId}','eliminar');" value="Eliminar"> </td>
                       
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
                        <th> Cargo del Aprobador </th>
                        <th> E-Mail Aprobador</th>            
                                      </tr>`;
        $("#tblFlujo thead").append(title); //Se agrega a la nueva tabla

        $("#tblFlujo tbody tr").remove();
        $.each(infoAprobadores, function (index, item) {
            console.log(item);
            let tr = `  <tr>
                            <td> ${item.orden}</td>
                            <td> ${item.descripcion}</td>
                            <td> ${item.nombreAprobador}</td>
                            <td> ${item.emailAprobador}</td>
                                    </tr>`;
            $("#tblFlujo tbody").append(tr);
        });
    }
    else {
        $("#tblFlujo thead").append('<tr><th><h2 class="noFound">No existe flujo de aprobacion creado</h2></th></tr>');
    }
}

function OpenNewFlujo() {
    const obj = document.getElementById('solicitud');
    var tipo = obj.options[obj.selectedIndex].text;
    document.getElementById("msjNuevoFlujo").innerHTML = 'Flujo de aprobacion ' + tipo;
    $('#nuevoFlujoModal').modal('show');
}

function OpenDataModal() {
    $("#preguntaModal").modal('hide');
    $('#dataModal').modal('show');
}

function CloseNewFlujo() {
    //ClearAll();
    $('#nuevoFlujoModal').modal('hide');
}

function CloseDataModal() {
    LimpiarAprobadores();
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

function OpenQuestionModal(proceso) {
    if (proceso === 'actualizar')
        $('#preguntaModal').modal('show');
    else if (proceso === 'eliminar')
        $('#dataModal2').modal('show');
}

function CloseQuestionModal(proceso) {

    LimpiarAprobadores();
    $("#dataModal2").modal('hide');
    $("#preguntaModal").modal('hide');
}

function ClearAll() {
    $("#tblFlujo thead tr").remove();
    $("#tblFlujo tbody tr").remove();
    LimpiarOcultos();
    CloseNewFlujo();
}


function LimpiarOcultos() {
    $('#destino').val('');
    $('#destinoId').val('');
    $('#paso').val('');
    $('#aprobador').val('');
    $('#cedulaAprobador').val('');
    $('#emailAprobador').val('');
    $('#aprobadorSuplente1').val('');
    $('#cedulaSuplente1').val('');
    $('#emailSuplente1').val('');
    $('#aprobadorSuplente2').val('');
    $('#cedulaSuplente2').val('');
    $('#emailSuplente2').val('');
}

function LimpiarAprobadores() {
    $('#aprobador').val('');
    $('#cedulaAprobador').val('');
    $('#emailAprobador').val('');
    $('#aprobadorSuplente1').val('');
    $('#cedulaSuplente1').val('');
    $('#emailSuplente1').val('');
    $('#aprobadorSuplente2').val('');
    $('#cedulaSuplente2').val('');
    $('#emailSuplente2').val('');
    $('#idx').val('');
}