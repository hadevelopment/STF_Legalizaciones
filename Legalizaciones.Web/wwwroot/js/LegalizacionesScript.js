
//funcion que me muestra la pantalla modal con los gastos
function AnadirGastos(boton) {
        $('#gastosModal').modal('show');

}

function EnviarReporte(boton) {
    $('#EnviarReporteModal').modal('show');

}

function ImprimirReporte(boton) {
    alert("Imprimir reporte");
}

//Funcion cuando selecciona un item del listado pantalla modal
function Seleccionar(Id) {
    //var wFecha = new Date(date.getFullYear(), date.getMonth(), date.getDate());

    $.ajax({
        type: "GET",
        url: "/Localidad/SolicitudGastos",
        datatype: "Json",
        data: { wId: Id },
        success: function (data) {
            console.log(data);
            if (data !== null) {
                $("#GastosId").val(Id);
                $("#MontoGasto").val(data.monto);
                $("#Origen").val(data.origen);
                $("#Destino").val(data.destino);
                $("#FechaGasto").val(data.fechaGasto);

                var wPais = data.paisId;
                var wCiudad = data.ciudadId;
                var wServicio = data.servicioId;

                $.ajax({
                    type: "GET",
                    url: "/Localidad/Paises",
                    datatype: "Json",
                    success: function (data) {
                        $("#PaisId").empty();
                        $.each(data, function (index, value) {
                            if (value.id == wPais) {
                                $('#PaisId').append('<option selected value="' + value.id + '">' + value.nombre + '</option>');
                            } else {
                                $('#PaisId').append('<option value="' + value.id + '">' + value.nombre + '</option>');
                            }
                        });
                    }
                });


                $.ajax({
                    type: "GET",
                    url: "/Localidad/CiudadesPais",
                    datatype: "Json",
                    data: { paisID: wPais },
                    success: function (data) {
                        $("#CiudadId").empty();
                        $.each(data, function (index, value) {
                            if (value.id == wCiudad) {
                                $('#CiudadId').append('<option selected value="' + value.id + '">' + value.nombre + '</option>');
                            } else {
                                $('#CiudadId').append('<option value="' + value.id + '">' + value.nombre + '</option>');
                            }
                        });
                    }
                });

                $.ajax({
                    type: "GET",
                    url: "/UNOEE/Servicios",
                    datatype: "Json",
                    success: function (data) {
                        $("#Servicio").empty();
                        $.each(data, function (index, value) {
                            if (value.id == wServicio) {
                                $('#TiposervicioId').append('<option selected value="' + value.id + '">' + value.nombre + '</option>');
                            } else {
                                $('#TiposervicioId').append('<option value="' + value.id + '">' + value.nombre + '</option>');
                            }
                        });
                    }
                });




            }
            
        }
    });


    funcion_Visible($('#RegistroDatos'));

}

function AddNuevoGasto() {
    funcion_Visible($('#RegistroDatos'));
}

function getDescPais(wID) {

}

function getDescCiudad(wID) {

}

function AgregarFilaDatagrid() {

    var Id = $("#GastosId").val();

    $('#tbGastos tbody tr').each(function () {
        $projectName = $(this).find('td:eq(0)').text();
        if ($projectName == Id) {
            alert('Este gasto ya esta agregado');
            return;
        }
    });

    var PaisId = $("#PaisId").val();
    var Pais = "Colombia";
    var CiudadId = $("#CiudadId").val();
    var Ciudad = "Cali";
    var Monto = $("#MontoGasto").val();
    var FechaGasto = $("#FechaGasto").val();

    var PaisId = $("#PaisId option:selected").val();
    var Pais = $("#PaisId option:selected").text();

    var CiudadId = $("#CiudadId option:selected").val();
    var Ciudad = $("#CiudadId option:selected").text();

    //var Monto = $("#MontoGasto").val();

    var ServicioId = $("#TiposervicioId option:selected").val();
    var Servicio = $("#TiposervicioId option:selected").text();


    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();

    today = mm + '/' + dd + '/' + yyyy;

    var ConceptoGasto = $("#ConceptoGasto").val();

    var ProveedorId = $("#ProveedorId").val();
    var Proveedor = "";
    if (ProveedorId == 1) {
        Proveedor = "Proveedor Uno";
    } else {
        Proveedor = "Proveedor Dos";
    }

    var Monto = CalcularGastoComidaLegalizacion();

                 

    //los primero campos que indican display-none no se muestran pero son los campos que necesito que se llenen con valores 
    //para poder realizar la serializacion de json cuando le envie el datagrid con los nombre verdaderos de la clase y con sus id
    //para mostrar coloco los textos
    //tiene que estar en orden con el header del datagrid
    //    <td class="display-none">FechaaGasto</td>
    //    <td class="display-none">PaisId</td>
    //    <td class="display-none">CiudadId</td>
    //    <td class="display-none">TipoServicioId</td>
    //    <td class="display-none">ProveedorId</td>
    //    <td class="display-none">Concepto</td>
    //    <td class="display-none">Valor</td>
    //    <td>Item</td>
    //    <td>Fecha</td>
    //    <td>Centro Oper.</td>
    //    <td>Unidad Neg</td>
    //    <td>Centro Cost</td>
    //    <td>Pais</td>
    //    <td>Ciudad</td>
    //    <td>Servicio</td>
    //    <td>Proveedor</td>
    //    <td>Concepto Gasto</td>
    //    <td>Valor</td>
    //    <td>Acciones</td>


    var row = `<tr>  
                    <td class="display-none">${today}</td>
                    <td class="display-none">${PaisId}</td> 
                    <td class="display-none">${CiudadId}</td> 
                    <td class="display-none">${ServicioId}</td> 
                    <td class="display-none">${ProveedorId}</td>
                    <td class="display-none">${ConceptoGasto}</td>
                    <td class="class="display-none">${Monto}</td>
                    <td>${Id}</td>
                    <td>${today}</td>
                    <td>Centro de Operacion</td>
                    <td>Unidad de Negocio</td>
                    <td>Centro de Costo</td>
                    <td>${Pais}</td>
                    <td class="CiudadId">${Ciudad}</td>
                    <td class="ServicioId">${Servicio}</td>
                    <td class="display-none">${Proveedor}</td>
                    <td class="display-none">${ConceptoGasto}</td>
                    <td class="Monto">${Monto}</td>                
                    <td>
                        <a class="btn btn-danger btn-sm btnDelete" onclick='remove(this)'>
                            <span class="glyphicon glyphicon-trash"></span>
                        </a>
                    </td>
                </tr>`;

    $('#Items').append(row);


    //Suma de Montos de Gastos legalizados
    var sum = 0;
    $('td.Monto').each(function () {
        sum += parseFloat(this.innerHTML);
    });

    //$("#txMontoT").text(sum);
    
    var wSaldo = parseFloat($('#txSaldo').val());
    var wDiferencia = wSaldo - sum; 

    if (wDiferencia > 0) {
        funcion_InVisible($("#txMontoSobrante"));
        funcion_Visible($("#txMontoFaltante"));
        $('#txMontoFaltante').text(sum);
    } else {
        funcion_InVisible($("#txMontoFaltante"));
        funcion_Visible($("#txMontoSobrante"));
        $('#txMontoSobrante').text(sum);
    }
        

}


