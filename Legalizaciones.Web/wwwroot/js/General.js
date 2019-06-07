$(document).ready(function () {
    $('.datepicker').datepicker({
        language: 'es',
        format: 'yyyy-mm-dd',
        todayHighlight: true,
        autoclose: true,
        orientation: 'bottom auto'
    }).on("changeDate", function (dateText, inst) {
        var id = $(this).attr('id');
        console.log(id);
    });
});