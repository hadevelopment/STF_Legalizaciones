var ciudadActualizar = 0;

$(document).ready(function () {
    //remove button click event
    $('#Items').on('click', '.btnDelete', function () {
        if (confirm('¿Esta Seguro(a) de eliminar el gasto?')) {
            $(this).parents('tr').remove();
            //Suma de Montos de Gastos
            CalcularMonto();
        } else {
            return false;
        }
    });

    //Propiedades del dropdown Zona
    $("#Zona").select2({
        multiple: false
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

        if (nombreServicio === "ALIMENTACION") {
            $('#divGastosFecha').addClass('display-none');
            $('#FechaGasto').val('N/A');
        } else {
            $('#divGastosFecha').removeClass('display-none');
            $('#FechaGasto').val('');
        }

        if (!nombreServicio.indexOf("TRANSPORTE") >= 0 || !nombreServicio.indexOf("TRASLADO") >= 0) {
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

    });

    $("#CentroOperacion").change(function () {
        var texto = $("#CentroOperacion option:selected").text();
        $('#hdfCentroOperacion').val(texto);
    });

    $("#UnidadNegocio").change(function () {
        var texto = $("#UnidadNegocio option:selected").text();
        $('#hdfUnidadNegocio').val(texto);
    });

    $("#CentroCosto").change(function () {
        var texto = $("#CentroCosto option:selected").text();
        $('#hdfCentroCosto').val(texto);
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

    $("#btnModalGastos").click(function () {
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

    function validarFormTDC() {
        return false;
    }

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
        var valor = $("#Destino option:selected").val();
        $('#hdfDestino').val(valor);
        $.ajax({
            type: "GET",
            url: "/Localidad/ZonasDestinos",
            datatype: "Json",
            data: { destinoID: valor },

            success: function (data) {

                var zona = $('#hdfZonaId').val();

                $("#Zona").empty();
                $('#Zona').append('<option selected value="">Seleccione...</option>');
                var sw = false;
                $.each(data, function (index, value) {
                    if (sw === false) {
                        if (value.id == zona) {
                            $('#Zona').append('<option value="' + value.id + '" selected>' + value.nombre + '</option>');
                        } else {
                            $('#Zona').append('<option value="' + value.id + '">' + value.nombre + '</option>');
                        }
                        sw = true;
                    }
                    else {
                        if (value.id == zona) {
                            $('#Zona').append('<option value="' + value.id + '" selected>' + value.nombre + '</option>');
                        } else {
                            $('#Zona').append('<option value="' + value.id + '">' + value.nombre + '</option>');
                        }
                    }
                });

                $('#Zona').trigger('change');

                //busco la moneda
                $.ajax({
                    type: "GET",
                    url: "/Localidad/MonedasPorDestino",
                    datatype: "Json",
                    data: { wIdDestino: valor },
                    success: function (data) {
                        $("#Moneda").empty();
                        var moneda = $('#hdfMonedaId').val();

                        $.each(data, function (index, value) {
                            if (value.id == moneda) {
                                $('#Moneda').append('<option value="' + value.id + '" selected>' + value.nombre + '</option>');
                            } else {
                                $('#Moneda').append('<option value="' + value.id + '">' + value.nombre + '</option>');
                            }
                        });

                        $("#Moneda").trigger('change');
                    }
                });

            }
        });

    });

    $("#Zona").change(function () {
        var valor = $("#Zona option:selected").val();
        $('#hdfZona').val(valor);
    });

    $("#Moneda").change(function () {
        var valor = $("#Moneda option:selected").val();
        $('#hdfMoneda').val(valor);
    });

    //Obtener PAIS
    $.ajax({
        type: "GET",
        url: "/Localidad/Paises",
        datatype: "Json",
        success: function (data) {
            $("#Pais").empty();
            $('#Pais').append('<option selected value="" disabled>Seleccione...</option>');
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
        var valor = $("#Pais option:selected").val();
        $.ajax({
            type: "GET",
            url: "/Localidad/CiudadesPais",
            datatype: "Json",
            data: { paisID: valor },
            success: function (data) {
                $("#Ciudad").empty();
                $('#Ciudad').append('<option value="" disabled>Seleccione...</option>');
                $.each(data, function (index, value) {
                    $('#Ciudad').append('<option value="' + value.id + '">' + value.nombre + '</option>');
                });
            }
        });

        //Si viene de una actualizacion
        if (ciudadActualizar > 0) {
            $("#Ciudad").val(ciudadActualizar);
            $('#Ciudad').trigger('change');
            ciudadActualizar = 0;
        }

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
                    select: function (e, ui) {
                        $('#hdfZonaOrigen').val(ui.item.id);
                        consultarLimiteGasto();
                    }
                });

                $("#ZonaDestino").autocomplete({
                    source: dataZona,
                    minLength: 0,
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
    var monto = $("#Monto").maskMoney('unmasked')[0];
    //$('#monto').val('');

    if (monto === 0)
    {
        $("#mensajeGastos").text("Monto no puede estar en cero.");
        $('#mensajeValidacionGastos').show("slow");
        return false;
    }
    
    if (!servicio.indexOf("TRANSPORTE") >= 0 || !servicio.indexOf("TRASLADO") >= 0) {
        if (fechaGasto !== "" && servicio !== "" && monto !== "" || servicio === "ALIMENTACION" && fechaGasto !== "" && monto !== "") {
            $("#mensajeGastos").text("");
            $('#mensajeValidacionGastos').hide("slow");

            monto = CalcularGastoComida();
            rowIndex = rowIndex + 1;
            var row = `<tr class="rowIndex-${rowIndex}">
                    <td class="fechaGasto">${fechaGasto}</td>
                    <td class="paisId display-none">${paisId}</td>
                    <td class="pais">${pais}</td>
                    <td class="servicioId display-none">${servicioId}</td>
                    <td class="servicio">${servicio}</td>
                    <td class="ciudadId display-none">${ciudadId}</td>
                    <td class="ciudad">${ciudad}</td>
                    <td class="origen">N/A</td>
                    <td class="destino">N/A</td>
                    <td class="monto text-right"><input type="text" class="txtLabel maskMoney" readonly="readonly" id="monto-${rowIndex}" value="${monto}"/></td>
                    <td>
                        <a class="btn btn-danger btn-sm btnDelete">
                            <span class="glyphicon glyphicon-trash"></span>
                        </a>
                        <a class="btn btn-danger btn-sm btnEdit" onClick = "ShowModalUpdate('rowIndex-${rowIndex}');">
                            <span class="glyphicon glyphicon-edit"  ></span>
                        </a>
                    </td>
                </tr>`;

            $('#Items').append(row);

            //Se aplica Formato Moneda al monto
            $('#monto-' + rowIndex).maskMoney({ thousands: ',', decimal: '.', allowZero: true, suffix: '' });
            $('#monto-' + rowIndex).focus();

            //Suma de Montos de Gastos
            CalcularMonto();
        } else {
            $("#mensajeGastos").text("Faltan datos por especificar.");
            $('#mensajeValidacionGastos').show("slow");
            return false;
        }
    } else {
        if (fechaGasto !== "" && servicio !== "" && monto !== "" && origen !== "" && destino !== "") {
            rowIndex = rowIndex + 1;
            var row2 = `<tr class="rowIndex-${rowIndex}">
                    <td class="fechaGasto">${fechaGasto}</td>
                    <td class="paisId display-none">${paisId}</td>
                    <td class="pais">${pais}</td>
                    <td class="servicioId display-none">${servicioId}</td>
                    <td class="servicio">${servicio}</td>
                    <td class="ciudadId display-none">${ciudadId}</td>
                    <td class="ciudad">${ciudad}</td>
                    <td class="origen">${origen}</td>
                    <td class="destino">${destino}</td>
                    <td class="monto text-right"><input type="text" class="txtLabel maskMoney" readonly="readonly" id="monto-${rowIndex}" value="${monto}"/></td>
                    <td>
                        <a class="btn btn-danger btn-sm btnDelete">
                            <span class="glyphicon glyphicon-trash"></span>
                        </a>
                         <a class="btn btn-danger btn-sm btnEdit" onClick = "ShowModalUpdate('rowIndex-${rowIndex}');">
                            <span class="glyphicon glyphicon-edit"  ></span>
                        </a>
                    </td>
                </tr>`;

            $('#Items').append(row2);

            //Se aplica Formato Moneda al monto
            $('#monto-' + rowIndex).maskMoney({ thousands: ',', decimal: '.', allowZero: true, suffix: '' });
            $('#monto-' + rowIndex).focus();

            //Suma de Montos de Gastos
            CalcularMonto();

        } else {
            $("#mensajeGastos").text("Faltan datos por especificar.");
            $('#mensajeValidacionGastos').show("slow");
            return false;
        }
    }
}

function actualizarGastos() {
    var value = $('#hdfRowIndex').val();
    var index = value.split('-')[1];
    var fechaGasto = $("#FechaGasto").val();
    var paisId = $("#Pais option:selected").val();
    var pais = $("#Pais option:selected").text();
    var servicioId = $("#Servicio option:selected").val();
    var servicio = $("#Servicio option:selected").text();
    var ciudadId = $("#Ciudad option:selected").val();
    var ciudad = $("#Ciudad option:selected").text();
    var origen = $("#ZonaOrigen").val();
    var destino = $("#ZonaDestino").val();
    var monto = $("#Monto").maskMoney('unmasked')[0];

    if (!servicio.indexOf("TRANSPORTE") >= 0 || !servicio.indexOf("TRASLADO") >= 0) {
        if (pais !== "" && ciudad !== "" && fechaGasto !== "" && servicio !== "" && monto !== "" || pais !== "" && ciudad !== "" && servicio === "ALIMENTACION" && fechaGasto !== "" && monto !== "") {

            $("#mensajeGastos").text("");
            $('#mensajeValidacionGastos').hide("slow");

            monto = CalcularGastoComida();
            $('.' + value + ' .fechaGasto').text(fechaGasto);
            $('.' + value + ' .paisId').text(paisId);
            $('.' + value + ' .pais').text(pais);
            $('.' + value + ' .servicioId').text(servicioId);
            $('.' + value + ' .servicio').text(servicio);
            $('.' + value + ' .ciudadId').text(ciudadId);
            $('.' + value + ' .ciudad').text(ciudad);
            $('.' + value + ' .origen').text(origen);
            $('.' + value + ' .destino').text(destino);
            $('#monto-' + index).val(monto);
            $('#monto-' + index).maskMoney({ thousands: ',', decimal: '.', allowZero: true, suffix: '' });
            $('#monto-' + index).focus();

            $('#gastosModal').modal('hide');

        } else {
            $("#mensajeGastos").text("Faltan datos por especificar.");
            $('#mensajeValidacionGastos').show("slow");
            return false;
        }
    } else {
        if (pais !== "" && ciudad !== "" && fechaGasto !== "" && servicio !== "" && monto !== "" && origen !== "" && destino !== "") {
            $("#mensajeGastos").text("");
            $('#mensajeValidacionGastos').hide("slow");

            $('.' + value + ' .fechaGasto').text(fechaGasto);
            $('.' + value + ' .paisId').text(paisId);
            $('.' + value + ' .pais').text(pais);
            $('.' + value + ' .servicioId').text(servicioId);
            $('.' + value + ' .servicio').text(servicio);
            $('.' + value + ' .ciudadId').text(ciudadId);
            $('.' + value + ' .ciudad').text(ciudad);
            $('.' + value + ' .origen').text(origen);
            $('.' + value + ' .destino').text(destino);
            $('#monto-' + index).val(monto);
            $('#monto-' + index).maskMoney({ thousands: ',', decimal: '.', allowZero: true, suffix: '' });
            $('#monto-' + index).focus();

        } else {
            $('#gastosModal').modal('hide');
            $("#mensajeGastos").text("Faltan datos por especificar.");
            $('#mensajeValidacionGastos').show("slow");
            return false;
        }
    }

    CalcularMonto();
    return true;
}


function ShowModalUpdate(value) {
    var rowIndex = value.split('-')[1];
    var fechaGasto = $('.' + value + ' .fechaGasto').text();
    var paisId = $('.' + value + ' .paisId').text();
    var pais = $('.' + value + ' .pais').text();
    var servicioId = $('.' + value + ' .servicioId').text();
    var servicio = $('.' + value + ' .servicio').text();
    var ciudadId = $('.' + value + ' .ciudadId').text();
    var ciudad = $('.' + value + ' .ciudad').text();
    var origen = $('.' + value + ' .origen').text();
    var destino = $('.' + value + ' .destino').text();
    var monto = $('#monto-' + rowIndex).val();

    ciudadActualizar = ciudadId;

    $('#FechaGasto').val(fechaGasto);
    $("#Pais").val(paisId);
    $('#Pais').trigger('change');

    $("#Servicio").val(servicioId);

    $('#ZonaOrigen').val(origen);
    $('#ZonaDestino').val(destino);
    $('#Monto').val(monto);

    $('#btnAdd').addClass('display-none');
    $('#btnUpd').removeClass('display-none');
    $('#hdfRowIndex').val(value);

    if (servicio === "ALIMENTACION") {
        $('#divGastosFecha').addClass('display-none');
        $('#FechaGasto').val('N/A');
    } else {
        $('#divGastosFecha').removeClass('display-none');
    }

    if (servicio.indexOf("TRANSPORTE") >= 0 || servicio.indexOf("TRASLADO") >= 0) {
        $('#divGastosDescripcion').removeClass('col-md-12');
        $('#divGastosDescripcion').addClass('col-md-4');
        $('#divZonas').removeClass('display-none');
    } else {
        $('#divGastosDescripcion').removeClass('col-md-4');
        $('#divGastosDescripcion').addClass('col-md-12');
        $('#divZonas').addClass('display-none');
    }

    $('#gastosModal').modal('show');
    //$('#Monto').focus();
}

function validarViaje(boton) {
    $('#btnAdd').removeClass('display-none');
    $('#btnUpd').addClass('display-none');
    $('#btnUpd').addClass('display-none');

    var destino = $("#Destino option:selected").val();
    var zona = $("#Zona option:selected").val();
    var moneda = $("#Moneda option:selected").val();

    var dateMin = $('#FechaDesde').val();
    var dateMax = $('#FechaHasta').val();

    $('#FechaGasto').datepicker('remove');

    $('#FechaGasto').datepicker({
        language: 'es',
        format: 'yyyy-mm-dd',
        todayHighlight: true,
        autoclose: true,
        orientation: 'bottom auto',
        startDate: dateMin,
        endDate: dateMax,
    });

    if (destino !== "" && zona !== "" && moneda !== "") {

        $('#Pais').val('');
        $('#Pais').trigger('change');

        $('#mensajeValidacionViaje').hide("slow");
        $('#gastosModal').modal('show');

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

    if (_monedaID !== "" && _paisID !== "" && _tipoServicioID !== "") {
        if (_tipoServicio.indexOf("TRANSPORTE") >= 0 || _tipoServicio.indexOf("TRASLADO") >= 0) {
            $.ajax({
                type: "GET",
                url: "/ConfiguracionGasto/obtenerLimiteGastoRuta",
                datatype: "Json",
                data: { paisID: _paisID, tipoServicioID: _tipoServicioID, monedaID: _monedaID, origenID: _origenID, destinoID: _destinoID },
                success: function (data) {
                    if (data !== null) {
                        if (data.monto > 0 && data.monto !== '') {
                            $('#AvisoMontoServicio').removeClass('display-none');
                            $('#MensajeAviso').val(data.monto);
                            $("#MensajeAviso").focus();

                            $('#Monto').on('input', function () {
                                var value = $(this).val();
                                if ((value !== '') && (value.indexOf('.') === -1)) {
                                    $(this).val(Math.max(Math.min(value, data.monto), -data.monto));
                                }
                            });

                            $('#Monto').val(data.monto);
                            $('#Monto').focus();
                        } else {
                            $('#AvisoMontoServicio').addClass('display-none');
                            $('#MensajeAviso').val('');
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
                        $('#MensajeAviso').val('');
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
                            $('#MensajeAviso').val(data.monto);
                            $("#MensajeAviso").focus();

                            $('#Monto').on('input', function () {
                                var value = $(this).val();
                                if ((value !== '') && (value.indexOf('.') === -1)) {
                                    $(this).val(Math.max(Math.min(value, data.monto), -data.monto));
                                }
                            });
                            $('#Monto').val(data.monto);
                            $('#Monto').focus();
                        } else {
                            $('#AvisoMontoServicio').addClass('display-none');
                            $('#MensajeAviso').val();

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
                        $('#MensajeAviso').val('');

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

window.onload = function () {
    if ($("#txProceso").val() !== "A") {
        CargarListasCrear();
        var date = new Date();
        $(".datepicker").datepicker("update", new Date());
    } else {
        CargarListasEditar();
    }
};

function CargarListasCrear() {
    //Obtener DESTINOS
    $.ajax({
        type: "GET",
        url: "/Localidad/Destinos",
        datatype: "Json",
        success: function (data) {
            $("#Destino").empty();
            $('#Destino').append('<option value="">Seleccione...</option>');
            $.each(data, function (index, value) {
                //$("#Destino").select2();
                $('#Destino').append('<option value="' + value.id + '">' + value.nombre + '</option>');
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

    //Obtener unidades de negocio
    $.ajax({
        type: "GET",
        url: "/UNOEE/UnidadNegocios",
        datatype: "Json",
        success: function (data) {
            $('#UnidadNegocio').empty();
            $('#UnidadNegocio').append('<option value="">Seleccione...</option>');
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
            $('#CentroOperacion').append('<option value="">Seleccione...</option>');
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
            $('#CentroCosto').append('<option value="">Seleccione...</option>');
            $.each(data, function (index, value) {
                $("#CentroCosto").select2();
                $('#CentroCosto').append('<option value="' + value.id + '">' + value.nombre + '</option>');
            });
        }
    });
}

function CargarListasEditar() {
    //Obtener DESTINOS
    $.ajax({
        type: "GET",
        url: "/Localidad/Destinos",
        datatype: "Json",
        success: function (data) {
            var destino = $('#hdfDestinoId').val();

            $("#Destino").empty();
            $('#Destino').append('<option value="">Seleccione...</option>');
            $.each(data, function (index, value) {
                if (value.id == destino) {
                    $('#Destino').append('<option value="' + value.id + '" selected>' + value.nombre + '</option>');
                } else {
                    $('#Destino').append('<option value="' + value.id + '">' + value.nombre + '</option>');
                }
            });

            $('#Destino').trigger('change');
        }
    });

    //Obtener zonas
    $.ajax({
        type: "GET",
        url: "/Home/Zonas",
        datatype: "Json",
        success: function (data) {
            var zona = $('#hdfZonaId').val();

            $.each(data, function (index, value) {
                if (value.id == zona) {
                    $('#Zona').append('<option value="' + value.id + '" selected>' + value.nombre + '</option>');
                } else {
                    $('#Zona').append('<option value="' + value.id + '">' + value.nombre + '</option>');
                }
            });

            $('#Zona').trigger('change');
        }
    });

    //Obtener MONEDAS
    $.ajax({
        type: "GET",
        url: "/Localidad/Monedas",
        datatype: "Json",
        success: function (data) {
            var moneda = $('#hdfMonedaId').val();

            $.each(data, function (index, value) {
                if (value.id == moneda) {
                    $('#Moneda').append('<option value="' + value.id + '" selected>' + value.nombre + '</option>');
                } else {
                    $('#Moneda').append('<option value="' + value.id + '">' + value.nombre + '</option>');
                }
            });
        }
    });

    //Obtener unidades de negocio
    $.ajax({
        type: "GET",
        url: "/UNOEE/UnidadNegocios",
        datatype: "Json",
        success: function (data) {
            var unidadNegocio = $('#hdfUnidadNegocioId').val();

            $('#UnidadNegocio').empty();
            $('#UnidadNegocio').append('<option value="">Seleccione...</option>');
            $.each(data, function (index, value) {
                if (value.id == unidadNegocio) {
                    $('#UnidadNegocio').append('<option value="' + value.id + '" selected>' + value.nombre + '</option>');
                } else {
                    $('#UnidadNegocio').append('<option value="' + value.id + '">' + value.nombre + '</option>');
                }
            });

            var texto = $("#UnidadNegocio option:selected").text();
            $('#hdfUnidadNegocio').val(texto);
        }
    });

    //Obtener centro de operaciones
    $.ajax({
        type: "GET",
        url: "/UNOEE/CentroOperaciones",
        datatype: "Json",
        success: function (data) {
            var centroOperacion = $('#hdfCentroOperacionId').val();

            $('#CentroOperacion').empty();
            $('#CentroOperacion').append('<option value="">Seleccione...</option>');
            $.each(data, function (index, value) {
                if (value.id == centroOperacion) {
                    $('#CentroOperacion').append('<option value="' + value.id + '" selected>' + value.nombre + '</option>');
                } else {
                    $('#CentroOperacion').append('<option value="' + value.id + '">' + value.nombre + '</option>');
                }
            });

            var texto = $("#CentroOperacion option:selected").text();
            $('#hdfCentroOperacion').val(texto);
        }
    });


    //Obtener centros de costo
    $.ajax({
        type: "GET",
        url: "/UNOEE/CentroCostos",
        datatype: "Json",
        success: function (data) {
            var centroCosto = $('#hdfCentroCostoId').val();

            $('#CentroCosto').empty();
            $('#CentroCosto').append('<option value="">Seleccione...</option>');
            $.each(data, function (index, value) {
                if (value.id == centroCosto) {
                    $('#CentroCosto').append('<option value="' + value.id + '" selected>' + value.nombre + '</option>');
                } else {
                    $('#CentroCosto').append('<option value="' + value.id + '">' + value.nombre + '</option>');
                }
            });

            var texto = $("#CentroCosto option:selected").text();
            $('#hdfCentroCosto').val(texto);
        }
    });
}

function ValidarForm() {
    var result = true;
    var destino = $("#Destino option:selected").val();
    var zona = $("#Zona option:selected").val();
    var moneda = $("#Moneda option:selected").val();
    var centroOperacion = $("#CentroOperacion option:selected").val();
    var unidadNegocio = $("#UnidadNegocio option:selected").val();
    var centroCosto = $("#CentroCosto option:selected").val();
    var fechaDesde = $('#FechaDesde').val();
    var fechaHasta = $('#FechaHasta').val();
    var cantidadGastos = $('.tableGastos tr').length;

    if (destino == "" || zona == "" || moneda == "" || centroOperacion == ""
        || unidadNegocio == "" || centroCosto == "" || fechaDesde == "" || fechaHasta == "") {

        $("#mensajeViaje").html("Faltan datos por específicar.");
        $('#mensajeValidacionViaje').show("slow");
        result = false;
    } else {
        $("#mensajeViaje").html("");
        $('#mensajeValidacionViaje').hide("slow");
    }

    if (cantidadGastos == 1) {
        $("#mensajeRegistro").text("Debe añadir al menos un Gasto.");
        $('#mensajeValidacionRegistro').show("slow");
        result = false;
    } else {
        $("#mensajeRegistro").text("");
        $('#mensajeValidacionRegistro').hide("slow");
    }

    return result;
}

function CalcularMonto() {
    console.log('calculando montos');

    //Suma de Montos de Gastos
    var sum = 0;
    $('td.monto input').each(function () {
        sum += parseFloat($(this).maskMoney('unmasked')[0]);
    });
    $("#txMontoT").val(sum);
    $('#txMontoT' + rowIndex).maskMoney({ thousands: ',', decimal: '.', allowZero: true, suffix: '' });
    $("#txMontoT").focus();
    $('#hdfMontoSolicitud').val($("#txMontoT").val());

    var cantidadGastos = $('.tableGastos tr').length;
    if (cantidadGastos > 1) {
        $('#Destino').attr('disabled', true);
        $('#Zona').attr('disabled', true);
        $('#Moneda').attr('disabled', true);
    } else {
        $('#Destino').attr('disabled', false);
        $('#Zona').attr('disabled', false);
        $('#Moneda').attr('disabled', false);
    }
}

function CalcularGastoComida() {
    var wServicio = $('#Servicio option:selected').text();
    var wMonto = $('#Monto').maskMoney('unmasked')[0];

    if (wServicio === "ALIMENTACION") {
        var FechaDesde = $("#FechaDesde").val();
        var FechaHasta = $("#FechaHasta").val();

        var fecha1 = moment(FechaDesde);
        var fecha2 = moment(FechaHasta);

        var wDias = fecha2.diff(fecha1, 'days');
        wDias = wDias + 1;

        var wMontoTotal = wMonto * parseInt(wDias);

        return wMontoTotal;
    }

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

function cargarGastos() {
    if (ValidarForm()) {
        var table = $('.tableGastos').tableToJSON({
            textExtractor: function (cellIndex, $cell) {
                // get text from an input or select inside table cells;
                // if empty or non-existent, get the cell text
                return $cell.find('input, select').val() || $cell.text();
            },
            ignoreColumns: [10]
        });

        if (table.length > 0) {
            $('#hdfGastosSolicitud').val(JSON.stringify(table));
        }

        return true;

    } else {
        return false;
    }
}

