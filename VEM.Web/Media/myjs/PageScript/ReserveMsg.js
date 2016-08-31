$(document).ready(function () {

    $(".btnDetailsContainerRoad").click(function () {
        $.get($(this).attr("url"), function (result) {
            $("#divContainerRoad").html(result).parent().parent().parent().parent().show();
            $("#divMachine").hide();
        });
    });
    $(".btnBackMachine").click(function () {
        $("#divContainerRoad").html("").parent().parent().parent().parent().hide();
        $("#divMachine").show();
    });
    
});