function GuardarGastos() {
    AgregarFilaDatagrid();
}


function funcion_Visible(obj) {
    obj.removeAttr("class = hide");
}

function funcion_InVisible(obj) {
    obj.addClass("hide");
}


//Funcion para cargar los datos
function cargarGastos() {
    var table = $('.tableGastos').tableToJSON({
        ignoreColumns: [16]
    });

    if (table.length > 0) {
        $("#mensajeRegistro").text("");
        $('#mensajeValidacionRegistro').hide("slow");
        $('#hdfGastosSolicitud').val(JSON.stringify(table));
        return true;
    } else {
        $("#mensajeRegistro").text("Debe añadir al menos un Gasto.");
        $('#mensajeValidacionRegistro').show("slow");
        return false;
    }

}

$("#PaisId").change(function () {
    valorPais = $("#PaisId").val();
    CargarCiudad(valorPais);
});

//-+-+-+-+-++- +-+-+-+-funciones ajax para carga de combos - +-+-+-+-+-+-+-+-


function CargarPais() {
    $.ajax({
        type: "GET",
        url: "/Localidad/Paises",
        datatype: "Json",
        success: function (data) {
            $("#PaisId").empty();
            $('#PaisId').append('<option selected value="">Seleccione...</option>');
            $.each(data, function (index, value) {
                $('#PaisId').append('<option value="' + value.id + '">' + value.nombre + '</option>');
            });
        }
    });
}

function CargarCiudad(valor) {
    $.ajax({
        type: "GET",
        url: "/Localidad/CiudadesPais",
        datatype: "Json",
        data: { paisID: valor },
        success: function (data) {
            $("#CiudadId").empty();
            $('#CiudadId').append('<option selected value="">Seleccione...</option>');
            $.each(data, function (index, value) {
                $('#CiudadId').append('<option value="' + value.id + '">' + value.nombre + '</option>');
            });
        }
    });
}


function CargarTipoServicio() {
    $('#TiposervicioId').append('<option value="1">Comida</option>');
    $('#TiposervicioId').append('<option value="2">transporte</option>');

}


function CargarMotivos() {
 $('#MotivoId').append('<option value="1">Motivo Uno</option>');
    $('#MotivoId').append('<option value="2">Motivo Dos</option>');

}


function CargarProveedor() {
    $('#ProveedorId').append('<option value="1">Proveedor Uno</option>');
    $('#ProveedorId').append('<option value="2">Proveedor Dos</option>');

}



//Cuando carga la pantalla
window.onload = function () {

    CargarTipoServicio();
    CargarProveedor();
    //CargarMotivos();
    CargarPais();

    var w = $('#txSaldo').val();
    w = w.replace(",", ".");
    $('#txSaldo').val(w); 

}


$('.datepicker').datepicker({
    format: 'dd/mm/yyyy',
    todayHighlight: true,
    autoclose: true,
    orientation: 'bottom auto'
});

$(".datepicker").datepicker("update", new Date());

function remove(tr) {
    $(tr).parent().parent().remove();
    return false;
}


function CalcularGastoComidaLegalizacion() {
    var wServicio = $('#TiposervicioId option:selected').text();
    var wMonto = $('#MontoGasto').val();

    if (wServicio == "Comida") {
        var FechaDesde = $("#FechaDesde").val();
        var FDdia = FechaDesde.substr(0, 2);
        var FDMes = FechaDesde.substr(3, 2);
        var FDAnno = FechaDesde.substr(6, 4);
        var wFDFormato = FDAnno + "-" + FDMes + "-" + FDdia;

        var FechaHasta = $("#FechaHasta").val();
        var FHdia = FechaHasta.substr(0, 2);
        var FHMes = FechaHasta.substr(3, 2);
        var FHAnno = FechaHasta.substr(6, 4);
        var wFHFormato = FHAnno + "-" + FHMes + "-" + FHdia;

        var fecha1 = moment(wFDFormato);
        var fecha2 = moment(wFHFormato);

        var wDias = fecha2.diff(fecha1, 'days');

        var wMontoTotal = wMonto * parseInt(wDias);

        return wMontoTotal;


    }

    return wMonto;

}
