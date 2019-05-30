$(document).ready(function () {

    var date = new Date();
    var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());

    $("#btnSubmit").click(function () {

        var list = [];
        $('#Items tr').each(function (index, ele) {
            var solicitudItem = {
                ServicioId: $('.servicioId', this).text(),
                Servicio: $('.servicioName', this).text(),
                Price: parseInt($('.price', this).text()),
                Quantity: parseInt($('.quantity', this).text()),
                TotalPrice: parseFloat($('.amount', this).text()),
                Ciudad: ($('.ciudad', this).text()),
                Origen: ($('.origen', this).text()),
                Destino: ($('.destino', this).text()),
                Estatus: parseInt($('.estatus', this).text())
            };
            list.push(solicitudItem);
        });
        console.log(list);
        $.ajax({
            type: 'POST',
            url: '/Solicitud/Crear',
            datatype: "Json",
            data: {
                SolicitudCode: $("#SolicitudCode").val(),
                FechaDesde: ($("#FechaDesde").val()),
                FechaHasta: ($("#FechaHasta").val()),
                CreatedDate: $("#CreatedDate").val(),
                DocERP: ($("#DocERP").val()),
                FechaERP: ($("#FechaERP").val()),
                Status: $("#Status").val(),
                Exportada: ($("#Exportada").val()),
                Concepto: ($("#Concepto").val()),
                Destino: ($("#Destino").val()),
                Zona: ($("#Zona").val()),
                CentroOperacion: ($("#CentroOperacion").val()),
                EmpleadoId: parseInt($("#Empleado").val()),
                UnidadNegocio: ($("#UnidadNegocio").val()),
                CentroCostos: ($("#CentroCosto").val()),
                Moneda: ($("#Moneda").val()),
                TotalAmount: parseFloat($("#TotalAmount").val()),
                GivenAmount: parseFloat($("#GivenAmount").val()),
                ChangeAmount: parseFloat($("#ChangeAmount").val()),
                SolicitudItems: list
            },
            success: function () {
                //alert('Successfully saved');
            },
            error: function (error) {
                console.log(error);
            }
        });
    });

    //remove button click event
    $('#Items').on('click', '.btnDelete', function () {
        $(this).parents('tr').remove();

        //Suma de Montos de Gastos
        var sum = 0;
        $('td.monto').each(function () {
            sum += parseFloat(this.innerHTML);
        });
        $("#txMontoT").val(sum);
        $('#hdfMontoSolicitud').val($("#txMontoT").val());
    });

    //Propiedades del dropdown unidades de negocio
    $("#CentroCosto").select2({
        multiple: false
    });

    //Propiedades del dropdown de centros de operaciones
    $("#CentroOperacion").select2({
        multiple: false
    });

    //Propiedades del dropdown unidades de negocio
    $("#UnidadNegocio").select2({
        multiple: false
    });

    //Propiedades del dropdown monedas
    $("#Moneda").select2({
        multiple: false
    });

    //Propiedades del dropdown concepto
    $("#Ciudad").select2({
        multiple: false
    });

    $("#Servicio").change(function () {
        var idServicio = $('#Servicio  option:selected').val();
        var nombreServicio = $('#Servicio  option:selected').text();
        console.log(nombreServicio);

        if (nombreServicio === "Comida") {
            $('#divGastosFecha').addClass('display-none');
        }else {
            $('#divGastosFecha').removeClass('display-none');
        }

        if (nombreServicio !== "Transporte" && nombreServicio !== "Movilidad") {
            $('#divGastosDescripcion').removeClass('col-md-4');
            $('#divGastosDescripcion').addClass('col-md-12');
            $('#divZonas').addClass('display-none');
        } else {
            $('#Monto').val("");

            $('#AvisoMontoServicio').addClass('display-none');
            $('#MensajeAviso').text('');

            $('#divGastosDescripcion').removeClass('col-md-12');
            $('#divGastosDescripcion').addClass('col-md-4');
            $('#divZonas').removeClass('display-none');
        }

        consultarLimiteGasto();

        //LoadProductsData($("#Servicio option:selected").val());
    });


    //total calculation
    function calculateSum() {
        var sum = 0;
        // iterate through each td based on class and add the values
        $(".amount").each(function () {

            var value = $(this).text();
            // add only if the value is number
            if (!isNaN(value) && value.length !== 0) {
                sum += parseFloat(value);
            }
        });

        $('#TotalAmount').val(sum);
        var a = $('#TotalAmount').val();
        var b = $('#GivenAmount').val();
        $('#ChangeAmount').val(a - b);
    };

    $('.amount').each(function () {
        calculateSum();
    });

    // change amount
    $('#GivenAmount').keyup(function () {
        var a = $('#TotalAmount').val();
        var b = $('#GivenAmount').val();
        $('#ChangeAmount').val(a - b);
    });

    $('.datepicker').datepicker({
        format: 'dd/mm/yyyy',
        todayHighlight: true,
        autoclose: true,
        orientation: 'bottom auto'
    });

    $('.datepicker2').datepicker({
        format: 'dd/mm/yyyy',
        todayHighlight: true,
        autoclose: true,
        orientation: 'bottom auto'
    });

    $(".datepicker").datepicker("update", new Date());

    $("#btnModalGastos").click(function () {
        console.log('prueba de click');
        $.ajax({
            url: '/Solicitud/AsignarGastos'
        }).done(function (msg) {
            $("#contentModal").html(msg);
            $("#modalGastos").modal();
        });
    });

    $('#gastosModal').on("hidden.bs.modal", function () {
        limpiarFormGastos();
        $('#mensajeValidacionGastos').hide("slow");
    });


    function limpiarFormGastos() {
        $('#FechaGasto').val('');
        $("#Servicio").val('');
        $('#ZonaOrigen').val('');
        $('#ZonaDestino').val('');
        $('#Monto').val('');

        $('#AvisoMontoServicio').addClass('display-none');
        $('#MensajeAviso').text('');

        $('#divGastosDescripcion').removeClass('col-md-4');
        $('#divGastosDescripcion').addClass('col-md-12');
        $('#divZonas').addClass('display-none');
    }

    //************************************   I N I C I O  ************************************
    /* Validaciones para los cambios de Destino - Pais. Se refrescan los combos de
     * Estado - Pais al cambiar destino o al cambiar pais */
    //*****************************************************************************************


    //Obtener EMPLEADOS
    //$.ajax({
    //    type: "GET",
    //    url: "/UNOEE/Empleados",
    //    datatype: "Json",
    //    success: function (data) {
    //        $("#Empleado").empty();
    //        $('#Empleado').append('<option selected value="">Seleccione...</option>');
    //        $.each(data, function (index, value) {
    //            //$("#Destino").select2();
    //            $('#Empleado').append('<option value="' + value.cedula + '">' + value.nombre + '</option>');
    //        });
    //    }
    //});

    ////Propiedades del dropdown destinos
    //$("#Empleado").select2({
    //    multiple: false
    //});


    //*****************************************************************************************
    // Obtener Zonas para ZonaDestino del modal de Gastos para transporte
    // Fecha 29/4/19
    // Autior: Joan
    //*****************************************************************************************
    $.ajax({
        type: "GET",
        url: "/Localidad/ZonasDestinos",
        datatype: "Json",
        success: function (data) {
            $("#ZonaDestino").empty();
            $('#ZonaDestino').append('<option selected value="">Seleccione...</option>');
            $.each(data, function (index, value) {
                //$("#Destino").select2();
                $('#ZonaDestino').append('<option value="' + value.id + '">' + value.nombre + '</option>');
            });
        }
    });

    //*****************************************************************************************
    // Obtener Servicios para  el modal de Gastos 
    // Fecha 29/4/19
    // Autior: Joan
    //*****************************************************************************************

    $.ajax({
        type: "GET",
        url: "/UNOEE/Servicios",
        datatype: "Json",
        success: function (data) {
            $("#Servicio").empty();
            $('#Servicio').append('<option selected value="">Seleccione...</option>');
            $.each(data, function (index, value) {
                //$("#Destino").select2();
                $('#Servicio').append('<option value="' + value.id + '">' + value.nombre + '</option>');
            });
        }
    });

    //Propiedades del dropdown destinos
    $("#Destino").select2({
        multiple: false
    });

    //EVENTO CHANGE DE DESTINOS   
    $("#Destino").change(function () {
        // console.log('parametro ' + $("#Destino").val());
        var valor = $("#Destino option:selected").val();
        $.ajax({
            type: "GET",
            url: "/Localidad/ZonasDestinos",
            datatype: "Json",
            data: { destinoID: valor },

            success: function (data) {
                $("#Zona").empty();
                $('#Zona').append('<option selected value="">Seleccione...</option>');
                var sw = false;
                $.each(data, function (index, value) {
                    if (sw === false) {
                        $('#Zona').append('<option value="' + value.id + '">' + value.nombre + '</option>');
                        sw = true;
                    }
                    else
                        $('#Zona').append('<option value="' + value.id + '">' + value.nombre + '</option>');
                });

                $("#Zona").select2({
                    multiple: false
                });

                //busco la moneda
                $.ajax({
                    type: "GET",
                    url: "/Localidad/MonedasPorDestino",
                    datatype: "Json",
                    data: { wIdDestino: valor },
                    success: function (data) {
                        $("#Moneda").empty();
                        $('#Moneda').append('<option selected value="">Seleccione...</option>');
                        $.each(data, function (index, value) {
                            $('#Moneda').append('<option value="' + value.id + '">' + value.nombre + '</option>');
                        });
                    }
                });

            }
        });

    });


    //Obtener PAIS
    $.ajax({
        type: "GET",
        url: "/Localidad/Paises",
        datatype: "Json",
        success: function (data) {
            $("#Pais").empty();
            $('#Pais').append('<option selected value="">Seleccione...</option>');
            $.each(data, function (index, value) {
                //$("#Destino").select2();
                $('#Pais').append('<option value="' + value.id + '">' + value.nombre + '</option>');
            });
        }
    });

    //Propiedades del dropdown pais
    $("#Pais").select2({
        multiple: false
    });

    //EVENTO CHANGE DE PAIS   
    $("#Pais").change(function () {
        // console.log('parametro ' + $("#Destino").val());
        var valor = $("#Pais option:selected").val();
        $.ajax({
            type: "GET",
            url: "/Localidad/CiudadesPais",
            datatype: "Json",
            data: { paisID: valor },
            success: function (data) {
                $("#Ciudad").empty();
                $('#Ciudad').append('<option selected value="">Seleccione...</option>');
                $.each(data, function (index, value) {
                    $('#Ciudad').append('<option value="' + value.id + '">' + value.nombre + '</option>');
                });
            }
        });


        var dataZona = [];

        $.ajax({
            type: "GET",
            url: "/UNOEE/OrigenDestinosPais",
            dataType: "json",
            data: { paisID: valor },
            success: function (data) {
                if (data.length > 0) {
                    $.map(data, function (item) {
                        var data1 = { label: item.nombre, value: item.nombre, id: item.id };
                        dataZona.push(data1);
                        //return { label: item.nombre, value: item.id };
                    });
                }

                $("#ZonaOrigen").autocomplete({
                    source: dataZona,
                    minLength: 0,
                    focus: function (event, ui) {
                        console.log(ui.item.label);
                    },
                    select: function (e, ui) {
                        $('#hdfZonaOrigen').val(ui.item.id);
                        consultarLimiteGasto();
                    }
                });

                $("#ZonaDestino").autocomplete({
                    source: dataZona,
                    minLength: 0,
                    focus: function (event, ui) {
                        console.log(ui.item.label);
                    },
                    select: function (e, ui) {
                        $('#hdfZonaDestino').val(ui.item.id);
                        consultarLimiteGasto();
                    }
                });
            }
        });

        consultarLimiteGasto();

    });

    //Propiedades del dropdown destinos
    $("#Destino").select2({
        multiple: false
    });

    //*****************************************   F I N ******************************************
    /* Validaciones para los cambios de Destino - Pais. Se refrescan los combos de
     * Estado - Pais al cambiar destino o al cambiar pais */
    //*****************************************************************************************


});

