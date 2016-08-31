


jQuery(document).ready(function () {

    DleteLink(".delete-link", '提示信息', "确认删除这个菜单吗？", '菜单删除成功');

    $(".chidrenManager").click(function () {
        var menuNo = $(this).attr("menuNo");
        AjaxRequest.submit(
                      "/System/GetChidrenList",
                     {MenuParendNo:menuNo},
                     function (result) {
                         $("#MainMenuSpan").attr("class","span6")
                         $("#childRenMenu").html(result).parent().parent().show();
                     },
                     function (XMLHttpRequest) {
                         //$(".ImportIcon").text("请稍后");
                     });

    });
    

});


function delMenu(btn) {
    Modal.confirm(
       {
           msg: "确认删除这个菜单吗？"
       })
       .on(function (e) {
           var alr = $(".myConfirm");
           var ahtml = alr.html();
           alr.html(ahtml);
           if (e) {
               $.getJSON($(btn).attr("url"), { date: new Date() }, function (result) {
                   if (result.Data) {
                       Modal.alert({ msg: "删除成功", title: '提示信息', btnok: '确定' }).on(function (e) {
                           window.location = window.location;
                       });

                   } else {
                       Modal.alert({ msg: STATUS[result.StatusCode], title: '错误信息', btnok: '确定' });
                   }
               });
           }
       });
    return false;
}