jQuery(document).ready(function () {
    FormValidation(
        '#MenuForm',
        {
            MenuName: {
                required: true
            },
            MenuOrder: {
                number: true,
                required: true
            }

        },
       {
           MenuName: {
               required: "必须填写菜单名称"
           },
           MenuOrder: {
               number: "必须填写数字",
               required: "必须填写顺序"
           }

       },
         "/System/MenuCreateEdit",
        "/System/MenuManager",
        "添加主菜单成功"
       );


    $(".ImportIcon").click(function () {
        AjaxRequest.submit(
                       "/System/ImportIcon",
                      null,
                      function (result) {
                          if ("操作成功" != STATUS[result.StatusCode]) {
                              Modal.alert({ msg: STATUS[result.StatusCode], title: '操作失败', btnok: '确定' });
                          } else {
                              Modal.alert({ msg: "图标导入成功", title: '操作完成', btnok: '确定' });
                          }
                      },
                      function (XMLHttpRequest) {
                      });


    });

    $(".upIcon").click(function () {
        AjaxRequest.submit(
                       "/System/GetIconList",
                      null,
                      function (result) {
                          ShowIconHtml("请选择图标", result);
                      },
                      function (XMLHttpRequest) {
                          //$(".ImportIcon").text("请稍后");
                      });


    });

});



function ShowIconHtml(title, context) {
    var str = GetIconHtml(title, context);
    $("#myHtmlModal").html(str);
    $("#myHtmlModal").attr("class", "alert alert-block alert-info modal hide fade in").attr("aria-hidden", "false").show();
    $("body").append("<div class='modal-backdrop fade in'></div>");
}



function SelectThisIcon(btn) {
    var id = $(btn).attr("id");
    var icon = $(btn).prev().attr("class");
    $('#selectIcon').attr("iconId",id).html("已选择:<i class='" + icon + "'></i>&nbsp;&nbsp;&nbsp;&nbsp;");
}

function SelectIconDone(btn) {
    var id = $(btn).prev().attr("iconId");
    if (id != undefined) {
        var icon = $(btn).prev().children().attr("class");
        $("#MenuPic").val(id);
        $("#pageIcon").attr("class",icon);
        hideModalhtml();
    } else {
        alert("请先选择一个图标");
    }
}


function GetIconHtml(title, context) {
    return [
    "<div class='modal-header'>",
          "<button type='button'  onclick='hideModalhtml()' class='close' data-dismiss='modal'></button>",
          " <h4 class='alert-heading'>" + title + "</h4>",
    "</div>",
    "<div class='modal-body alert alert-block alert-info fade in'  style='font-size:14px;width: 620px;'>",
        "<p>" + context + "</p>",
    "</div>",
     "<div class='modal-footer'>",
    "<span id='selectIcon' style='font-size:16px;'></span><a class='btn green' href='#' onclick='SelectIconDone(this)' >确定</a>  <a onclick='hideModalhtml()' class='btn black' href='#'>取消</a>",
    "</div>"
    ].join("\n");
}