//get products details
function LoadProductsData(id) {
    $('#Price').empty();
    $('#Description').empty();
    $('#Quantity').val("");
    $('#Amount').val("");
    $.ajax({
        type: "GET",
        url: "/Home/GetServicioById",
        datatype: "Json",
        data: { servicioId: id },
        success: function (data) {
            $.each(data, function (index, value) {
                $("#Price").val(value.price);
                $("#Description").val(value.description);
            });

        }
    });
}

var rowIndex = 0;

function validarGastos() {

    //var zona = $("#Zona option:selected").text();
    var fechaGasto = $("#FechaGasto").val();
    var paisId = $("#Pais option:selected").val();
    var pais = $("#Pais option:selected").text();
    var servicioId = $("#Servicio option:selected").val();
    var servicio = $("#Servicio option:selected").text();
    var ciudadId = $("#Ciudad option:selected").val();
    var ciudad = $("#Ciudad option:selected").text();
    var origen = $("#ZonaOrigen").val();
    var destino = $("#ZonaDestino").val();
    var monto = $("#Monto").val();
    //$('#monto').val('');

    if (servicio !== "Movilidad" && servicio !== "Transporte") {

        if (servicio !== "" && monto !== "" && monto !== "0" && pais !== "Seleccione..." && ciudad !== "Seleccione..." && servicio !== "Seleccione...") {

            //monto = CalcularGastoComida();
            if (monto > 0) {

                rowIndex = rowIndex + 1;
                var row = `<tr class="rowIndex${rowIndex}">
                    <td class="fechaGasto">${fechaGasto}</td>
                    <td class="paisId display-none">${paisId}</td>
                    <td class="pais">${pais}</td>
                    <td class="servicioId display-none">${servicioId}</td>
                    <td class="servicio">${servicio}</td>
                    <td class="ciudadId display-none">${ciudadId}</td>
                    <td class="ciudad">${ciudad}</td>
                    <td class="origen">N/A</td>
                    <td class="destino">N/A</td>
                    <td class="monto">${monto}</td>
                    <td>
                        <a class="btn btn-danger btn-sm btnDelete">
                            <span class="glyphicon glyphicon-trash"></span>
                        </a>
                        <a class="btn btn-danger btn-sm btnEdit">
                            <span class="glyphicon glyphicon-edit" onClick = "ShowModalUpdate('rowIndex${rowIndex}');" ></span>
                        </a>
                    </td>
                </tr>`;

                alert("OK1");
                $('#Items').append(row);

                alert("OK2");

                //Suma de Montos de Gastos
                var sum = 0;
                $('td.monto').each(function () {
                    sum += parseFloat(this.innerHTML);
                });
                alert("OK3");

                var v = String(sum);
                v = v.replace('.', ',');
                $("#txMontoT").val(v);
                $('#hdfMontoSolicitud').val($("#txMontoT").val());

                alert("OK4");

            } else {
                    $("#mensajeGastos").text("El monto debe ser superior a 0.");
                    $('#mensajeValidacionGastos').show("slow");
                    return false
            }

        } else {
            $("#mensajeGastos").text("Faltan datos por especificar.");
            $('#mensajeValidacionGastos').show("slow");
            return false;
        }

    } else {
        if (fechaGasto !== "" && servicio !== "" && monto !== "" && origen !== "" && destino !== "") {
            rowIndex = rowIndex + 1;
            var row2 = `<tr class="rowIndex${rowIndex}">
                    <td class="fechaGasto">${fechaGasto}</td>
                    <td class="paisId display-none">${paisId}</td>
                    <td class="pais">${pais}</td>
                    <td class="servicioId display-none">${servicioId}</td>
                    <td class="servicio">${servicio}</td>
                    <td class="ciudadId display-none">${ciudadId}</td>
                    <td class="ciudad">${ciudad}</td>
                    <td class="origen">${origen}</td>
                    <td class="destino">${destino}</td>
                    <td class="monto">${monto}</td>
                    <td>
                        <a class="btn btn-danger btn-sm btnDelete">
                            <span class="glyphicon glyphicon-trash"></span>
                        </a>
                         <a class="btn btn-danger btn-sm btnEdit">
                            <span class="glyphicon glyphicon-edit" onClick = "ShowModalUpdate('rowIndex${rowIndex}');" ></span>
                        </a>
                    </td>
                </tr>`;

            $('#Items').append(row2);

            //Suma de Montos de Gastos
            var sum2 = 0;
            $('td.monto').each(function () {
                sum2 += parseFloat(this.innerHTML);
            });
            $("#txMontoT").val(sum2);
            $('#hdfMontoSolicitud').val($("#txMontoT").val());

        } else {
            $("#mensajeGastos").text("Faltan datos por especificar.");
            $('#mensajeValidacionGastos').show("slow");
            return false;
        }
    }


}

