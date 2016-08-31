jQuery(document).ready(function () {
    FormValidation(
       '#UserCreateForm',
       {
           LoginName: {
               required: true
           },
           LoginPassword: {
               minlength: 5,
               required: true
           },
           Tel: {
               minlength: 5,
               required: true
           },
           Email: {
               email: true
           },
           CompanyName: {
               minlength: 2,
           },

           CitySelect: {
               min: true
           },
           DistrictSelect: {
               min: true
           }

       },
       {
           LoginName: {
               required: "请填写用户名"
           },
           LoginPassword: {
               minlength: "密码不能少与{0}位",
               required: "请填写密码"
           },
           Tel: {
               minlength: "电话号不能少于{0}位",
               required: "请输入电话号码"
           },
           Email: {
               email: "电子邮件格式不正确"
           },
           CompanyName: {
               minlength: "公司名不能少于{0}个字"
           },

           CitySelect: {
               min: "必须选择城市"
           },
           DistrictSelect: {
               min: "必须选择区县"
           }

       },
       "/Privilege/UserCreate",
       "/Privilege/UserIndex",
       "添加用户成功"
        );

});
