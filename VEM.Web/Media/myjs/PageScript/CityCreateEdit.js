jQuery(document).ready(function () {
    FormValidation(
        '#CityForm',
        {
            CityName: {
                required: true
            }

        },
        {
            CityName: {
                required: "必须填写城市名称"
            }

        },
        "/System/CityCreateEdit",
        "/System/RegionManager",
        "您的操作已成功完成"
        );

});