function actualizarGastos() {
    var value = $('#hdfRowIndex').val();

    var fechaGasto = $("#FechaGasto").val();
    var paisId = $("#Pais option:selected").val();
    var pais = $("#Pais option:selected").text();
    var servicioId = $("#Servicio option:selected").val();
    var servicio = $("#Servicio option:selected").text();
    var ciudadId = $("#Ciudad option:selected").val();
    var ciudad = $("#Ciudad option:selected").text();
    var origen = $("#ZonaOrigen").val();
    var destino = $("#ZonaDestino").val();
    var monto = $("#Monto").val();


    $('.' + value + ' .fechaGasto').text(fechaGasto);
    $('.' + value + ' .paisId').text(paisId);
    $('.' + value + ' .pais').text(pais);
    $('.' + value + ' .servicioId').text(servicioId);
    $('.' + value + ' .servicio').text(servicio);
    $('.' + value + ' .ciudadId').text(ciudadId);
    $('.' + value + ' .ciudad').text(ciudad);
    $('.' + value + ' .origen').text(origen);
    $('.' + value + ' .destino').text(destino);
    $('.' + value + ' .monto').text(monto);

    $('#gastosModal').modal('hide');

    var sum = 0;
    $('td.monto').each(function () {
        sum += parseFloat(this.innerHTML);
    });
    var v = String(sum);
    v = v.replace('.', ',');
    $("#txMontoT").val(v);
    $('#hdfMontoSolicitud').val($("#txMontoT").val());
}



