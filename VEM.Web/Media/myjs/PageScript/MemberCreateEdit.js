jQuery(document).ready(function () {
    FormValidation(
       '#MemberForm',
       {
           LoginName: {
               required: true
           },
           Tel: {
               minlength: 5,
           },
           Email: {
               email: true
           }
       },
       {
           LoginName: {
               required: "请填写用户名"
           },
           
           Tel: {
               minlength: "电话号不能少于{0}位",
           },
           Email: {
               email: "电子邮件格式不正确"
           }
       },
       "/Member/MemberCreateEdit",
       "/Member/MemberIndex",
       "操作成功完成"
        );

});
