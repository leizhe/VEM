jQuery(document).ready(function () {
    FormValidation(
       '#RoleForm',
       {
           RoleName: {
               minlength: 2,
               required: true
           }
       },
       {
           RoleName: {
               minlength: "角色名称必须大于2个字",
               required: "请填写角色名称"
           }
       },
       "/Privilege/RoleCreate",
        "/Privilege/RoleIndex",
        "角色创建成功"
        );

});
