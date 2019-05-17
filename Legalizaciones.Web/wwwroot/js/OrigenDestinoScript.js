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
        url: "/Localidad/Paises",
        datatype: "Json",
        success: function(data) {
            $("#Pais").empty();
            $('#Pais').append('<option selected value="">Seleccione...</option>');
            $.each(data,
                function(index, value) {
                    //$("#Destino").select2();
                    $('#Pais').append('<option value="' + value.id + '">' + value.nombre + '</option>');
                });
        }
    });

}

function CargarCombosAlEditar() {
    //Obtener DESTINOS

    $.ajax({
        type: "GET",
        url: "/Localidad/PaisMaeEdit" + "?Id=" + $('#hdPaisId').val(),
        datatype: "Json",
        success: function(resultado) {
            $.each(resultado,
                function(i, value) {
                    var wN = value.nombre;
                    var wSel = wN.substr(wN.length - 2, 2);
                    if (wSel == "XX") {
                        wN = wN.substr(0, wN.length - 2);
                        $('#Pais').append('<option selected value="' + value.id + '">' + wN + '</option>');
                    } else {
                        $('#Pais').append('<option value="' + value.id + '">' + value.nombre + '</option>');
                    }
                });

        }
    });

}

$("#Pais").select2({
    multiple: false
});

$(document).ready(function() {
    $('#btnUpload').on('click', function() {

     
        var fileExtension = ['xls', 'xlsx'];
        var filename = $('#fileExcel').val();
        if (filename.length == 0) {
            alert("Please select a file.");
            return false;
        } else {
            var extension = filename.replace(/^.*\./, '');
            if ($.inArray(extension, fileExtension) == -1) {
                alert("Please select only excel files.");
                return false;
            }
        }

        
        var fdata = new FormData();
        var fileUpload = $("#fileExcel").get(0);
        var files = fileUpload.files;
        fdata.append(files[0].name, files[0]);

        $.ajax({
            type: "POST",
            url: "/OrigenDestino/ImportData",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: fdata,
            contentType: false,
            processData: false,
            success: function (response) {
                if (response.length == 0)
                    alert('Some error occured while uploading');
                else {
                    $('#divTable').html(response);
                    $('#divUpload').hide();
                    $('#divUpdateGrid').css('visibility', 'visible');
                }
            },
            error: function (e) {
                $('#divTable').html(e.responseText);
            }
        });
    })
});