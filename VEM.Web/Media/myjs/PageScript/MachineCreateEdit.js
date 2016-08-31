
jQuery(document).ready(function () {
    FormValidation(
        '#MachineForm',
        {
            MachineModelSelect: {
                min: true
            },
            MachineCode: {
                required: true
            },
            CitySelect: {
                min: true
            },
            DistrictSelect: {
                min: true
            }
        },
        {
            MachineModelSelect: {
                min: "必须选择型号"
            },
            MachineCode: {
                required: "必须填写本机编号"
            },
            CitySelect: {
                min: "必须选择城市"
            },
            DistrictSelect: {
                min: "必须选择区县"
            }
        },
         "/Machine/MachineCreateEdit",
        "/Machine/MachineIndex",
        "您的操作已成功完成"
        );

});
