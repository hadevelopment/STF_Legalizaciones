window.onload = function () {

    if ($("#txProceso").val() != "A") {
        CargarComboAlcrear();
    } else {
        CargarCombosAlEditar();
    }

}

function CargarComboAlcrear() {

    //Obtener DESTINOS
    $.ajax({
        type: "GET",
        url: "/Localidad/Destinos",
        datatype: "Json",
        success: function(data) {
            $("#Destino").empty();
            $('#Destino').append('<option selected value="">Seleccione...</option>');
            $.each(data,
                function(index, value) {
                    //$("#Destino").select2();
                    $('#Destino').append('<option value="' + value.id + '">' + value.nombre + '</option>');
                });
        }
    });

}

function CargarCombosAlEditar() {
    //Obtener DESTINOS

    $.ajax({
        type: "GET",
        url: "/Localidad/ZonasDestinosMaeEdit" + "?Id=" + $('#hdDestinoId').val(),
        datatype: "Json",
        success: function(resultado) {
            $.each(resultado,
                function(i, value) {
                    var wN = value.nombre;
                    var wSel = wN.substr(wN.length - 2, 2);
                    if (wSel == "XX") {
                        wN = wN.substr(0, wN.length - 2);
                        $('#Destino').append('<option selected value="' + value.id + '">' + wN + '</option>');
                    } else {
                        $('#Destino').append('<option value="' + value.id + '">' + value.nombre + '</option>');
                    }
                });

        }
    });

}

$("#Destino").select2({
    multiple: false
});