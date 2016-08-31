jQuery(document).ready(function () {
    FormValidation(
        '#DistrictForm',
        {
            DistrictName: {
                required: true
            }

        },
        {
            DistrictName: {
                required: "必须填写区县名称"
            }

        },
        "/System/DistrictCreate",
        "/System/RegionManager",
        "添加区县成功"
        );

});
