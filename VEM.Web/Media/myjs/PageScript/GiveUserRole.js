var GiveUserRoleValidation = function () {    
    return {
        init: function () {
            $('#UserDetailsForm').validate({
                submitHandler: function (form) {
                    AjaxRequest.submit(
                       "/Privilege/SetRole",
                       $(form).serialize(),
                      function (result) {
                          if ("操作成功" != STATUS[result.StatusCode]) {
                              $("#errorText").text(STATUS[result.StatusCode]).parent().show();
                          } else {
                              ModalHtml.showSuccess("操作成功!", "角色分配完成", "/Privilege/UserIndex");
                          }
                      },
                      function (XMLHttpRequest) {
                      });
                }
            });
        }

    };

}();
