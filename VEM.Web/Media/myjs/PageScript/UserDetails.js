
jQuery(document).ready(function () {
    UploadImageFunc(
        "btnUploadImage",
        "uploadImage",
        "/Account/UploadImage",
        function (data) {
            $("#showPic").attr("src", data);
            $("input[name='userPic']").val(data);
        });
    FormValidation(
       '#UserDetailsForm',
       {
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
           Tel: {
               minlength: "电话号不能少于{0}位"
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
        "/Account/UserDetails",
       "/Home/Index",
      "您的个人信息修改成功"
        );

});
