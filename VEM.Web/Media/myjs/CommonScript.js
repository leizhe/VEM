

window.Modal = function () {
    var reg = new RegExp("\\[([^\\[\\]]*?)\\]", 'igm');
    var alr = $(".myConfirm");
    var ahtml = alr.html();

    //关闭时恢复 modal html 原样，供下次调用时 replace 用
    //var _init = function () {
    //	alr.on("hidden.bs.modal", function (e) {
    //		$(this).html(ahtml);
    //	});
    //}();


    var _alert = function (options) {
        //$(".modal-backdrop").remove();
        alr.html(ahtml);	// 复原
        alr.find('.ok').removeClass('btn-success').addClass('btn-primary');
        alr.find('.cancel').hide();
        _dialog(options);

        return {
            on: function (callback) {
                if (callback && callback instanceof Function) {
                    alr.find('.ok').click(function () { callback(true) });
                }
            }
        };
    };
    //$(".modal-backdrop").remove(); $('#body').css('modal-open', 'initial');
    var _confirm = function (options) {
        alr.html(ahtml); // 复原
        alr.find('.ok').removeClass('btn-primary').addClass('btn-success');
        alr.find('.cancel').show();
        _dialog(options);

        return {
            on: function (callback) {
                if (callback && callback instanceof Function) {
                    alr.find('.ok').click(function () { callback(true); });
                    alr.find('.cancel').click(function () { callback(false) });
                }
            }
        };
    };

    var _dialog = function (options) {
        var ops = {
            msg: "提示内容",
            title: "操作提示",
            btnok: "确定",
            btncl: "取消"
        };

        $.extend(ops, options);

     

        var html = alr.html().replace(reg, function (node, key) {
            return {
                Title: ops.title,
                Message: ops.msg,
                BtnOk: ops.btnok,
                BtnCancel: ops.btncl
            }[key];
        });

        alr.html(html);
        alr.modal({
            width: 500,
            backdrop: 'static'
        });
    }

    return {
        alert: _alert,
        confirm: _confirm
    }

}();

var ModalHtml = function () {

    return {
        showSuccess: function (title, context, menuUrl) {
            var str = GetSuccessHtml(title, context, menuUrl);
            $("#myHtmlModal").html(str);
            $("#myHtmlModal").attr("class", "alert alert-block alert-success modal hide fade in").attr("aria-hidden", "false").show();
            $("body").append("<div class='modal-backdrop fade in'></div>");
        },
        showHtml: function (title, context, Htmlfooter) {
            var str = [GetHtml(title, context), Htmlfooter].join("\n");
            $("#myHtmlModal").html(str);
            $("#myHtmlModal").attr("class", "alert alert-block alert-success modal hide fade in").attr("aria-hidden", "false").show();
            $("body").append("<div class='modal-backdrop fade in'></div>");
        }
    };
}();

var AjaxRequest = function () {
    return {
        submit: function (url, data, success, send) {
            $.ajax({
                type: 'post',
                url: url,
                data: data,
                cache: false,
                async: false,
                traditional: true,
                dataType: 'json',
                success: function (result) {
                    if ("未知错误" == STATUS[result.StatusCode]) {
                        Modal.alert({ msg: result.Data, title: '错误信息', btnok: '确定' });
                        return false;
                    }
                    success(result);
                },
                error: function () { Modal.alert({ msg: "Ajax提交错误", title: '错误信息', btnok: '确定' }); },
                beforeSend: function (XMLHttpRequest) { send(XMLHttpRequest); }
            });
        }

    };

}();

var PageButton = function () {
    return {
        init: function (btnPrivileString) {
            $(".btn").hide();
            var arr = new Array();
            arr = btnPrivileString.split(',');
            $.each(arr, function () {
                $("[name='" + this + "']").show();
            });
        }
    };
}();

function GetSuccessHtml(title, context,menuUrl) {
    return [
    "<div class='modal-header'>",
          "<button type='button'  onclick='hideModalhtml()' class='close' data-dismiss='modal'></button>",
          " <h4 class='alert-heading'>"+title+"</h4>",
    "</div>",
    "<div class='modal-body alert alert-block alert-success fade in' style='width: 400px;'>",
        "<p>" + context + "</p>",
    "</div>",
    "<div class='modal-footer'>",
    " <a class='btn green' href='javaScript:window.location = window.location;'>留在本页</a>  <a class='btn black' href='" + menuUrl + "'>回到主菜单</a>",
    "</div>"
    ].join("\n");
}