function ShowModalUpdate(value) {

    var fechaGasto = $('.' + value + ' .fechaGasto').text();
    var paisId = $('.' + value + ' .paisId').text();
    var pais = $('.' + value + ' .pais').text();
    var servicioId = $('.' + value + ' .servicioId').text();
    var servicio = $('.' + value + ' .servicio').text();
    var ciudadId = $('.' + value + ' .ciudadId').text();
    var ciudad = $('.' + value + ' .ciudad').text();
    var origen = $('.' + value + ' .origen').text();
    var destino = $('.' + value + ' .destino').text();
    var monto = $('.' + value + ' .monto').text();

    console.log(fechaGasto + ', ' + paisId + ', ' + pais + ', ' + servicioId + ', ' + servicio + ', ' + ciudadId + ', ' + ciudad + ', ' + origen + ', ' + destino + ', ' + monto);

    $('#FechaGasto').val(fechaGasto);
    $("#Pais").val(paisId);
    $("#Servicio").val(servicioId);
    $("#Ciudad").val(ciudadId);
    $('#ZonaOrigen').val(origen);
    $('#ZonaDestino').val(destino);
    $('#Monto').val(monto);

    $('#btnAdd').addClass('display-none');
    $('#btnUpd').removeClass('display-none');
    $('#hdfRowIndex').val(value);

    if (servicio === "Movilidad" || servicio === "Transporte") {
        $('#divGastosDescripcion').removeClass('col-md-12');
        $('#divGastosDescripcion').addClass('col-md-4');
        $('#divZonas').removeClass('display-none');
    } else {
        $('#divGastosDescripcion').removeClass('col-md-4');
        $('#divGastosDescripcion').addClass('col-md-12');
        $('#divZonas').addClass('display-none');
    }

    $('#gastosModal').modal('show');
}

