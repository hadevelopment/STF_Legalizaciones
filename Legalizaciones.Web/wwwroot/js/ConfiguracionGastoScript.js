$(document).ready(function () {

    $.ajax({
        type: "GET",
        url: "/UNOEE/Cargos",
        datatype: "Json",
        success: function (data) {
            $("#CargoId").empty();
            $('#CargoId').append('<option disabled selected value="">Seleccione...</option>');
            $.each(data, function (index, value) {
                //$("#Destino").select2();
                $('#CargoId').append('<option value="' + value.id + '">' + value.nombre + '</option>');
            });
        }
    });

    //Propiedades del dropdown destinos
    $("#CargoId").select2({
        multiple: false
    });

    $('#CargoId').change(function () {
        var cargoId = $('#CargoId  option:selected').val();
        var cargo = $('#CargoId  option:selected').text();

        if (cargoId !== "") {
            $('#hdfCargo').val(cargo);
            console.log($('#hdfCargo').val());
        } else {
            $('#hdfCargo').val('');
        }
    });

    $('#PaisId').change(function () {
        var paisId = $('#PaisId  option:selected').val();
        var pais = $('#PaisId  option:selected').text();

        if (paisId !== "") {
            $('#hdfPais').val(pais);
            console.log($('#hdfPais').val());
        } else {
            $('#hdfPais').val('');
        }
    });

    $('#TipoServicioId').change(function () {
        var tipoServicioId = $('#TipoServicioId  option:selected').val();
        var tipoServicio = $('#TipoServicioId  option:selected').text();

        if (tipoServicioId !== "") {
            $('#hdfTipoServicio').val(tipoServicio);
            console.log($('#hdfTipoServicio').val());
            if (tipoServicio !== "Transporte" || tipoServicio !== "Movilidad") {
                $('.zonaPais').empty();
                $('.zonaPais').append('<option disabled selected value="">Seleccione...</option>');
            }
        } else {
            $('#hdfTipoServicio').val('');
        }

        if (tipoServicio === "Transporte" || tipoServicio === "Movilidad") {
            $('.zonaPais').attr("required", "required");
        } else {
            $('.zonaPais').removeAttr("required");
        }
    });

    $.ajax({
        type: "GET",
        url: "/Localidad/Paises",
        datatype: "Json",
        success: function (data) {
            $("#PaisId").empty();
            $('#PaisId').append('<option disabled selected value="">Seleccione...</option>');
            $.each(data, function (index, value) {
                //$("#Destino").select2();
                $('#PaisId').append('<option value="' + value.id + '">' + value.nombre + '</option>');
            });
        }
    });

    //Propiedades del dropdown destinos
    $("#PaisId").select2({
        multiple: false
    });

    $('#PaisId').change(function () {
        var pais = $('#PaisId  option:selected').val();
        var servicio = $('#TipoServicioId  option:selected').text();

        if (pais !== "" && servicio === "Transporte" || pais !== "" && servicio === "Movilidad") {
            $('.zonaPais').removeAttr("disabled");
            cargarOrigenDestinos(pais);
        } else {
            $('.zonaPais').attr("disabled", "disabled");
        }
    });


    $.ajax({
        type: "GET",
        url: "/UNOEE/Servicios",
        datatype: "Json",
        success: function (data) {
            $("#TipoServicioId").empty();
            $('#TipoServicioId').append('<option disabled selected value="">Seleccione...</option>');
            $.each(data, function (index, value) {
                //$("#Destino").select2();
                $('#TipoServicioId').append('<option value="' + value.id + '">' + value.nombre + '</option>');
            });
        }
    });

    //Propiedades del dropdown destinos
    $("#TipoServicioId").select2({
        multiple: false
    });

    //*****************************************************************************************
    // Obtener Servicios para  el modal de Gastos 
    // Fecha 09/05/19
    // Autior: Daniel
    //*****************************************************************************************

    $.ajax({
        type: "GET",
        url: "/UNOEE/Servicios",
        datatype: "Json",
        success: function (data) {
            $("#TipoServicioId").empty();
            $('#TipoServicioId').append('<option disabled selected value="">Seleccione...</option>');
            $.each(data, function (index, value) {
                //$("#Destino").select2();
                $('#TipoServicioId').append('<option value="' + value.id + '">' + value.nombre + '</option>');
            });
        }
    });

    //Propiedades del dropdown destinos
    $("#TipoServicioId").select2({
        multiple: false
    });

    $('#TipoServicioId').change(function () {
        var pais = $('#PaisId  option:selected').val();
        var servicio = $('#TipoServicioId  option:selected').text();

        if (pais !== "" && servicio === "Transporte" || pais !== "" && servicio === "Movilidad") {
            $('#hdfOrigen').val('');
            $('#hdfDestino').val('');
            $('.zonaPais').removeAttr("disabled");
            cargarOrigenDestinos(pais);
        } else {
            $('.zonaPais').attr("disabled", "disabled");
        }
    });

    //Obtener MONEDAS
    $.ajax({
        type: "GET",
        url: "/Localidad/Monedas",
        datatype: "Json",
        success: function (data) {
            $("#MonedaId").empty();
            $('#MonedaId').append('<option disabled selected value="">Seleccione...</option>');
            $.each(data, function (index, value) {
                //$("#Destino").select2();
                $('#MonedaId').append('<option value="' + value.id + '">' + value.nombre + '</option>');
            });
        }
    });

    //Propiedades del dropdown destinos
    $("#MonedaId").select2({
        multiple: false
    });

    $('#ZonaOrigenId').change(function () {
        var zonaOrigenId = $('#ZonaOrigenId  option:selected').val();
        var zonaOrigen = $('#ZonaOrigenId  option:selected').text();

        if (zonaOrigenId !== "") {
            $('#hdfOrigen').val(zonaOrigen);
            console.log($('#hdfOrigen').val());
        } else {
            $('#hdfOrigen').val('');
        }
    });

    $('#ZonaDestinoId').change(function () {
        var zonaDestinoId = $('#ZonaDestinoId  option:selected').val();
        var zonaDestino = $('#ZonaDestinoId  option:selected').text();

        if (zonaDestinoId !== "") {
            $('#hdfDestino').val(zonaDestino);
            console.log($('#hdfDestino').val());
        } else {
            $('#hdfDestino').val('');
        }
    });

    $('#MonedaId').change(function () {
        var monedaId = $('#MonedaId  option:selected').val();
        var moneda = $('#MonedaId  option:selected').text();

        if (monedaId !== "") {
            $('#hdfMoneda').val(moneda);
            console.log($('#hdfMoneda').val());
        } else {
            $('#hdfMoneda').val('');
        }
    });

    function cargarOrigenDestinos(pais) {
        $.ajax({
            type: "GET",
            url: "/UNOEE/OrigenDestinosPais",
            datatype: "Json",
            data: { paisID: pais },
            success: function (data) {
                $(".zonaPais").empty();
                $('.zonaPais').append('<option disabled selected value="">Seleccione...</option>');
                $.each(data, function (index, value) {
                    $('.zonaPais').append('<option value="' + value.id + '">' + value.nombre + '</option>');
                });
            }
        });
    }


    
});