function GetHtml(title, context) {
    return [
    "<div class='modal-header'>",
          "<button type='button'  onclick='hideModalhtml()' class='close' data-dismiss='modal'></button>",
          " <h4 class='alert-heading'>" + title + "</h4>",
    "</div>",
    "<div class='modal-body alert alert-block alert-info fade in'>",
        "<p>" + context + "</p>",
    "</div>"
    ].join("\n");
}

function hideModalhtml() {
    $("#myHtmlModal").attr("class", "alert alert-block alert-success modal hide fade").attr("aria-hidden", "true").hide();
    $(".modal-backdrop").remove();
    return true;
}

function FormValidation(fromName, rulesValue, messageValue, ajaxUrl, backUrl,successMsg ) {
    var form1 = $(fromName);
    var error1 = $('.alert-error', form1);
    var success1 = $('.alert-success', form1);

    form1.validate({
        errorElement: 'span',
        errorClass: 'help-inline',
        focusInvalid: false,
        ignore: "",
        rules: rulesValue,
        messages: messageValue,
        invalidHandler: function (event, validator) {
            success1.hide();
            $("#errorText").text("你的表单有错误. 请重新检查.").parent().show();
            App.scrollTo(error1, -200);
        },

        highlight: function (element) {
            $(element)
                .closest('.help-inline').removeClass('ok');
            $(element)
                .closest('.control-group').removeClass('success').addClass('error');

        },

        unhighlight: function (element) {
            $(element)
                .closest('.control-group').removeClass('error');

        },

        success: function (label) {
            label
                .addClass('valid').addClass('help-inline ok')
            .closest('.control-group').removeClass('error').addClass('success');
            

        },
        submitHandler: function (form) {
            error1.hide();
            AjaxRequest.submit(
                ajaxUrl,
                $(form).serialize(),
               function (result) {
                   if ("操作成功" != STATUS[result.StatusCode]) {
                       $("#errorText").text(STATUS[result.StatusCode]).parent().show();
                   } else {
                       ModalHtml.showSuccess("操作成功!", successMsg, backUrl);
                   }
               },
               function (XMLHttpRequest) {
               });
        }
    });

}

function DleteLink(linkClass,title,msg,callbackMsg) {
    $(linkClass).click(function () {
        var btn = this;
        Modal.confirm(
        {
            title:title,
            msg: msg
        })
        .on(function (e) {
            var alr = $(".myConfirm");
            var ahtml = alr.html();
            alr.html(ahtml);
            if (e) {
                $.getJSON($(btn).attr("href"), { date: new Date() }, function (result) {
                    if (result.Data) {
                        //alert(callbackMsg);
                        Modal.alert({ msg: callbackMsg, title: '提示信息', btnok: '确定' }).on(function (e) {
                            window.location = window.location;
                        });
                        

                    } else {
                        //$(".modal-backdrop").remove();
                      
                        Modal.alert({ msg: STATUS[result.StatusCode], title: '错误信息', btnok: '确定' });
                    }
                });
            }
        });
        return false;
    });
}

function UploadImageFunc(btnid, filePathId, ajaxUrl, successFunc) {
    $("#" + btnid).click(
          function(){
              var filePath = $("input[name='" + filePathId + "']").val();
                 var extStart = filePath.lastIndexOf(".");
                 var ext = filePath.substring(extStart, filePath.length).toUpperCase();

                 if (ext != ".BMP" && ext != ".PNG" && ext != ".JPG" && ext != ".JPEG") {
                         alert("图片仅限于.BMP .PNG .JPG .JPEG文件。");
                         return false;
                     }
                 else {
                         $.ajaxFileUpload({
                             url: ajaxUrl,
                                 secureuri: false,
                                 fileElementId: filePathId,
                                 dataType: 'JSON',
                                 data:{fileType:ext},
                                 success: function (result) {
                                     var reg = /<pre.+?>(.+)<\/pre>/g;
                                     var data = result.match(reg);
                                     result = JSON.parse(RegExp.$1);
                                     if ("操作成功" != STATUS[result.StatusCode]) {
                                         $("#errorText").text(STATUS[result.StatusCode]).parent().show();
                                     } else {
                                         successFunc(result.Data);
                                     }
                                 },
                                 error: function (data, status, e) {
                                    // alert(data+","+status+","+e);
                                 }
                         });
                 }
         });
 }
