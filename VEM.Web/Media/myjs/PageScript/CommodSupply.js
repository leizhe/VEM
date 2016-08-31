$(document).ready(function () {
  
    $(".btnManageContainerRoad").click(function () {
        $.get($(this).attr("url"), function (result) {
            ModalHtml.showHtml("管理货道", result, "");
        });
    });
});