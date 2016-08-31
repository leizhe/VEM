$(document).ready(function () {
    DleteLink(".delete-link", '提示信息', '确认删除会员吗？', '会员删除成功');
    DleteLink(".reset-pass", '提示信息', "重置密码吗？重置后密码初始值为：123456", "密码重置成功");
   
    $(".btnMemberCoupon").click(function () {
        MinSpanShow("/Member/GetMemberCoupon?id=" + $(this).attr("mid"))
    });

    $(".btnMemberPay").click(function () {
        MinSpanShow("/Member/GetMemberPayRecord?id=" + $(this).attr("mid"))
    });
});

function MinSpanShow(url) {
    $.get(url, function (result) {
        $("#mainSpan").attr("class", "span8");
        $("#minSpan").html(result).show();
       
    });
}

function AddCoupon(btn) {
    var mid = $(btn).attr("mid");
    $.get("/Member/GetMemberAddCoupon?id=" + mid, function (result) {
        ModalHtml.showHtml("添加优惠券", GetAddCouponHtml(result), GetHtmlfooter());
    });
    
}

function GetAddCouponHtml(htmlResult) {
    return [
    htmlResult,
    ].join("\n");
}

function MemberCouponFormSubmit() {
    AjaxRequest.submit(
               "/Member/AddMemberCoupon",
               $('#MemberCouponForm').serialize(),
              function (result) {
                  if ("操作成功" != STATUS[result.StatusCode]) {
                      Modal.alert({ msg: STATUS[result.StatusCode], title: '错误信息', btnok: '确定' });
                  } else {
                      if (hideModalhtml()) {
                          Modal.alert({ msg: "发放优惠券成功", title: '提示信息', btnok: '确定' })
                              .on(function (e) {
                              if (e) {
                                  window.location = window.location;
                              }
                          });
                      }
                      
                  }
              },
              function (XMLHttpRequest) {
              });
}

function GetHtmlfooter() {
    return [
    "<div class='modal-footer'>",
        " <a class='btn green' href='javaScript:MemberCouponFormSubmit();'>保存</a>  <a class='btn black' onclick='hideModalhtml()' href=''>取消</a>",
    "</div>"
    ].join("\n");
}

function DelMemberCoupon(btn)
{
    Modal.confirm(
    {
        title: "提示信息",
        msg: "确认要删除这个优惠券吗？"
    })
    .on(function (e) {
        if (e) {
            $.getJSON($(btn).attr("url"), { date: new Date() }, function (result) {
                if (result.Data) {
                    alert("删除成功");
                    window.location = window.location;

                } else {
                    Modal.alert({ msg: STATUS[result.StatusCode], title: '错误信息', btnok: '确定' });
                }
            });
        }
    });
    return false;
}
