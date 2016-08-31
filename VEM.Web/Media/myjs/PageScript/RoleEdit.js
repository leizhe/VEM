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
       "/Privilege/RoleEdit",
        "/Privilege/RoleIndex",
        "角色修改成功"
        );

});