function validarViaje(boton) {
    $('#btnAdd').removeClass('display-none');
    $('#btnUpd').addClass('display-none');
    $('#btnUpd').addClass('display-none');

    var destino = $("#Destino option:selected").val();
    var zona = $("#Zona option:selected").val();
    var moneda = $("#Moneda option:selected").val();

    if (destino !== "" && zona !== "" && moneda !== "") {


            $('#mensajeValidacionViaje').hide("slow");
            $('#gastosModal').modal('show');

        //var wFechaDesde = $("#FechaDesde").val();
        //var wFechaHasta = $("#FechaHasta").val();

     

        //if (wFechaDesde > wFechaHasta) {

        //    $("#mensajeViaje").html("La fecha Hasta se encuentra incorrecta <strong>Favor Verifique las fechas</strong> para añadir gastos.");
        //    $('#mensajeValidacionViaje').show("slow");
        //    return false;

        //} else {

        //    $('#mensajeValidacionViaje').hide("slow");
        //    $('#gastosModal').modal('show');
        //    return true;

        //}

    } else {
        $("#mensajeViaje").html("Seleccione <strong>Destino, Zona Visitada y Moneda</strong> para añadir gastos.");
        $('#mensajeValidacionViaje').show("slow");
        return false;
    }
}


function consultarLimiteGasto() {
    var _monedaID = $("#Moneda option:selected").val();
    var _paisID = $("#Pais option:selected").val();
    var _tipoServicio = $("#Servicio option:selected").text();
    var _tipoServicioID = $("#Servicio option:selected").val();
    var _origenID = $("#hdfZonaOrigen").val();
    var _destinoID = $("#hdfZonaDestino").val();
    //console.log(monedaID + ', ' + paisID + ', ' + tipoServicioID + ', ' + origenID + ', ' + destinoID);

    if (_monedaID !== "" && _paisID !== "" && _tipoServicioID !== "") {
        if (_tipoServicio === "Transporte" || _tipoServicio === "Movilidad") {
            $.ajax({
                type: "GET",
                url: "/ConfiguracionGasto/obtenerLimiteGastoRuta",
                datatype: "Json",
                data: { paisID: _paisID, tipoServicioID: _tipoServicioID, monedaID: _monedaID, origenID: _origenID, destinoID: _destinoID },
                success: function (data) {
                    if (data !== null) {
                        if (data.monto > 0 && data.monto !== '') {
                            $('#AvisoMontoServicio').removeClass('display-none');
                            $('#MensajeAviso').text('Monto Limite: ' + data.monto);

                            $('#Monto').on('input', function () {
                                var value = $(this).val();
                                if ((value !== '') && (value.indexOf('.') === -1)) {
                                    $(this).val(Math.max(Math.min(value, data.monto), -data.monto));
                                }
                            });

                            $('#Monto').val(data.monto);
                        } else {
                            $('#AvisoMontoServicio').addClass('display-none');
                            $('#MensajeAviso').text('');
                            $('#Monto').val('');

                            $('#Monto').on('input', function () {
                                var value = $(this).val();
                                if ((value !== '') && (value.indexOf('.') === -1)) {
                                    $(this).val(Math.max(Math.min(value, 99999999999), -99999999999));
                                }
                            });
                        }
                    } else {
                        $('#AvisoMontoServicio').addClass('display-none');
                        $('#MensajeAviso').text('');
                        $('#Monto').val('');

                        $('#Monto').on('input', function () {
                            var value = $(this).val();
                            if ((value !== '') && (value.indexOf('.') === -1)) {
                                $(this).val(Math.max(Math.min(value, 99999999999), -99999999999));
                            }
                        });
                    }
                }
            });
        }
        else {
            $.ajax({
                type: "GET",
                url: "/ConfiguracionGasto/obtenerLimiteGasto",
                datatype: "Json",
                data: { paisID: _paisID, tipoServicioID: _tipoServicioID, monedaID: _monedaID },
                success: function (data) {
                    if (data !== null) {
                        if (data.monto > 0 && data.monto !== '') {
                            $('#AvisoMontoServicio').removeClass('display-none');
                            $('#MensajeAviso').text('Monto Limite: ' + data.monto);

                            $('#Monto').on('input', function () {
                                var value = $(this).val();
                                if ((value !== '') && (value.indexOf('.') === -1)) {
                                    $(this).val(Math.max(Math.min(value, data.monto), -data.monto));
                                }
                            });
                            $('#Monto').val(data.monto);
                        } else {
                            $('#AvisoMontoServicio').addClass('display-none');
                            $('#MensajeAviso').text('');

                            $('#Monto').on('input', function () {
                                var value = $(this).val();
                                if ((value !== '') && (value.indexOf('.') === -1)) {
                                    $(this).val(Math.max(Math.min(value, 99999999999), -99999999999));
                                }
                            });
                            $('#Monto').val('');
                        }
                    } else {
                        $('#AvisoMontoServicio').addClass('display-none');
                        $('#MensajeAviso').text('');

                        $('#Monto').on('input', function () {
                            var value = $(this).val();
                            if ((value !== '') && (value.indexOf('.') === -1)) {
                                $(this).val(Math.max(Math.min(value, 99999999999), -99999999999));
                            }
                        });
                        $('#Monto').val('');
                    }
                }
            });
        }
    }
}

