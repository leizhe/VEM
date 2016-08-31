
jQuery(document).ready(function () {
    $(".row-details").click(function () {
        var css = $(this).attr("class");
        if (css == "row-details row-details-close") {
            $(this).attr("class", "row-details row-details-open");
            $(this).parent().parent().next().show();
        } else {
            $(this).attr("class", "row-details row-details-close");
            $(this).parent().parent().next().hide();
        }
    });

    $(".up-district").click(function () {
        var btn = this;

        Modal.confirm(
        {
            title: '修改区县',
            msg: "<div class='page-full-width'><input id='dName' style='width:98%' type='text' value='" + $(btn).parent().prev().children().html() + "'/></div>"
        })
        .on(function (e) {
            if (e) {
                AjaxRequest.submit(
                       $(btn).attr("href"),
                       { cid: $(btn).attr("cid"), did: $(btn).attr("did"), dname: $("#dName").val() },
                      function (result) {
                          if ("操作成功" != STATUS[result.StatusCode]) {
                              $("#errorText").text(STATUS[result.StatusCode]).parent().show();
                          } else {
                              $(btn).parent().prev().children().html($("#dName").val());
                              //$(btn).parent().parent().remove();
                              //ModalHtml.showSuccess("操作成功!", "修改用户成功", "/Privilege/UserIndex");
                          }
                      },
                      function (XMLHttpRequest) {
                      });
            }
        });
        return false;
    });


    $(".del-district").click(function () {
        var btn = this;

        Modal.confirm(
        {
            title: '删除区县',
            msg: "确定要删除" + $(btn).parent().prev().children().html() + "吗？"
        })
        .on(function (e) {
            if (e) {
                AjaxRequest.submit(
                       $(btn).attr("href"),
                       {id: $(btn).attr("did")},
                      function (result) {
                          if ("操作成功" != STATUS[result.StatusCode]) {
                              Modal.alert({ msg: STATUS[result.StatusCode], title: '错误信息', btnok: '确定' });
                          } else {
                              $(btn).parent().parent().remove();
                          }
                      },
                      function (XMLHttpRequest) {
                      });
            }
        });
        return false;
    });


    DleteLink(".delete-link", '提示信息', "确认删除这个城市吗？", '城市删除成功');

});
