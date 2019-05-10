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
                CentroCosto: ($("#CentroCosto").val()),
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



    //Obtener zonas
    //$.ajax({
    //    type: "GET",
    //    url: "/Home/Zonas",
    //    datatype: "Json",
    //    success: function (data) {
    //        $.each(data, function (index, value) {
    //            $("#Zona").select2();
    //            $('#Zona').append('<option value="' + value.nombre + '">' + value.nombre + '</option>');
    //        });
    //    }
    //});

    //Propiedades del dropdown concepto
    $("#Zona").select2({
        multiple: false
    });

    //Obtener centro de operaciones
    //$.ajax({
    //    type: "GET",
    //    url: "/UNOEE/CentroOperaciones",
    //    datatype: "Json",
    //    success: function (data) {
    //        $('#CentroOperacion').empty();
    //        $('#CentroOperacion').append('<option selected value="">Seleccione...</option>');
    //        $.each(data, function (index, value) {
    //            $("#CentroOperacion").select2();
    //            $('#CentroOperacion').append('<option value="' + value.id + '">' + value.nombre + '</option>');
    //        });
    //    }
    //});

    //Propiedades del dropdown de centros de operaciones
    $("#CentroOperacion").select2({
        multiple: false
    });

    //Obtener unidades de negocio
    //$.ajax({
    //    type: "GET",
    //    url: "/UNOEE/UnidadNegocios",
    //    datatype: "Json",
    //    success: function (data) {
    //        $('#UnidadNegocio').empty();
    //        $('#UnidadNegocio').append('<option selected value="">Seleccione...</option>');
    //        $.each(data, function (index, value) {
    //            $("#UnidadNegocio").select2();
    //            $('#UnidadNegocio').append('<option value="' + value.id + '">' + value.nombre + '</option>');
    //        });
    //    }
    //});
    //Propiedades del dropdown unidades de negocio
    $("#UnidadNegocio").select2({
        multiple: false
    });

    //Obtener centros de costo
    //$.ajax({
    //    type: "GET",
    //    url: "/UNOEE/CentroCostos",
    //    datatype: "Json",
    //    success: function (data) {
    //        $('#CentroCosto').empty();
    //        $('#CentroCosto').append('<option selected value="">Seleccione...</option>');
    //        $.each(data, function (index, value) {
    //            $("#CentroCosto").select2();
    //            $('#CentroCosto').append('<option value="' + value.id + '">' + value.nombre + '</option>');
    //        });
    //    }
    //});

    //Propiedades del dropdown unidades de negocio
    $("#CentroCosto").select2({
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

    $(".datepicker").datepicker("update", new Date());
    //$('#Product').select2();

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
        $('#mensajeValidacionGastos').hide("slow");
    });


    //************************************   I N I C I O  ************************************
    /* Validaciones para los cambios de Destino - Pais. Se refrescan los combos de
     * Estado - Pais al cambiar destino o al cambiar pais */
    //*****************************************************************************************


    //Obtener EMPLEADOS
    $.ajax({
        type: "GET",
        url: "/UNOEE/Empleados",
        datatype: "Json",
        success: function (data) {
            $("#Empleado").empty();
            $('#Empleado').append('<option selected value="">Seleccione...</option>');
            $.each(data, function (index, value) {
                //$("#Destino").select2();
                $('#Empleado').append('<option value="' + value.cedula + '">' + value.nombre + '</option>');
            });
        }
    });

    //Propiedades del dropdown destinos
    $("#Empleado").select2({
        multiple: false
    });

    //Obtener DESTINOS
    //$.ajax({
    //    type: "GET",
    //    url: "/Localidad/Destinos",
    //    datatype: "Json",
    //    success: function (data) {
    //        $("#Destino").empty();
    //        $('#Destino').append('<option selected value="">Seleccione...</option>');
    //        $.each(data, function (index, value) {
    //            //$("#Destino").select2();
    //            $('#Destino').append('<option value="' + value.id + '">' + value.nombre + '</option>');
    //        });
    //    }
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

    if (servicio !== "Movilidad" && servicio !== "Transporte") {
        if (fechaGasto !== "" && servicio !== "" && monto !== "") {

            var row = `<tr>
                    <td class="fechaGasto">${fechaGasto}</td>
                    <td class="PaisId display-none">${paisId}</td>
                    <td class="Pais">${pais}</td>
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
                    </td>
                </tr>`;

            $('#Items').append(row);

            //Suma de Montos de Gastos
            var sum = 0;
            $('td.monto').each(function () {
                sum += parseFloat(this.innerHTML);
            });
            $("#txMontoT").val(sum);
            $('#hdfMontoSolicitud').val($("#txMontoT").val());

        } else {
            $("#mensajeGastos").text("Faltan datos por especificar.");
            $('#mensajeValidacionGastos').show("slow");
            return false;
        }

    } else {
        if (fechaGasto !== "" && servicio !== "" && monto !== "" && origen !== "" && destino !== "") {

            var row2 = `<tr>
                    <td class="fechaGasto">${fechaGasto}</td>
                    <td class="PaisId display-none">${paisId}</td>
                    <td class="Pais">${pais}</td>
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

function validarViaje(boton) {
    var destino = $("#Destino option:selected").val();
    var zona = $("#Zona option:selected").val();
    var moneda = $("#Moneda option:selected").val();

    if (destino !== "" && zona !== "" && moneda !== "") {

        $('#mensajeValidacionViaje').hide("slow");
        $('#gastosModal').modal('show');
        
        return true;

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
                    
                    if (data.monto > 0 && data.monto !== '') {
                        $('#AvisoMontoServicio').removeClass('display-none');
                        $('#MensajeAviso').text('Monto Limite: ' + data.monto);

                        $('#Monto').on('input', function () {
                            var value = $(this).val();
                            if ((value !== '') && (value.indexOf('.') === -1)) {
                                $(this).val(Math.max(Math.min(value, data.monto), -data.monto));
                            }
                        });
                    } else {
                        $('#AvisoMontoServicio').addClass('display-none');
                        $('#MensajeAviso').text('');
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
                    
                    if (data.monto > 0 && data.monto !== '') {
                        $('#AvisoMontoServicio').removeClass('display-none');
                        $('#MensajeAviso').text('Monto Limite: ' + data.monto);

                        $('#Monto').on('input', function () {
                            var value = $(this).val();
                            if ((value !== '') && (value.indexOf('.') === -1)) {
                                $(this).val(Math.max(Math.min(value, data.monto), -data.monto));
                            }
                        });
                    } else {
                        $('#AvisoMontoServicio').addClass('display-none');
                        $('#MensajeAviso').text('');
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