//*****************************************************************************//

//Metodos creados por eliezer vargas para los combos en la pagina de editar    //

//*****************************************************************************//

window.onload = function () {
    if ($("#txProceso").val() !== "A") {
        CargarComboAlcrear();
    } else {
        CargarCombosAlEditar();
    }
}

function CargarCombosAlEditar() {
    //Obtener DESTINOS

    $.ajax({
        type: "GET",
        url: "/Localidad/DestinosEdit" + "?Id=" + $('#Id').val(),
        datatype: "Json",
        success: function (resultado) {
            $.each(resultado, function (i, value) {
                var wN = value.nombre;
                var wSel = wN.substr(wN.length - 2, 2);
                if (wSel === "XX") {
                    wN = wN.substr(0, wN.length - 2);
                    $('#Destino').append('<option selected value="' + value.id + '">' + wN + '</option>');
                } else {
                    $('#Destino').append('<option value="' + value.id + '">' + value.nombre + '</option>');
                }
            });

        }
    });

    //zonas por destino
    $.ajax({
        type: "GET",
        url: "/Localidad/ZonasDestinosEdit" + "?Id=" + $('#Id').val(),
        datatype: "Json",
        success: function (resultado) {
            $.each(resultado, function (i, value) {
                var wN = value.nombre;
                var wSel = wN.substr(wN.length - 2, 2);
                if (wSel === "XX") {
                    wN = wN.substr(0, wN.length - 2);
                    $('#Zona').append('<option selected value="' + value.id + '">' + wN + '</option>');
                } else {
                    $('#Zona').append('<option value="' + value.id + '">' + value.nombre + '</option>');
                }
            });

        }
    });

    //Obtener monedas
    $.ajax({
        type: "GET",
        url: "/Localidad/MonedasEdit" + "?Id=" + $('#Id').val(),
        datatype: "Json",
        success: function (resultado) {
            $.each(resultado, function (i, value) {
                var wN = value.nombre;
                var wSel = wN.substr(wN.length - 2, 2);
                if (wSel === "XX") {
                    wN = wN.substr(0, wN.length - 2);
                    $('#MonedaId').append('<option selected value="' + value.id + '">' + wN + '</option>');
                } else {
                    $('#MonedaId').append('<option value="' + value.id + '">' + value.nombre + '</option>');
                }
            });

        }
    });


    //Obtener centro de operaciones
    $.ajax({
        type: "GET",
        url: "/UNOEE/CentroOperaciones",
        datatype: "Json",
        success: function (data) {
            $('#CentroOperacion').empty();
            $.each(data, function (index, value) {
                $('#CentroOperacion').append('<option selected value="' + value.id + '">' + value.nombre + '</option>');
            });
        }
    });


    //Obtener unidades de negocio
    $.ajax({
        type: "GET",
        url: "/UNOEE/UnidadNegocios",
        datatype: "Json",
        success: function (data) {
            $('#UnidadNegocio').empty();
            $.each(data, function (index, value) {
                $('#UnidadNegocio').append('<option selected value="' + value.id + '">' + value.nombre + '</option>');
            });
        }
    });

    //Obtener centros de costo
    $.ajax({
        type: "GET",
        url: "/UNOEE/CentroCostos",
        datatype: "Json",
        success: function (data) {
            $('#CentroCosto').empty();
            $.each(data, function (index, value) {
                $('#CentroCosto').append('<option selected value="' + value.id + '">' + value.nombre + '</option>');
            });
        }
    });

    $("#CentroCosto").select2({
        multiple: false
    });

}

