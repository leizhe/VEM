jQuery(document).ready(function () {
    FormValidation(
        '#MachineModelForm',
        {
            Name: {
                required: true
            },
            ModelCode: {
                required: true
            },
            ContainerRoadCount: {
                required: true,
                min:1

            },

        },
        {
            Name: {
                required: "必须填写名称"
            },
            ModelCode: {
                required: "必须填写型号"
            },
            ContainerRoadCount: {
                required: "必须填写货道数量",
                min:"货道数量必须大于{0}"
            },

        },
        "/Machine/MachineModelCreateEdit",
        "/Machine/MachineModel",
        "您的操作已成功完成"
        );



});
