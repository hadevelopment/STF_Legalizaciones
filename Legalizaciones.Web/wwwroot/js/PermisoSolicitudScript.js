$(document).ready(function () {
    //Obtener EMPLEADOS
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
                    $('#Empleado').append('<option value="' + value.cedula + '">' + value.nombre + '</option>');
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

    // Element Actions

    $('#Empleado').on('change', function (e) {
        var selectedValue = $('#Empleado').val();
        if (selectedValue !== '-1') {
            $('#addEmpleadoPermiso').prop("disabled", false);
        } else {
            $('#addEmpleadoPermiso').prop("disabled", true);
        } 
    }); 

    // Modal Actions
    $('#addEmpleadoPreferenciaLegalizacion').on('hidden.bs.modal', function () {
        $('#addEmpleadoPermiso').prop("disabled", true);
        $("#Empleado").prop('selectedIndex', 0).change();
    });

    // Ajax Requests
    $("#addEmpleadoPermiso").click(function () {
        var cedulaEmpleado = $("#Empleado").val();
        var empleado = arrayEmpleadoPermiso.find(x => x.cedula === cedulaEmpleado);
        $.ajax({
            type: "POST",
            url: "/Empleado/PreferenciasLegalizacion",
            data: empleado,
            datatype: "Json",
            beforeSend: function () {
                // console.log("Before Send Request");
            },
            success: function (data) {
                // console.log("Succes!")
            },  
            complete: function () {
                location.reload();
                $("#addEmpleadoPreferenciaLegalizacion").modal("hide");
            }
        });
    });

    var validobj = $("#frm").validate({
        onkeyup: false,
        errorClass: "myErrorClass",

        //put error message behind each form element
        errorPlacement: function (error, element) {
            var elem = $(element);
            error.insertAfter(element);
        },

        //When there is an error normally you just add the class to the element.
        // But in the case of select2s you must add it to a UL to make it visible.
        // The select element, which would otherwise get the class, is hidden from
        // view.
        highlight: function (element, errorClass, validClass) {
            var elem = $(element);
            if (elem.hasClass("select2-offscreen")) {
                $("#s2id_" + elem.attr("id") + " ul").addClass(errorClass);
            } else {
                elem.addClass(errorClass);
            }
        },

        //When removing make the same adjustments as when adding
        unhighlight: function (element, errorClass, validClass) {
            var elem = $(element);
            if (elem.hasClass("select2-offscreen")) {
                $("#s2id_" + elem.attr("id") + " ul").removeClass(errorClass);
            } else {
                elem.removeClass(errorClass);
            }
        }
    });

    //If the change event fires we want to see if the form validates.
    //But we don't want to check before the form has been submitted by the user
    //initially.
    $(document).on("change", ".select2-offscreen", function () {
        if (!$.isEmptyObject(validobj.submitted)) {
            validobj.form();
        }
    });

    //A select2 visually resembles a textbox and a dropdown.  A textbox when
    //unselected (or searching) and a dropdown when selecting. This code makes
    //the dropdown portion reflect an error if the textbox portion has the
    //error class. If no error then it cleans itself up.
    $(document).on("select2-opening", function (arg) {
        var elem = $(arg.target);
        if ($("#s2id_" + elem.attr("id") + " ul").hasClass("myErrorClass")) {
            //jquery checks if the class exists before adding.
            $(".select2-drop ul").addClass("myErrorClass");
        } else {
            $(".select2-drop ul").removeClass("myErrorClass");
        }
    });



});