function CargarComboAlcrear() {

    //Obtener DESTINOS
    $.ajax({
        type: "GET",
        url: "/Localidad/Destinos",
        datatype: "Json",
        success: function (data) {
            $("#Destino").empty();
            $('#Destino').append('<option selected value="">Seleccione...</option>');
            $.each(data, function (index, value) {
                //$("#Destino").select2();
                $('#Destino').append('<option value="' + value.id + '">' + value.nombre + '</option>');
            });
        }
    });

    //Obtener MONEDAS
    $.ajax({
        type: "GET",
        url: "/Localidad/Monedas",
        datatype: "Json",
        success: function (data) {
            $.each(data, function (index, value) {
                $('#Moneda').append('<option value="' + value.id + '">' + value.nombre + '</option>');
            });
        }
    });

    //Obtener zonas
    $.ajax({
        type: "GET",
        url: "/Home/Zonas",
        datatype: "Json",
        success: function (data) {
            $.each(data, function (index, value) {
                $("#Zona").select2();
                $('#Zona').append('<option value="' + value.nombre + '">' + value.nombre + '</option>');
            });
        }
    });

    //Obtener unidades de negocio
    $.ajax({
        type: "GET",
        url: "/UNOEE/UnidadNegocios",
        datatype: "Json",
        success: function (data) {
            $('#UnidadNegocio').empty();
            $('#UnidadNegocio').append('<option selected value="">Seleccione...</option>');
            $.each(data, function (index, value) {
                $("#UnidadNegocio").select2();
                $('#UnidadNegocio').append('<option value="' + value.id + '">' + value.nombre + '</option>');
            });
        }
    });

    //Obtener centro de operaciones
    $.ajax({
        type: "GET",
        url: "/UNOEE/CentroOperaciones",
        datatype: "Json",
        success: function (data) {
            $('#CentroOperacion').empty();
            $('#CentroOperacion').append('<option selected value="">Seleccione...</option>');
            $.each(data, function (index, value) {
                $("#CentroOperacion").select2();
                $('#CentroOperacion').append('<option value="' + value.id + '">' + value.nombre + '</option>');
            });
        }
    });

    //Obtener centros de costo
    $.ajax({
        type: "GET",
        url: "/UNOEE/CentroCostos",
        datatype: "Json",
        success: function (data) {
            $('#CentroCosto').empty();
            $('#CentroCosto').append('<option selected value="">Seleccione...</option>');
            $.each(data, function (index, value) {
                $("#CentroCosto").select2();
                $('#CentroCosto').append('<option value="' + value.id + '">' + value.nombre + '</option>');
            });
        }
    });



}


