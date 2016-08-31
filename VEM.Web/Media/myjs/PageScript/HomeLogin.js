var Login = function () {
    var handleLogin = function () {
        $('.login-form').validate({
            errorElement: 'label', 
            errorClass: 'help-inline',
            focusInvalid: false, 
            rules: {
                username: {
                    required: true
                },
                password: {
                    required: true
                },
                remember: {
                    required: false
                }
            },

            messages: {
                username: {
                    required: "用户名不能为空."
                },
                password: {
                    required: "密码不能为空."
                }
            },

            invalidHandler: function (event, validator) {
                $("#errorText").text("请输入你的用户名和密码");
                $('.alert-error', $('.login-form')).show();
            },

            highlight: function (element) {
                $(element)
                    .closest('.control-group').addClass('error'); 
            },

            success: function (label) {
                label.closest('.control-group').removeClass('error');
                label.remove();
            },

            errorPlacement: function (error, element) {
                error.addClass('help-small no-left-padding').insertAfter(element.closest('.input-icon'));
            },
            submitHandler: function (form) {
                AjaxRequest.submit(
                    "/Account/Login",
                    $(form).serialize(),
                   function (result) {
                       if ("操作成功" != STATUS[result.StatusCode]) {
                           $("#errorText").text(STATUS[result.StatusCode]).parent().show();
                       } else {
                           window.location.href = '/Home/Index';
                       }
                   },
                   function (XMLHttpRequest) {
                   });
            }
        });

        $('.login-form input').keypress(function (e) {
            if (e.which == 13) {
                if ($('.login-form').validate().form()) {

                    $('.login-form').submit();
                }
                return false;
            }
        });
    }

    var handleForgetPassword = function () {
        $('.forget-form').validate({
            errorElement: 'label',
            errorClass: 'help-inline',
            focusInvalid: false, 
            ignore: "",
            rules: {
                email: {
                    required: true,
                    email: true
                }
            },

            messages: {
                email: {
                    required: "电子邮件不能为空.",
                    email: "请输入正确的电子邮件地址"
                }
            },

            invalidHandler: function (event, validator) {

            },

            highlight: function (element) {
                $(element)
                    .closest('.control-group').addClass('error'); 
            },

            success: function (label) {
                label.closest('.control-group').removeClass('error');
                label.remove();
            },

            errorPlacement: function (error, element) {
                error.addClass('help-small no-left-padding').insertAfter(element.closest('.input-icon'));
            },

            //submitHandler: function (form) {
            //    window.location.href = "index.html";
            //}
        });

        $('.forget-form input').keypress(function (e) {
            if (e.which == 13) {
                if ($('.forget-form').validate().form()) {
                    $('.forget-form').submit();
                }
                return false;
            }
        });

        jQuery('#forget-password').click(function () {
            jQuery('.login-form').hide();
            jQuery('.forget-form').show();
        });

        jQuery('#back-btn').click(function () {
            jQuery('.login-form').show();
            jQuery('.forget-form').hide();
        });

    }
    return {
        init: function () {
            handleLogin();
            handleForgetPassword();
       
            jQuery('#forget-password').click(function () {
                jQuery('.login-form').hide();
                jQuery('.forget-form').show();
            });

            jQuery('#back-btn').click(function () {
                jQuery('.login-form').show();
                jQuery('.forget-form').hide();
            });

            $.backstretch([
		        "/Media/image/bg/1.jpg",
		        "/Media/image/bg/2.jpg",
		        "/Media/image/bg/3.jpg",
		        "/Media/image/bg/4.jpg"
            ], {
                fade: 1000,
                duration: 8000
            }
        	);

        }

    };

}();

jQuery(document).ready(function () {
    //App.init();
    Login.init();
});