jQuery(document).ready(function () {
    FormValidation(
        '#EditPassForm',
       {
           oldPass: {
               required: true
           },
           newPass: {
               minlength: 5,
               required: true
           },
           rnewPass: {
               minlength: 5,
               required: true,
               equalTo: "#newPass"
           }
       },
        {
            oldPass: {
                required: "请填写原密码"
            },
            newPass: {
                minlength: "密码长度不能小于{0}位",
                required: "请填写新密码"
            },
            rnewPass: {
                minlength: "密码长度不能小于{0}位",
                required: "请填写重复填写新密码",
                equalTo: "两次密码输入不一致"
            }
        },
        "/Account/EditPass",
        "/Home/index",
        "密码修改成功"
        );

});
