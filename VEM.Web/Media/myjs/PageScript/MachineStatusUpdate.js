$(document).ready(function () {

    $(".btnMachineStatusType").click(function () {
        var type = $(this).attr("thistype");
        var userId = $(this).attr("userid");
        $("#machineStatusType").val(type);
        $("#userId").val(userId);

        var username = $(this).attr("username");
        if (type == "0") {
            $("#ThisUserDiv").html("<i class='icon-globe'></i><span class='label label-info'>此售货机租借给</span><code>" + username + "</code>");
          
        } else {
            $("#ThisUserDiv").html("<i class='icon-globe'></i><span class='label label-important'>此售货机出售给</span><code>" + username + "</code>");

        }

    });

    $(".btnSaveMachineStatus").click(function () {
        var type = $("#machineStatusType").val();
        var userId = $("#userId").val();
        var machineId = $("#machineId").val();
        if (userId=="" || type=="") {
            Modal.alert({ msg: "请先选择一个用户", title: '错误信息', btnok: '确定' });
            return false;
        }

        AjaxRequest.submit(
                "/Machine/MachineStatusUpdate",
                { MachineId: machineId, UserId: userId, MachineStatusType: type },
               function (result) {
                   if ("操作成功" != STATUS[result.StatusCode]) {
                       Modal.alert({ msg: STATUS[result.StatusCode], title: '错误信息', btnok: '确定' });
                   } else {
                       ModalHtml.showSuccess("操作成功!", "操作成功完成", "/Machine/MachineStatus");
                   }
               },
               function (XMLHttpRequest) {
               });

    });


});