function CalcularGastoComida() {
    var wServicio = $('#Servicio option:selected').text();
    var wMonto = $('#Monto').val();

    //if (wServicio == "Comida") {
    //    var FechaDesde = $("#FechaDesde").val();
    //    var FDdia = FechaDesde.substr(0,2);
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

    //    var wMontoTotal = wMonto * parseInt(wDias);

    //    return wMontoTotal;
        

    //}

    return wMonto;

}


$("#Concepto").keydown(function () {
    var wConcepto = $("#Concepto").val();

    if (wConcepto.length > 50) {
        wConcepto = wConcepto.substr(0, 50);
        $("#Concepto").val(wConcepto);
        toastr.warning("El concepto no puede ser superior a 50 caracteres.", "Información")
    }

});

$("#FechaDesde").change(function () {

    var wFechaDesde = $("#FechaDesde").val();
    var whoy = new Date();

    if (validarFormatoFecha(wFechaDesde)) {
        if (existeFecha(wFechaDesde)) {
            if (validarFechaMenorActual(wFechaDesde)) {
                $("#AuxFechaDesde").val($("#FechaDesde").val());
            } else {
                toastr.warning("La fecha desde no puede ser inferior al dia de hoy", "Información")
                $("#FechaDesde").datepicker("update", new Date());
                $("#AuxFechaDesde").val($("#FechaDesde").val());
            }
        } else {
            toastr.warning("La fecha no Existe", "Información")
            $("#FechaDesde").datepicker("update", new Date());
            $("#AuxFechaDesde").val($("#FechaDesde").val());
        }
    } else {
        toastr.warning("Formato de fecha es incorrecto", "Información")
        $("#FechaDesde").datepicker("update", new Date());
        $("#AuxFechaDesde").val($("#FechaDesde").val());
    }

});


$("#FechaHasta").change(function () {

    var wFechaDesde = $("#FechaDesde").val();
    var wFechaHasta = $("#FechaHasta").val();
    var whoy = new Date();
    whoy.setDate(whoy.getDate() + 1);

    if (validarFormatoFecha(wFechaDesde)) {
        if (existeFecha(wFechaDesde)) {
            if (validarFechaMenorDesde(wFechaDesde,wFechaHasta)) {
                $("#AuxFechaHasta").val($("#FechaHasta").val());
            } else {
                if ($("#TxtCargoFecha").val() == "N") {
                    $("#TxtCargoFecha").val("S");
                } else {
                    toastr.warning("La fecha hasta no puede ser inferior a la fecha desde", "Información");
                }
                $("#FechaHasta").datepicker("update", whoy);
                $("#AuxFechaHasta").val($("#FechaHasta").val());
            }
        } else {
            toastr.warning("La fecha no Existe", "Información");
            $("#FechaHasta").datepicker("update", whoy);
            $("#AuxFechaHasta").val($("#FechaHasta").val());
        }
    } else {
        toastr.warning("Formato de fecha es incorrecto", "Información");
        $("#FechaHasta").datepicker("update", whoy);
        $("#AuxFechaHasta").val($("#FechaHasta").val());
    }

});


function validarFormatoFecha(campo) {
    var RegExPattern = /^\d{1,2}\/\d{1,2}\/\d{2,4}$/;
    if ((campo.match(RegExPattern)) && (campo != '')) {
        return true;
    } else {
        return false;
    }
}

function existeFecha(fecha) {
    var fechaf = fecha.split("/");
    var day = fechaf[0];
    var month = fechaf[1];
    var year = fechaf[2];
    var date = new Date(year, month, '0');
    if ((day - 0) > (date.getDate() - 0)) {
        return false;
    }
    return true;
}

function existeFecha2(fecha) {
    var fechaf = fecha.split("/");
    var d = fechaf[0];
    var m = fechaf[1];
    var y = fechaf[2];
    return m > 0 && m < 13 && y > 0 && y < 32768 && d > 0 && d <= (new Date(y, m, 0)).getDate();
}

function validarFechaMenorActual(date) {
    var x = new Date();
    var fecha = date.split("/");
    x.setFullYear(fecha[2], fecha[1] - 1, fecha[0]);
    var today = new Date();

    if (x >= today)
        return true;
    else
        return false;
}

function validarFechaMenorDesde(date,date2) {
    var x = new Date();
    var y = new Date();

    var fecha = date.split("/");
    var fechaH = date2.split("/");
    x.setFullYear(fecha[2], fecha[1] - 1, fecha[0]);
    y.setFullYear(fechaH[2], fechaH[1] - 1, fechaH[0]);
    //var today = new Date();

    if (x >= y)
        return false;
    else
        return true;
}

