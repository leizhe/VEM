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
            },
            MenuUrl: {
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
           },
           MenuUrl: {
               required: "必须填写URL"
           },

       },
        "/System/ChildMenuCreateEdit",
         "/System/MenuManager",
        "添加子菜单成功"
        );

});
