$(document).ready(function () {
    $('.datepicker').datepicker({
        language: 'es',
        format: 'yyyy-mm-dd',
        todayHighlight: true,
        autoclose: true,
        orientation: 'bottom auto'//,
        //startDate: '2019-06-26'
    }).on("changeDate", function (dateText, inst) {
        var id = $(this).attr('id');
        console.log(id);
    });
});